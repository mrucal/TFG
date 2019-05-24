using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectScript : MonoBehaviour
{

    public GameObject controller_escena;
    public string enableFunction;

    public string[] enableFunctions;

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

    public void enable_i(int i)
    {
        controller_escena.SendMessage(enableFunctions[i]);
    }
}
