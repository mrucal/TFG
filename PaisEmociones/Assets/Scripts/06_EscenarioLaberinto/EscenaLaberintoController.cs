using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaLaberintoController  : MonoBehaviour {

    public GameObject personaje;
    //public GameObject pelota_movimiento;

    public int dificultad;
    private int n_cruces;
    private int cruce_i = 0, cruce_actual;
    private List<int> cruces;

    private int n_pasillos = 3;
    public Pasillo[] pasillos;

    /*public PasilloTexturas texturas0;
    public PasilloTexturas texturas1;
    public PasilloTexturas texturas2;*/

    public PasilloTexturas[] texturas;// = new PasilloTexturas[3];

    private int[] soluciones_dif0 = { 0, 1, 0, 2, 2, 1 };
    private int[] soluciones_dif1 = { 0, 1, 0, 2, 2, 1 };
    private int[] soluciones_dif2 = { 0, 1, 0, 2, 2, 1 };//GVGRRV

    private int[][] soluciones = new int[3][];

    private int solucion_actual;

    private bool clicks_activos = true;

    private const float t_sig_escena = 2f;

    private const float td = 0f;

    // Use this for initialization
    void Start () {
        n_cruces =  2 + (2 * dificultad);
        soluciones[0] = soluciones_dif0;
        soluciones[1] = soluciones_dif1;
        soluciones[2] = soluciones_dif2;
        /*texturas[0] = texturas0;
        texturas[1] = texturas1;
        texturas[2] = texturas2;*/
        cruces = new List<int>();
        for (int i = 0; i < soluciones[dificultad].Length; i++)
            cruces.Add(i);
        siguienteCruce();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool isClicksActivos()
    {
        return clicks_activos;
    }

    void mostrarCruceLaberinto(int dificultad,int i)
    {
        List<int> orden = new List<int>();
        for (int j = 0; j < 3; j++)
            orden.Add(j);
        int r;
        for (int j = 0; j < n_pasillos; j++){
            r = Random.Range(0, orden.Count);
            pasillos[j].mostrarCrucePasillo(texturas[orden[r]].obtenerTextura(dificultad, i));
            if (soluciones[dificultad][cruce_actual] == orden[r])
                solucion_actual = j;
            //print(soluciones[dificultad][cruce_actual] + " " + orden[r] + " " + r + " " + orden.Count);
            orden.RemoveAt(r);

        }

        /*for (int j = 0; j < n_pasillos; j++)
            //pasillos[j].mostrarCrucePasillo(dificultad, i);
            pasillos[j].mostrarCrucePasillo(texturas[j].obtenerTextura(dificultad,i));*/
    }

    void siguienteCruce()
    {
        //print(cruces.Count);
        int r = Random.Range(0, cruces.Count);print(r+" "+cruces.Count);
        cruce_actual = cruces[r];
        print("Cruce_i:" + cruce_i + " cruce_actual:" + cruce_actual + " n_cruces:" + n_cruces);
        mostrarCruceLaberinto(dificultad, cruce_actual);
        cruces.Remove(cruce_actual);
        cruce_i++;
    }

    public void corregirCruce(int idPasillo)
    {
        if (solucion_actual == idPasillo) {//(soluciones[dificultad][cruce_actual] == idPasillo){
            print("Cruce " + cruce_actual + ": CORRECTO!!");
        }else{
            print("Cruce " + cruce_actual + ": HAS FALLADO :(");
        }
        if (cruce_i < n_cruces){
            siguienteCruce();
        }else
        {
            StartCoroutine(SiguienteEscena("07_EscenaCastillo", t_sig_escena));
        }
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
