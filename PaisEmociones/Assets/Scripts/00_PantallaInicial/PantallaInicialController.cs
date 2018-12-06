using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicialController  : MonoBehaviour {

    public GameObject boton_inicio;

    private const float t_sig_escena = 2f;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Inicio()
    {
        boton_inicio.GetComponent<Animator>().Play("BotonInicioPulsado");
        StartCoroutine(SiguienteEscena("01_EscenaParque", 1+t_sig_escena));
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
