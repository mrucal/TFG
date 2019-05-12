using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveController : MonoBehaviour
{

    public CandadoController candado;

    public string bloquear, desbloquear;

    public void OnTouchDrag()
    {
        candado.SendMessage(desbloquear);
    }

    public void OnTouchUp()
    {
        candado.SendMessage(bloquear);
    }
}
