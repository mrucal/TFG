using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAudioController : MonoBehaviour {

    public AudioSource[] sonidos;
    public AudioSource sonido_reir;
    public AudioSource sonido_llorar;
    public AudioSource sonido_enfado;

    private void Awake()
    {
        sonidos = GetComponents<AudioSource>();
        sonido_reir = sonidos[0];
        sonido_llorar = sonidos[1];
        sonido_enfado = sonidos[2];
    }

    public void PlayLlorarSonido()
    {
        sonido_llorar.Play();
    }

    public void StopLlorarSonido()
    {
        sonido_llorar.Stop();
    }
}
