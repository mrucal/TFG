﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonTrofeos : MonoBehaviour {

    public static BotonTrofeos bt;

    public MenuTrofeo2 mt;
    public SwitchController switch_controller;

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        try{
            switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
            mt = GameObject.Find("MenuTrofeos").GetComponent<MenuTrofeo2>();
        }
        catch { }
    }

    private void Awake()
    {
        //print("AWAKE BOTON 1");
        if (bt == null)
        {
            //print("AWAKE BOTON 2");
            bt = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (bt != this)
        {
            Destroy(gameObject);
            //print("AWAKE BOTON 3");
        }
        //print("AWAKE BOTON 4");
        SceneManager.sceneLoaded += this.OnLoadCallback;
        switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
        mt = GameObject.Find("MenuTrofeos").GetComponent<MenuTrofeo2>();
    }

    void Start()
    {
        

    }

    private void OnMouseDown()
    {
        //print("BOTON BINGO!");
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
