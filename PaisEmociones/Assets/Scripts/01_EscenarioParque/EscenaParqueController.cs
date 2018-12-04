using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaParqueController  : MonoBehaviour {

    public GameObject personaje;
    public GameObject pelota;
    public GameObject sol;
    //public GameObject pelota_movimiento;

    private const float t_sig_escena = 2f;

    private const float td = 0f;
    private const float t_transicion = 3f;
    private const float t_patada = 0.65f;
    private const float t_pelota = 2f;

    private string escena_anterior = "Inicio";

	// Use this for initialization
	void Start () {
        sol.GetComponent<Animator>().Play("SolAnimation");
        if (escena_anterior.Equals("Inicio"))
        {
            print("BREAK 1");
            StartCoroutine(patear(td + t_transicion));
            escena_anterior = "Patio";
        }
        else
        {
            print("BREAK 2 "+pelota.transform.position);
            pelota.transform.position = new Vector3(0,0,0);
            print("BREAK 3 " + pelota.transform.position);
            escena_anterior = "Inicio";
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator patear(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        personaje.GetComponent<Animator>().Play("PersonajePelota");
        StartCoroutine(moverpelota(t_patada));
    }

    IEnumerator moverpelota(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        pelota.GetComponent<Animator>().Play("PelotaMoveAnimation");
        StartCoroutine(SiguienteEscena("02_EscenaPortal", t_pelota + t_sig_escena/*7f*/));
        //pelota_movimiento.GetComponent<Animator>().Play("AlegriaAnimation");
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
