using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectScript : MonoBehaviour
{

    public GameObject controller_escena;
    public string enableFunction;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void enable()
    {
        controller_escena.SendMessage(enableFunction);
    }
}
