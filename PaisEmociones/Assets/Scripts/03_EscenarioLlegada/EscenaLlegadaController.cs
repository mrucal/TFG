using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaLlegadaController : MonoBehaviour {

    public GameObject personaje;

    public GameObject sol;

    private const float t_sig_escena = 2f;

    private const float t_animacion_inicial = 7.3f;
    private const float td = 0f;

    // Use this for initialization
    void Start () {
        sol.GetComponent<Animator>().Play("SolNubeAnimation");
        StartCoroutine(PersonajeFeliz(t_animacion_inicial+td));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator PersonajeFeliz(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        //print("BREAK " + personaje.name);
        personaje.GetComponent<Animator>().Play("PersonajeFelizIdleAnimation");
        sol.GetComponent<Animator>().Play("SolAnimation");
        StartCoroutine(SiguienteEscena("04_EscenaDragon", t_sig_escena));
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
    

}
