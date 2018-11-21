using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPortal : MonoBehaviour {

    public GameObject portal;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DestruirPortal()
    {
        Destroy(portal);
    }
}
