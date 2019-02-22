using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EmocionTexturas  : MonoBehaviour {
    
    public Texture2D[] texturas_pasillo_dif0;
    public Texture2D[] texturas_pasillo_dif1;
    public Texture2D[] texturas_pasillo_dif2;

    public Texture2D[][] texturas_pasillo;

    public List<int> elegidos;

    void Awake () {
        texturas_pasillo = new Texture2D[3][];
        texturas_pasillo[0] = texturas_pasillo_dif0;
        texturas_pasillo[1] = texturas_pasillo_dif1;
        texturas_pasillo[2] = texturas_pasillo_dif2;
    }

    private void OnMouseDown()
    {
    }
    
    private int elegirTextura(int dificultad)
    {
        int r;
        if (elegidos.Count == texturas_pasillo[dificultad].Length)
            elegidos.Clear();
        do
        {
            r = Random.Range(0, texturas_pasillo[dificultad].Length);
        } while (elegidos.Contains(r));
        elegidos.Add(r);
        return r;
    }
    public Texture2D obtenerTextura(int dificultad, int i)
    {
        //print("break");
        return texturas_pasillo[dificultad][i];
    }

    public Texture2D obtenerTextura(int dificultad)
    {
        return texturas_pasillo[dificultad][elegirTextura(dificultad)];
    }


}
