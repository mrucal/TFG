using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje2 : MonoBehaviour {

    public GameObject controller_escena;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void activarPortal()
    {
        controller_escena.SendMessage("activarPortal");
    }
}
