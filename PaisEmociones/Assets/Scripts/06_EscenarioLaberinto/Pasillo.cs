using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pasillo  : MonoBehaviour {

    public int idPasillo;

    public EscenaLaberintoController elc;

    /*public Texture2D[] texturas_pasillo_dif0;
    public Texture2D[] texturas_pasillo_dif1;
    public Texture2D[] texturas_pasillo_dif2;

    public Texture2D[][] texturas_pasillo;*/

    void Start () {
        /*texturas_pasillo = new Texture2D[3][];
        texturas_pasillo[0] = texturas_pasillo_dif0;
        texturas_pasillo[1] = texturas_pasillo_dif1;
        texturas_pasillo[2] = texturas_pasillo_dif2;*/
    }

    private void OnMouseDown()
    {
        if (elc.isClicksActivos())
        {
            elc.corregirCruce(idPasillo);
        }
    }

    /*public void mostrarCrucePasillo(int dificultad, int i)
    {
        GetComponent<MeshRenderer>().material.mainTexture = texturas_pasillo[dificultad][i];
    }*/

    public void mostrarCrucePasillo(Texture2D textura)
    {
        GetComponent<MeshRenderer>().material.mainTexture = textura;
    }

}
