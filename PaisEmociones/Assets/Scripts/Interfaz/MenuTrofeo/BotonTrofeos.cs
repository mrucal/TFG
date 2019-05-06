using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonTrofeos : MonoBehaviour {

    BotonTrofeos bt;

    public MenuTrofeo mt;
    public SwitchController switch_controller;

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        try{
            switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
        }catch { }
    }

    private void Awake()
    {
        /*if (bt == null)
        {
            bt = this;            
            DontDestroyOnLoad(gameObject);
        }
        else if (bt != this)
            Destroy(gameObject);*/
        SceneManager.sceneLoaded += this.OnLoadCallback;
        switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
    }

    void Start()
    {
        

    }

    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        switch_controller.desactivar_objetos();
        Invoke("activarMenu", 1f);
    }

    private void activarMenu()
    {
        mt.GetComponent<Canvas>().enabled = true;
        mt.GetComponent<BoxCollider>().enabled = true;
    }
}
