using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScript : MonoBehaviour {

    private bool click;

    public GameObject touchObject;
    public GameObject controller;
    public string name_function;
    


    void Start()
    {
        click = false;
    }


    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
               // print(touchObject.name + " " + hit.transform.name);
                if (hit.transform.name == touchObject.name && !click)
                {
                    //print("Clcik en " + controller.name + "!!");
                    StartCoroutine(resetClick());
                    //print(name_function);
                    controller.SendMessage(name_function);
                    /*escena.SendMessage("cambiarEstado", "EscenaDragonFeliz");
                    dragon.SendMessage("cambiarEstado", "DragonFelizAnimation");*/
                }
            }
        }
    }


    IEnumerator resetClick()
    {
        click = true;
        yield return new WaitForSeconds(0.1f);
        click = false;
    }
}
