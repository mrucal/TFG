using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour {

    private Animator animator;

	void Start () {
        animator = GetComponent<Animator>();	
	}
	
	void Update () {
		
	}

    public void cambiarEstado(string estado = null)
    {
        if (estado != null)
            animator.Play(estado);
    }
}
