using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasilloTexturas  : MonoBehaviour {
    
    public Texture2D[] texturas_pasillo_dif0;
    public Texture2D[] texturas_pasillo_dif1;
    public Texture2D[] texturas_pasillo_dif2;

    public Texture2D[][] texturas_pasillo;

    void Start () {
        texturas_pasillo = new Texture2D[3][];
        texturas_pasillo[0] = texturas_pasillo_dif0;
        texturas_pasillo[1] = texturas_pasillo_dif1;
        texturas_pasillo[2] = texturas_pasillo_dif2;
    }

    private void OnMouseDown()
    {
    }

    public Texture2D obtenerTextura(int dificultad, int i)
    {
        //print("break");
        return texturas_pasillo[dificultad][i];
    }



}
