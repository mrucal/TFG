using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour {

    private Animator animator;

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
}
