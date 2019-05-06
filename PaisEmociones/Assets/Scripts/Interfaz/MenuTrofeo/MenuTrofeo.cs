using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTrofeo : MonoBehaviour {

    public MenuTrofeo mt = null;
    private EstadoJuego estado_juego;

    public GameObject [] prefab;

    private float pos_centro = 371.5f,/* ancho = 300,*/ ancho_trofeo = 60;

    private int[] n_trofeos;// = { 4, 6, 2 };
    private List<GameObject> trofeos = new List<GameObject>();
    private SwitchController switch_controller;

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        try{
            switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
            //switch_controller.objetos.Insert(0, GameObject.Find("Boton").GetComponent<GameObject>());
        }catch { }
    }

    private void Awake()
    {
        if (mt == null)
        {
            print("AWAKE 1 "+mt);
            mt = this;
            DontDestroyOnLoad(this.gameObject);
            gameObject.GetComponent<Canvas>().enabled = false;
            estado_juego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
            n_trofeos = new int[3];
            for (int i = 0; i < 3; i++)
            {
                n_trofeos[i] = estado_juego.datos.trofeos[(estado_juego.datos.dificultad * 3) + i];
                //print(n_trofeos[i]+" "+ estado_juego.datos.trofeos[(estado_juego.datos.dificultad * 3) + i]);
            }
            SceneManager.sceneLoaded += this.OnLoadCallback;
            switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
        }
        else if (mt != this)
        {
            print("AWAKE 2");
            Destroy(gameObject);
        }
    }

    void Start()
    {

        /*float n = 3;
        float ancho_real = ancho_trofeo * n;
        float inicio = pos_centro - (ancho_real/2) + (ancho_trofeo/2);

        for(int i=0; i<n;i++)
            Instantiate(prefab[0], new Vector3(inicio+(i*ancho_trofeo), prefab[0].transform.position.y, prefab[0].transform.position.z), Quaternion.identity, gameObject.transform);*/
            //print("BREAK START MENU");
        Iniciar();
        
    }

    public void Clear()
    {
        for (int i = 0; i < trofeos.Count; i++)
            Destroy(trofeos[i].gameObject);
    }

    public void Iniciar()
    {
        //print("BREAK INICIAR");
        float[] ancho_real = new float[3];
        float[] inicio = new float[3];

        for (int i = 0; i < 3; i++)
        {
            if (n_trofeos[i] != 0)
            {
                ancho_real[i] = ancho_trofeo * n_trofeos[i];
                inicio[i] = pos_centro - (ancho_real[i] / 2) + (ancho_trofeo / 2);
                for (int j = 0; j < n_trofeos[i]; j++)
                    trofeos.Add(Instantiate(prefab[i], new Vector3(inicio[i] + (j * ancho_trofeo), prefab[i].transform.position.y, prefab[i].transform.position.z), Quaternion.identity, gameObject.transform));
            }

        }
    }

    public void Actualizar()
    {
        //print("BREAK ACTUALIZAR");
        Clear();
        for (int i = 0; i < 3; i++)
        {
            n_trofeos[i] = estado_juego.datos.trofeos[(estado_juego.datos.dificultad * 3) + i];
        }
        Iniciar();
    }
    private void OnMouseDown()
    {
        GetComponent<AudioSource>().Play();
        Invoke("desactivarMenu", 0.5f);
    }

    private void desactivarMenu()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        switch_controller.activar_objetos();
    }

}
