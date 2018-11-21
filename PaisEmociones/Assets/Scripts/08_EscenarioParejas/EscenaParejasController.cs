using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaParejasController  : MonoBehaviour {

    public GameObject personaje;
    //public GameObject pelota_movimiento;

    private const float td = 0f;
    private const float t_sig_escena = 5f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Inicio()
    {
        print("hola");
        StartCoroutine(SiguienteEscena("07_EscenaCastillo", t_sig_escena));
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
