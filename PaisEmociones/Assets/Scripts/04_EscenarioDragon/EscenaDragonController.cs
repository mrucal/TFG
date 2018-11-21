using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaDragonController : MonoBehaviour
{

    private Animator animator;
    public GameObject personaje;
    public GameObject dragon;
    public GameObject espada;
    public GameObject astilla;
    public GameObject conejo;
    public GameObject emoticono;

    private const float t_sig_escena = 5.0f;

    private const float t_pers_dr_as = 4.6f;
    private const float t_pers_fin_as = 2.6f;
    private const float t_pers_dr_esp = 4.5f;
    private const float t_pers_fin_esp = 12.6f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void cambiarEstado(string estado = null)
    {
        if (estado != null)
            animator.Play(estado);
    }

    /*public void AnimacionAstilla()
    {
        Destroy(astilla);
        Destroy(espada);
        cambiarEstado("EscenaDragonFeliz");
        dragon.SendMessage("cambiarEstado", "DragonFelizAnimation");    
        personaje.GetComponent<Animator>().Play("PersonajeAndando");

    }*/

    public void AnimacionAstilla()
    {
        //Destroy(astilla);
        //Destroy(espada);
        conejo.GetComponent<Animator>().Play("ConejoAndandoAnimation");
        personaje.GetComponent<Animator>().Play("PersonajeAndandoAstilla");
        espada.GetComponent<Animator>().Play("EspadaQuietaAnimation");
        StartCoroutine(esperarPersonajeAstilla(t_pers_dr_as));
        //cambiarEstado("EscenaDragonFeliz");
        //dragon.SendMessage("cambiarEstado", "DragonFelizAnimation");

    }

    IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }

    IEnumerator esperarPersonajeAstilla(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(astilla);
        emoticono.GetComponent<Animator>().Play("AlegriaAnimation");
        cambiarEstado("EscenaDragonFeliz");
        dragon.SendMessage("cambiarEstado", "DragonFelizAnimation");
        StartCoroutine(SiguienteEscena("05_EscenaPueblo", t_pers_fin_as + t_sig_escena/*7f*/));
    }

    public void AnimacionEspada()
    {
        Destroy(espada);
        //dragon.SendMessage("cambiarEstado", "DragonEnfadoMaximoAnimation");
        personaje.GetComponent<Animator>().Play("PersonajeAndandoEspada");
        conejo.GetComponent<Animator>().Play("ConejoAndandoAnimation");
        StartCoroutine(esperarPersonajeEspada(t_pers_dr_esp/*4.5f*/));

    }

    IEnumerator esperarPersonajeEspada(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        dragon.SendMessage("cambiarEstado", "DragonEnfadoMaximoAnimation");
        astilla.GetComponent<Animator>().Play("AstillaMoveAnimation");
        StartCoroutine(SiguienteEscena("05_EscenaPueblo", t_pers_fin_esp + t_sig_escena/*7f*/));
    }
}
