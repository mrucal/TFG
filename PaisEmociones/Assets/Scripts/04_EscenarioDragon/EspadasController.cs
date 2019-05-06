using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EspadasController : MonoBehaviour
{
    private bool click;

    public GameObject escena;
    public GameObject dragon;
    

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
                if (hit.transform.name == "Espada" && !click)
                {
                    click = true;
                    //print("Clcik en espada!!");
                    StartCoroutine(resetClick());
                    escena.SendMessage("AnimacionEspada");
                    /*escena.SendMessage("cambiarEstado", "EscenaDragonFeliz");
                    dragon.SendMessage("cambiarEstado", "DragonFelizAnimation");*/
                }
        }
    }


    IEnumerator resetClick()
    {
        yield return new WaitForSeconds(0.1f);
        click = false;
    }
}
