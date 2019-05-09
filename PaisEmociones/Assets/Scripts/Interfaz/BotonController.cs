using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonController : MonoBehaviour {

    public GameObject controller;
    public string name_function;

    public void llamar_funcion()
    {
        controller.SendMessage(name_function);
    }

}
