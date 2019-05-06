using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaParejasController : MonoBehaviour {

    //public GameObject personaje;
    //public GameObject pelota_movimiento;

    public AnimacionTrofeo animacion_trofeo;
    public SwitchController switch_controller;

    private const float t_sig_escena = 1f;

    private const float td = 0f;

    public GameObject CasillaPrefab;

    public int dificultad;

    private int n_juegos = 1;
    private int total_juegos = 3;

    private float[] col_dif = {3.0f, 4.0f, 4.0f };
    private float[] fil_dif = {2.0f,2.0f,3.0f};

    private float[] ancho_real_dif = { 12f, 13f, 13f };
    private float[] alto_real_dif = { 7.5f, 6f, 8.5f };

    private int n_parejas;

    public Transform casillas_parent;
    private List<GameObject> casillas = new List<GameObject>();

    public Texture2D[] parejas1dif0;
    public Texture2D[] parejas2dif0;

    public Texture2D[] parejas1dif1;
    public Texture2D[] parejas2dif1;

    public Texture2D[] parejas1dif2;
    public Texture2D[] parejas2dif2;

    public Texture2D[][][] parejas;

    private int turno = 0;
    private bool fin_turno = false;
    private Casilla ultima_casilla = null, casilla_actual = null;

    private EstadoJuego estado_juego;

    private void Iniciar()
    {
        estado_juego.cargar();
        estado_juego.datos.ultima_escena = "07_EscenaParejas";
        estado_juego.guardar();
        //estado_juego.reset();
    }

    private void Finalizar()
    {
        estado_juego.guardar();
    }

    private void Awake()
    {
        estado_juego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        animacion_trofeo = GameObject.Find("AnimacionTrofeo").GetComponent<AnimacionTrofeo>();
        animacion_trofeo.funcion = "asignarParejas";
        animacion_trofeo.controller = gameObject;
        switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
    }

    // Use this for initialization
    void Start()
    {
        Iniciar();
        dificultad = estado_juego.datos.dificultad;
        n_juegos = 1;
        parejas = new Texture2D[3][][];
        for (int i = 0; i < 3; i++)
            parejas[i] = new Texture2D[2][];
        parejas[0][0] = parejas1dif0;
        parejas[0][1] = parejas2dif0;
        parejas[1][0] = parejas1dif1;
        parejas[1][1] = parejas2dif1;
        parejas[2][0] = parejas1dif2;
        parejas[2][1] = parejas2dif2;

        Crear();
	}

    public bool isFinTurno()
    {
        return fin_turno;
    }

    private void Crear()
    {
        int cont = 0;
        float col = col_dif[dificultad];
        float fil = fil_dif[dificultad];
        float ancho_real = ancho_real_dif[dificultad];
        float alto_real = alto_real_dif[dificultad];
        float x_factor = ancho_real / col;
        float x_norm = -((col / 2)/*casillas ancho*/ - (CasillaPrefab.transform.localScale[0] / 2.0f) /*mitad ancho casilla*/- 0.1f /*mitad espacio entre casillas*/ ); // medidas normalizadas (respecto a unidad)
        float x_inicio = x_norm * x_factor;

        float y_factor = alto_real / fil;
        float y_norm = ((fil / 2.0f)/*casillas ancho*/ - (CasillaPrefab.transform.localScale[1] / 2.0f) /*mitad ancho casilla*/- 0.1f /*mitad espacio entre casillas*/ ); // medidas normalizadas (respecto a unidad)
        float y_inicio = (y_norm * y_factor) + 0.26f /*para centrar*/;

        for (int i=0; i < fil; i++){
            for(int j=0; j<col; j++){

                GameObject casillaTemp = Instantiate(CasillaPrefab, new Vector3(((x_factor*j)+x_inicio), (y_factor*-i)+y_inicio, -0.5f), Quaternion.Euler(new Vector3(0,180,0)));

                casillaTemp.transform.localScale = new Vector3(casillaTemp.transform.localScale[0] * x_factor, casillaTemp.transform.localScale[1] * y_factor, casillaTemp.transform.localScale[2]);
                
                casillaTemp.GetComponent<Casilla>().numCasilla = cont;
                //casillaTemp.GetComponent<Casilla>().asignarCarta();

                casillas.Add(casillaTemp);
                switch_controller.objetos.Add(casillaTemp);

                casillaTemp.transform.parent = casillas_parent;

                cont++;
            }
        }
        asignarParejas();
    }

    private void asignarParejas()
    {
        for (int i = 0; i < casillas.Count; i++)
            casillas[i].GetComponent<BoxCollider>().enabled = true;

        n_parejas = casillas.Count / 2; // Número de parejas que tendrá el tablero
        int posibles_parejas = parejas[dificultad][0].Length; // Número total de sprites para las parejas

        List<int> i_tex_tablero = new List<int>(); // Indices aleatorios en la lista de sprites de parejas de las parejas que tendrá el tablero
        while (i_tex_tablero.Count < n_parejas)
        {
            int i = Random.Range(0, posibles_parejas);
            if (!i_tex_tablero.Contains(i))
                i_tex_tablero.Add(i);
        }

        List<Texture2D> parejas_ordenadas = new List<Texture2D>();
        List<int> indices_ordenados = new List<int>();
        for (int i = 0; i < n_parejas; i++){
            parejas_ordenadas.Add(parejas[dificultad][0][i_tex_tablero[i]]);
            //estado_juego.datos.total_parejas = parejas[dificultad][0][i_tex_tablero[i]];
            //print("BREAK PAREJAS " + parejas[dificultad][0][i_tex_tablero[i]].name);
            switch (parejas[dificultad][0][i_tex_tablero[i]].name[2])
            {
                case 'A':
                    estado_juego.datos.total_parejas[0]++;
                    break;
                case 'T':
                    estado_juego.datos.total_parejas[1]++;
                    break;
                case 'E':
                    estado_juego.datos.total_parejas[2]++;
                    break;
            }
            parejas_ordenadas.Add(parejas[dificultad][1][i_tex_tablero[i]]);
            indices_ordenados.Add(i_tex_tablero[i]);
            indices_ordenados.Add(i_tex_tablero[i]);
        }

        int r;
        List<Texture2D> parejas_barajadas = new List<Texture2D>();
        List<int> indices_barajados = new List<int>();
        for (int i=0; i < n_parejas*2; i++)
        {
            r = Random.Range(0, parejas_ordenadas.Count);
            parejas_barajadas.Add(parejas_ordenadas[r]);
            parejas_ordenadas.RemoveAt(r);
            indices_barajados.Add(indices_ordenados[r]);
            indices_ordenados.RemoveAt(r);
        }

        for (int i = 0; i < casillas.Count; i++)
        {
            casillas[i].GetComponent<Casilla>().ocultarCarta();
            casillas[i].GetComponent<Casilla>().asignarCarta(parejas_barajadas[i]);
            casillas[i].GetComponent<Casilla>().idCarta=indices_barajados[i];
        }

    }

    public void destaparCasilla(Casilla c)
    {
        casilla_actual = c;
        if (c.isOculta()) {
            GetComponents<AudioSource>()[0].Play();
            if (turno == 0) { // Se destapa la primera pareja
                c.mostrarCarta();
                ultima_casilla = c;
            } else { // Se destapa la segunda pareja
                fin_turno = true;
                c.mostrarCarta();
                if (dificultad == 2)
                {
                    if(c.carta.name[1] != ultima_casilla.carta.name[1] && c.carta.name[2] == ultima_casilla.carta.name[2])
                    {
                        n_parejas--;
                        print("Quedan " + n_parejas + " parejas");
                        fin_turno = false;
                        if (n_parejas == 0)
                            Invoke("ganar", 1f);
                    }else
                    {
                        Invoke("ocultarAcutalUltima", 1f);
                    }
                }
                else
                {
                    if (c.idCarta != ultima_casilla.idCarta)
                    {
                        Invoke("ocultarAcutalUltima", 1f);
                    }
                    else
                    {
                        n_parejas--;
                        print("Quedan " + n_parejas + " parejas");
                        fin_turno = false;
                        if (n_parejas == 0)
                            Invoke("ganar", 1f);
                    }
                }
            }
            turno = (turno + 1) % 2;
        }
    }

    private void ocultarAcutalUltima()
    {
        print(ultima_casilla.numCasilla + " " + casilla_actual.numCasilla);
        casilla_actual.ocultarCarta();
        ultima_casilla.ocultarCarta();
        fin_turno = false;
    }

    public void ganar()
    {
        print("HAS GANADO!!!!");
        estado_juego.ganarTrofeo(n_juegos - 1);
        if (n_juegos < total_juegos)
        {
            for (int i = 0; i < casillas.Count; i++)
                casillas[i].GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(SiguienteEscena("-", t_sig_escena, n_juegos - 1));
            //asignarParejas();
        }
        else
        {
            Finalizar();
            StartCoroutine(SiguienteEscena("06_EscenaCastillo", t_sig_escena, n_juegos - 1));
        }

        n_juegos++;
    }

    public IEnumerator SiguienteEscena(string escena, float seconds, int emocion)
    {
        yield return new WaitForSeconds(seconds);
        //SceneManager.LoadScene(escena);
        animacion_trofeo.IniciarAnimacion(true, emocion, escena);
    }

}
