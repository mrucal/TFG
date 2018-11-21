using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject sol;
    GameObject emoticono;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Soleado()
    {
        sol.GetComponent<Animator>().Play("SolAnimation");
    }

    void Nublado()
    {
        sol.GetComponent<Animator>().Play("SolNubeAnimation");
    }
}
