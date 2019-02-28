using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trofeo : MonoBehaviour {

    public string siguiente_escena;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<BoxCollider>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        SceneManager.LoadScene(siguiente_escena);
    }
}
