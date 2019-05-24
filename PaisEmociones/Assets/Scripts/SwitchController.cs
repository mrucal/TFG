using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour {

    public List<GameObject> objetos;
    public SwitchController sc = null;

    private void Awake()
    {
        try
        {
            objetos[0] = GameObject.Find("Boton").GetComponent<BotonTrofeos>().gameObject;
        }
        catch { }
        if (sc != null)
        {
            objetos = sc.objetos;
            //print("EMPTY");
        }
        //else
            //print("NOT EMPTY");
        
    }

    private void Start()
    {
    }
    public void activar_objeto_i(int i)
    {
        try
        {
            objetos[i].GetComponent<BoxCollider>().enabled = true;
        }
        catch { }
        try
        {
            objetos[i].GetComponent<CapsuleCollider>().enabled = true;
        }
        catch { }
    }

    public void desactivar_objeto_i(int i)
    {
        try
        {
            objetos[i].GetComponent<BoxCollider>().enabled = false;
        }
        catch { }
        try
        {
            objetos[i].GetComponent<CapsuleCollider>().enabled = false;
        }
        catch { }
    }

    public void activar_objetos()
    {
        for (int i = 0; i < objetos.Count; i++)
        {
            try
            {
                objetos[i].GetComponent<BoxCollider>().enabled = true;
            }
            catch { }
            try
            {
                objetos[i].GetComponent<CapsuleCollider>().enabled = true;
            }
            catch { }
        }
    }

    public void desactivar_objetos()
    {
        for (int i = 0; i < objetos.Count; i++)
        {
            try
            {
                objetos[i].GetComponent<BoxCollider>().enabled = false;
            }
            catch { }
            try
            {
                objetos[i].GetComponent<CapsuleCollider>().enabled = false;
            }
            catch { }
        }
    }

    public void activar_objetos_menos(int j)
    {
        for (int i = 0; i < objetos.Count; i++)
        {
            if (j != i)
            {
                try
                {
                    objetos[i].GetComponent<BoxCollider>().enabled = true;
                }
                catch { }
                try
                {
                    objetos[i].GetComponent<CapsuleCollider>().enabled = true;
                }
                catch { }
            }        
        }
    }

    public void desactivar_objetos_menos(int j)
    {
        for(int i = 0; i < objetos.Count; i++)
        {
            if (i != j)
            {
                try
                {
                    objetos[i].GetComponent<BoxCollider>().enabled = false;
                }
                catch { }
                try
                {
                    objetos[i].GetComponent<CapsuleCollider>().enabled = false;
                }
                catch { }
            }
        }
    }

    public void activar_lista_objetos(int[] lista)
    {
        for (int i = 0; i < lista.Length; i++)
        {
            try
            {
                objetos[lista[i]].GetComponent<BoxCollider>().enabled = true;
            }
            catch { }
            try
            {
                objetos[lista[i]].GetComponent<CapsuleCollider>().enabled = true;
            }
            catch { }
        }
    }

    public void desactivar_objetos(int[] lista)
    {
        for (int i = 0; i < lista.Length; i++)
        {
            try
            {
                objetos[lista[i]].GetComponent<BoxCollider>().enabled = false;
            }
            catch { }
            try
            {
                objetos[lista[i]].GetComponent<CapsuleCollider>().enabled = false;
            }
            catch { }
        }
    }
    
}
