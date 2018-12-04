using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaMagoController  : MonoBehaviour {

    public GameObject personaje;

    private const float t_sig_escena = 2f;

    private const float td = 0f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Inicio()
    {
        StartCoroutine(SiguienteEscena("07_EscenaCastillo", t_sig_escena));
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
