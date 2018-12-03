using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaPortalController : MonoBehaviour {

    public GameObject personaje;

    public GameObject sol;

    private const float t_sig_escena = 2f;

    private const float td = 0f;
    private const float t_entrar_portal = 7f;

    private bool enabled_portal = false;

    // Use this for initialization
    void Start () {
            sol.GetComponent<Animator>().Play("SolAnimation");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void enablePortal()
    {
        //print("ACTIVADO!!");
        enabled_portal = true;
    }

    void PortalRun()
    {
        if (enabled_portal)
        {
            personaje.GetComponent<Animator>().Play("PersonajeEntrarPortal");
            StartCoroutine(SiguienteEscena("03_EscenaLlegada", t_entrar_portal + t_sig_escena));
        }
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }

}
