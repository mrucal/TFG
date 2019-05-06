using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBasicoController : MonoBehaviour {
    
    public void PlaySonido_i(int i)
    {
        GetComponents<AudioSource>()[i].Play();
    }

    public void StopSonido(int i)
    {
        GetComponents<AudioSource>()[i].Stop();
    }
}
