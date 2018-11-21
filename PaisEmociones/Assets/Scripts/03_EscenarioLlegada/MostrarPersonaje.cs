using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarPersonaje : MonoBehaviour {


    public GameObject personaje;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void mostrar()
    {
        print("Mostrar Personaje");
        Color color = personaje.GetComponent<SpriteRenderer>().color;
        color.a = 255;
    }
}
