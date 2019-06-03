using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EstadoJuego : MonoBehaviour
{
    public static EstadoJuego ej;
    public Datos datos;

    private MenuTrofeo2 mt = null;

    private void Awake()
    {
        if (ej == null)
        {
            ej = this;
            DontDestroyOnLoad(gameObject);
            datos.dificultad = 0;
            datos.ultima_escena = "01_EscenaParque";
        }
        else if (ej != this)
        {
            Destroy(gameObject);
        }
    }

    public void guardar()
    {
        /*print("BREAK GUARDAR " + datos.fallos[0][2]);
        datos.aciertosd = datos.aciertos[datos.dificultad];
        datos.fallosd = datos.fallos[datos.dificultad];*/
        string estadojson = JsonUtility.ToJson(datos);
        //print("JSON GUARDAR: " + estadojson);
        PlayerPrefs.SetString("EstadoJuego", estadojson);
    }

    public void cargar()
    {
        string estadojson = PlayerPrefs.GetString("EstadoJuego");
        //print("JSON CARGAR: " + estadojson);
        if (!string.IsNullOrEmpty(estadojson))
        {
            datos = JsonUtility.FromJson<Datos>(estadojson);
            /*datos.aciertosd = datos.aciertos[datos.dificultad];
            datos.fallosd = datos.fallos[datos.dificultad];*/
            if (mt != null)
                mt.Actualizar();
        }
    }

    public void reset()
    {
        datos = new global::Datos();
        /*datos.dificultad = 0;
        datos.ultima_escena = null;
        datos.aciertos = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        datos.fallos = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };*/
        guardar();
    }

    public void asignarMenuTrofeo(MenuTrofeo2 mt)
    {
        this.mt = mt;
    }

    public void ganarTrofeo(int emocion)
    {
        datos.trofeos[(datos.dificultad * 3) + emocion]++;
        if (mt != null)
            mt.Actualizar();
    }

    public void perderTrofeo(int emocion)
    {
        if(datos.trofeos[(datos.dificultad * 3) + emocion]>0)
            datos.trofeos[(datos.dificultad * 3) + emocion]--;
        if (mt != null)
            mt.Actualizar();
    }

    public int getAciertosEmocion(int emocion)
    {
        return datos.aciertos[(datos.dificultad *3)+emocion];
    }

    public int getAciertosLaberintoEmocion(int emocion)
    {
        return datos.aciertos_laberinto[(datos.dificultad * 3) + emocion];
    }

    public void incrementarAciertos(int emocion)
    {
        datos.aciertos[(datos.dificultad * 3) + emocion]++;
        ganarTrofeo(emocion);
        datos.aciertos_general++;
        datos.tot_general++;
    }

    public int getFallosEmocion(int emocion)
    {
        return datos.fallos[(datos.dificultad * 3) + emocion];
    }

    public int getFallosLaberintoEmocion(int emocion)
    {
        return datos.fallos_laberinto[(datos.dificultad * 3) + emocion];
    }

    public void incrementarFallos(int emocion)
    {
        datos.fallos[(datos.dificultad * 3) + emocion]++;
        perderTrofeo(emocion);
        datos.fallos_general++;
        datos.tot_general++;
    }

    public void incrementarAciertosLaberinto(int emocion)
    {
        datos.aciertos_laberinto[(datos.dificultad * 3) + emocion]++;
        ganarTrofeo(emocion);
        ganarTrofeo(emocion);
        //datos.aciertos_general++;
        datos.total_laberinto[(datos.dificultad * 3) + emocion]++;
    }

    public void incrementarFallosLaberinto(int emocion)
    {
        datos.fallos_laberinto[(datos.dificultad * 3) + emocion]++;
        perderTrofeo(emocion);
        //datos.fallos_general++;
    }

    public int getTotalLaberinto()
    {
        int total = 0;
        for (int i = (datos.dificultad * 3); i < 3; i++)
            total += datos.total_laberinto[i];
        return total;
    }

    public int getAciertosLaberinto()
    {
        int total = 0;
        for (int i = (datos.dificultad * 3); i < 3; i++)
            total += datos.aciertos_laberinto[i];
        return total;
    }

    public int getFallosLaberinto()
    {
        int total = 0;
        for (int i = (datos.dificultad * 3); i < 3; i++)
            total += datos.fallos_laberinto[i];
        return total/2;
    }

    public bool juegoGanado()
    {
        for (int i = 0; i < 3; i++)
            if (datos.trofeos[(datos.dificultad * 3) + i] < datos.min_trofeos[(datos.dificultad * 3) + i])
                return false;

        return true;
    }

    public int[] getMinTrofeos()
    {
        int[] mt = { 0, 0, 0 };
        for(int i=0; i < 3; i++)
            mt[i] = datos.min_trofeos[(datos.dificultad*3)+i];
        
        return mt;
    }

    public void incremetarParejaEmocion(int emocion)
    {
        datos.total_parejas_emocion[(datos.dificultad * 3) + emocion]++;
    }

    public void incremetarIntentosTablero(int tablero)
    {
        datos.intentos_parejas[(datos.dificultad * 3) + tablero]++;
    }

    public void resetIntentosTablero()
    {
        for(int i = (datos.dificultad * 3); i< 3;i++)
            datos.intentos_parejas[i] = 0;
    }

    /* public int getTotalParejasTablero(int tablero)
     {
         int total = 0;
         for (int i = (datos.dificultad * 3); i < 3; i++)
             total += datos.total_parejas_emocion[i];
         return total / 2;
     }*/
    public int getIntentosTablero(int tablero)
    {
        return datos.intentos_parejas[(datos.dificultad * 3) + tablero];
    }

    public void siguienteEscena(bool acierto = true)
    {
        //print("ZIGUIENTE EZENA: " + acierto+" indice: "+datos.indice_escena + " len: " + datos.escenas.Count);
        if (!acierto)
            datos.indice_escena++;
        else
            datos.escenas.RemoveAt(datos.indice_escena);

        //print("ZIGUIENTE EZENA2: " + acierto + " indice: " + datos.indice_escena+" len: "+datos.escenas.Count);
        guardar();
        SceneManager.LoadScene(datos.escenas[datos.indice_escena]);
    }

    public void siguienteEscenaBoton()
    {
        datos.indice_escena++;
        guardar();
        SceneManager.LoadScene(datos.escenas[datos.indice_escena]);
    }

    public void anteriorEscenaBoton()
    {
        datos.indice_escena--;
        guardar();
        SceneManager.LoadScene(datos.escenas[datos.indice_escena]);

    }
}

