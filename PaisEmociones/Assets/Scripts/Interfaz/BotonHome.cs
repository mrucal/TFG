using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonHome : MonoBehaviour {

    static BotonHome bh = null;

    private void Awake()
    {
        if (bh == null)
        {
            bh = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (bh != this)
            Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        Invoke("GoHome", 2f);
    }
    
    private void GoHome()
    {
        Destroy(gameObject);
        try
        {
            Destroy(GameObject.Find("Boton").GetComponent<BotonTrofeos>().gameObject);
        }
        catch { }
        SceneManager.LoadScene("00_PantallaInicial");
    }
}
