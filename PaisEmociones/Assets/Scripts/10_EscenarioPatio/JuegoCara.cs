using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JuegoCara : MonoBehaviour {

    public EscenaPatioController epc;

    public GameObject enable;
    public SwitchController switch_controller;
    public GameObject niña;

    public RawImage[] partes;
    public int[] emocion_parte = { -1, -1 };

    public Texture2D[] ojos_cara;
    public Texture2D[] boca_cara;
    public Texture2D[][] texturas_partes;
    
    public Texture2D[] boton_ojos_cara;
    public Texture2D[] boton_boca_cara;
    public Texture2D[][] texturas_botones;

    public BotonCara[] botones;
    // Use this for initialization
    void Start () {
        emocion_parte = new int[]{ -1, -1 };

        texturas_partes = new Texture2D[2][];
        texturas_partes[0] = ojos_cara;
        texturas_partes[1] = boca_cara;

        texturas_botones = new Texture2D[2][];
        texturas_botones[0] = boton_ojos_cara;
        texturas_botones[1] = boton_boca_cara;

        asignarPartesBotonoes();

    }
	

    void iniciarJuegoCara()
    {
        niña.GetComponent<Animator>().Play("NiñaTristeIdle");
        niña.GetComponents<AudioSource>()[1].Play();
        niña.GetComponents<AudioSource>()[0].Stop();
        switch_controller.desactivar_objeto_i(1);
        Invoke("enableJuego", 1f);
    }

    void enableJuego()
    {
        BotonHome bh = GameObject.Find("Home").GetComponent<BotonHome>();
        bh.transform.position = new Vector3(5.5f, bh.transform.position.y, bh.transform.position.z);
        enable.SetActive(true);
    }

    void disableJuego()
    {
        BotonHome bh = GameObject.Find("Home").GetComponent<BotonHome>();
        bh.transform.position = new Vector3(7.5f, bh.transform.position.y, bh.transform.position.z);
        enable.SetActive(false);
    }

    void asignarPartesBotonoes()
    {
        List<int> bi = new List<int>(new int[] { 0, 1, 2, 3, 4, 5 });
        int k;
        for(int i = 0; i < 2; i++)
        {
            for(int j=0; j<3; j++)
            {
                k = Random.Range(0, bi.Count); 
                botones[bi[k]].asignarParte(i, j, texturas_botones[i][j]);
                bi.RemoveAt(k);
            }
        }
    }

    public void ponerParte(int p, int e)
    {
        partes[p].texture = texturas_partes[p][e];
        emocion_parte[p] = e;

        GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>().incrementarIntentosCara(p, e);

        if (emocion_parte[0] != -1 && emocion_parte[1] != -1)
        {
            if (emocion_parte[0] == 0 && emocion_parte[1] == 0)
            {
                print("CORRECTO!! WOOOOW");
                float tiempo = 1f;
                StartCoroutine(play(gameObject, 1, tiempo));
                tiempo += getTiempoAudio(gameObject, 1) + 1f;
                StartCoroutine(play(gameObject, 0, tiempo));
                tiempo += getTiempoAudio(gameObject, 0) + 1f;
                StartCoroutine(niñaFeliz(tiempo));
                /*Invoke("disableJuego",tiempo);
                epc.Invoke("AnimacionPortal", tiempo);
                niña.GetComponent<Animator>().Play("NiñaFelizAnimation");*/
            }else
            {
                int m = (Random.Range(1, 30) % 3)+2;
                print("BREAK: " + m);
                StartCoroutine(play(gameObject, m, 0f));
                
            }
        }
    }

    public IEnumerator niñaFeliz(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        float tiempo = 0f;
        Invoke("disableJuego", tiempo);
        epc.Invoke("playIntroduccionAnimacionPortal", tiempo+0.5f);
        niña.GetComponent<Animator>().Play("NiñaFelizAnimation");
    }

    public IEnumerator play(GameObject objeto, int i, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        objeto.GetComponents<AudioSource>()[i].Play();
    }

    float getTiempoAudio(GameObject objeto, int i)
    {
        return objeto.GetComponents<AudioSource>()[i].clip.length;
    }
}