[System.Serializable]
public class Datos
{
    public int dificultad = 0;

    public string ultima_escena;

    public List<string> escenas =new List<string>( new string[]{ "01_EscenaParque" , "02_EscenaPortal", "03_EscenaLlegada" 
                                    , "04_EscenaDragon", "05_EscenaPueblo","06_EscenaCastillo"
                                    , "07_EscenaParejas","06_EscenaCastillo","08_EscenaLaberinto"
                                    ,"09_EscenaMago","10_EscenaPatio","01_EscenaParque" ,});

    public bool modo_atras = false;

    public bool parejas = false;

    public bool final = false;

    public int indice_escena = 0;

    public int[] trofeos = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public int[] min_trofeos = { 2, 3, 3, 3, 4, 4, 4, 5, 5 };

    public int[] total_general = { 0, 1, 1 };

    public float tot_general = 0f;

    public int aciertos_general;
    public int fallos_general;

    public int[] aciertos = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] fallos = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public int[] total_laberinto = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] aciertos_laberinto = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] fallos_laberinto = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    public int[] total_parejas_emocion = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] intentos_parejas = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] n_parejas = { 0,0,0}; // Una para cada dificultad
    /*public int[][] aciertos = new int[][] { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };
    public int[][] fallos = new int[][] { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };*/

}
