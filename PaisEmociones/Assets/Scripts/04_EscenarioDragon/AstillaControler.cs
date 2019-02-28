using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstillaControler : MonoBehaviour {
    private bool click;

    public GameObject escena;


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
                if (hit.transform.name == "Astilla" && !click)
                {
                    print("Clcik en astilla!!");
                    //StartCoroutine(resetClick());
                    click = true;
                    escena.SendMessage("AnimacionAstilla");
                    /*escena.SendMessage("cambiarEstado", "EscenaDragonFeliz");
                    dragon.SendMessage("cambiarEstado", "DragonFelizAnimation");*/
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
