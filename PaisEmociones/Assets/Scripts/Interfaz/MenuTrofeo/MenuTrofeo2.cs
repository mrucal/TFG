using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTrofeo2 : MonoBehaviour {

    private static MenuTrofeo2 mt = null;
    public EstadoJuego estado_juego;

    //public GameObject [] prefab;
    public GameObject[] trofeosA;
    public GameObject[] trofeosT;
    public GameObject[] trofeosE;
    public GameObject[][] trofeos;

    private float pos_centro = 371.5f,/* ancho = 300,*/ ancho_trofeo = 60;

    public int[] n_trofeos = null;// = { 4, 6, 2 };
    //MENU1 public List<GameObject> trofeos = new List<GameObject>();
    public SwitchController switch_controller;

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        try
        {
            switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();
            Actualizar();
            //switch_controller.objetos.Insert(0, GameObject.Find("Boton").GetComponent<GameObject>());
        }
        catch { }
    }

    private void Awake()
    {
        if (mt == null)
        {
            //print("AWAKE 1 " + mt);
            mt = this;
            DontDestroyOnLoad(this.gameObject);
            gameObject.GetComponent<Canvas>().enabled = false;
            
            SceneManager.sceneLoaded += this.OnLoadCallback;
            switch_controller = GameObject.Find("SwitchController").GetComponent<SwitchController>();

            trofeos = new GameObject[3][];
            trofeos[0] = trofeosA;
            trofeos[1] = trofeosT;
            trofeos[2] = trofeosE;
        }
        else if (mt != this)
        {
            //print("AWAKE 2");
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
        //print("START 1");
        estado_juego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        //print("B dificultad: " + estado_juego.datos.dificultad);
        n_trofeos = new int[3];
        for (int i = 0; i < 3; i++)
        {
            n_trofeos[i] = estado_juego.datos.trofeos[(estado_juego.datos.dificultad * 3) + i];
            //print("Ntrofeos: " + n_trofeos[i] + " dificultad: " + estado_juego.datos.dificultad);
            //print(n_trofeos[i]+" "+ estado_juego.datos.trofeos[(estado_juego.datos.dificultad * 3) + i]);
        }
        Iniciar();

    }

    public void Clear()
    {
        /*MENU1 for (int i = 0; i < trofeos.Count; i++)
            Destroy(trofeos[i].gameObject);*/
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < trofeos[i].Length; j++)
                trofeos[i][j].SetActive(false);
    }

    public void Iniciar()
    {
        //print("BREAK INICIAR");
        /*MENU1 float[] ancho_real = new float[3];
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

        }*/

        //n_trofeos = new int[3]; n_trofeos[0] = 4; n_trofeos[1] = 2; n_trofeos[2] = 3;
        int nti_max = 7,nti;

        for (int i = 0; i < 3; i++)
        {
            nti = n_trofeos[i] < nti_max ? n_trofeos[i] : nti_max;
            for (int j = 0; j < nti; j++)
            {
                //print(i + " " + j + " "+ trofeos[i][j].name);
                trofeos[i][j].SetActive(true);
            }
        }
    }

    public void Actualizar()
    {
        //print("BREAK ACTUALIZAR");
        /* MENU1 Clear();
        for (int i = 0; i < 3; i++)
        {
            n_trofeos[i] = estado_juego.datos.trofeos[(estado_juego.datos.dificultad * 3) + i];
        }*/
        if (n_trofeos.Length!=0 && estado_juego!=null)
        {
            //print("BREAK ACTUALIZAR 2: " + n_trofeos.Length);
            Clear();
            for (int i = 0; i < 3; i++)
            {
                n_trofeos[i] = estado_juego.datos.trofeos[(estado_juego.datos.dificultad * 3) + i];
                //print("Ntrofeos: " + n_trofeos[i] + " dificultad: " + estado_juego.datos.dificultad);
                //print(n_trofeos[i]+" "+ estado_juego.datos.trofeos[(estado_juego.datos.dificultad * 3) + i]);
            }
            Iniciar();
        }
    }
    private void desactivarMenu()
    {
        GetComponent<AudioSource>().Play();
        Invoke("esconderMenu", 0.5f);
    }

    private void esconderMenu()
    {
        gameObject.GetComponent<Canvas>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        switch_controller.activar_objetos();
    }

}
