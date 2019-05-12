using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonConfiguracion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        Invoke("AbrirMenuConfiguración", 2f);
    }

    public void AbrirMenuConfiguración()
    {
        print("CONFIG!!");
    }
}
