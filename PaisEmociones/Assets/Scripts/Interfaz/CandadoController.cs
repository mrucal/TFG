using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandadoController : MonoBehaviour {

    public bool llaveL = false, llaveR = false, abierto = false;

    public GameObject controller;
    public string funcion_activar;

    public bool LR = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (LR)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //print("PRESS L");
                desbloquearL();
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                //print("DE_PRESS L");
                bloquearL();
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //print("PRESS R");
                desbloquearR();
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                //print("DE_PRESS R");
                bloquearR();
            }
        }else
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //print("PRESS L");
                desbloquearL();
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                //print("DE_PRESS L");
                bloquearL();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //print("PRESS R");
                desbloquearR();
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                //print("DE_PRESS R");
                bloquearR();
            }
        }
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            abierto = false;
        }*/
        if (llaveL && llaveR && !abierto)
        {
            abierto = true;
            activar();
        }
	}

    public void activar()
    {
        GetComponent<AudioSource>().Play();
        GameObject.Find("SwitchController").GetComponent<SwitchController>().desactivar_objetos();
        controller.SendMessage(funcion_activar);
    }

    public void bloquearL()
    {
        llaveL = false;
    }

    public void desbloquearL()
    {
        llaveL = true;
    }

    public void bloquearR()
    {
        llaveR = false;
    }

    public void desbloquearR()
    {
        llaveR = true;
    }
}
