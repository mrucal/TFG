using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje1 : MonoBehaviour {
    public void playGuion()
    {
        GetComponent<AudioSource>().Play();
    }
}
