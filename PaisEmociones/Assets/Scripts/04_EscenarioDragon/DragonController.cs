using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour {

    private Animator animator;

    public GameObject personaje;

    /*private AudioSource[] sonidos;
    private AudioSource gruñidoSonido;
    private AudioSource fuegoSonido;

    private void Awake()
    {
        sonidos = gameObject.GetComponents<AudioSource>();
        gruñidoSonido = sonidos[0];
        fuegoSonido = sonidos[1];
    }*/

    void Start () {
        animator = GetComponent<Animator>();	
	}

    public void cambiarEstado(string estado = null)
    {
        if (estado != null)
            animator.Play(estado);
    }
    /*
    public void PlayGruñidoSonido()
    {
        gruñidoSonido.Play();
    }

    public void PlayFuegoSonido()
    {
        fuegoSonido.Play();
    }*/
    
    public void terminarAnimacionFeliz()
    {
        //GetComponent<Animator>().Play("DragonFelizAnimation");
        //print("Animacion");
        GetComponents<AudioSource>()[3].Play();
        Invoke("terminarAnimacionFeliz2",GetComponents<AudioSource>()[3].clip.length+0.5f);
    }

    public void terminarAnimacionFeliz2()
    {
        GetComponent<Animator>().Play("DragonFelizAnimationMove");
    }

    public void terminarAnimacionFeliz3()
    {
        personaje.GetComponent<Animator>().Play("PersonajeAndandoAstilla2");
    }

    public void terminarAnimacionEspada()
    {
        personaje.GetComponent<Animator>().Play("PersonajeAndandoEspada2");
    }
}
