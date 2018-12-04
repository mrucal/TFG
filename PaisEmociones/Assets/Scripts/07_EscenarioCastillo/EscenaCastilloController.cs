using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaCastilloController  : MonoBehaviour {

    public GameObject personaje;
    public GameObject interruptor;
    public GameObject sol;
    //public GameObject pelota_movimiento;

    private const float t_sig_escena = 2f;

    private const float td = 0f;
    private const float t_entrada = 5f;

    private bool enabled_interruptor = false;

    static string escena_anterior = "laberinto";

    // Use this for initialization
    void Start () {
        sol.GetComponent<Animator>().Play("SolAnimation");
        enabled_interruptor = false;
        //print(escena_anterior);
        if (escena_anterior.Equals("laberinto"))
        {
            personaje.GetComponent<Animator>().Play("PersonajeLlegaCastillo");
            interruptor.GetComponent<Animator>().Play("PantallaParpadea");
        }else
        {
            interruptor.GetComponent<Animator>().Play("PantallaCorrecta");
            personaje.GetComponent<Animator>().Play("PersonajeEntraCastillo");
            this.GetComponent<Animator>().Play("AbrirCastillo");
            StartCoroutine(SiguienteEscena("09_EscenaMago", t_entrada + t_sig_escena));
            escena_anterior = "laberinto";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void enableInterruptor()
    {
        enabled_interruptor = true;
    }
    
    void InterruptorOn()
    {
        if (enabled_interruptor)
        {
            StartCoroutine(SiguienteEscena("08_EscenaParejas", t_sig_escena));
            escena_anterior = "parejas";
        }
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
