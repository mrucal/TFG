using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casilla : MonoBehaviour {

    public int numCasilla = 0;

    public int idCarta;

    public EscenaParejasController epc;

    public Texture2D reverso;
    public Texture2D carta = null;

    private bool oculta = true;

    private void Awake()
    {
        epc = GameObject.Find("ControllerParejas").GetComponent<EscenaParejasController>();
    }

    private void OnMouseDown()
    {
        print("NumCasilla: "+numCasilla.ToString()+" IdPareja "+idCarta);
        /*if (oculta){
            if (carta != null)
                mostrarCarta();
        }else
            ocultarCarta();
        oculta = !oculta;*/
        if(!epc.isFinTurno())
            epc.destaparCasilla(this);
    }

    public void asignarCarta(Texture2D carta)
    {
        this.carta = carta;
    }

    public void mostrarCarta()
    {
        GetComponent<MeshRenderer>().material.mainTexture = carta;
        oculta = false;
    }

    public void ocultarCarta()
    {
        GetComponent<MeshRenderer>().material.mainTexture = reverso;
        oculta = true;
    }

    public bool isOculta()
    {
        return oculta;
    }
}
