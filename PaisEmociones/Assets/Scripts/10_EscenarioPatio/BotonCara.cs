using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonCara : MonoBehaviour {

    public int parte = 0;
    public int emocion = 0;

    public Texture2D textura;

    public void asignarParte(int p, int e, Texture2D t)
    {
        parte = p;
        emocion = e;
        textura = t;
        GetComponent<MeshRenderer>().material.mainTexture = textura;
    }

    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        GameObject.Find("JuegoCara").GetComponent<JuegoCara>().ponerParte(parte,emocion);
    }
}
