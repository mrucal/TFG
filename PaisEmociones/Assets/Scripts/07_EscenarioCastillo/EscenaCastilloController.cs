using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaCastilloController  : MonoBehaviour {

    public GameObject personaje;
    public GameObject sol;
    //public GameObject pelota_movimiento;

    private const float td = 0f;
    private const float t_sig_escena = 5f;

    static string escena_anterior = "laberinto";

    // Use this for initialization
    void Start () {
        sol.GetComponent<Animator>().Play("SolAnimation");
        if (escena_anterior.Equals("laberinto"))
        {
            personaje.GetComponent<Animator>().Play("PersonajeLlegaCastillo");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void InterruptorOn()
    {
        StartCoroutine(SiguienteEscena("08_EscenaParejas", t_sig_escena));
        escena_anterior = "parejas";
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
