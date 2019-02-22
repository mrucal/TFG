using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoJuego : MonoBehaviour
{
    public static EstadoJuego ej;
    public Datos datos;

    private void Awake()
    {
        if (ej == null)
        {
            ej = this;
            DontDestroyOnLoad(gameObject);
            datos.dificultad = 0;
            datos.ultima_escena = "01_EscenaParque";
        }
        else if (ej != this)
            Destroy(gameObject);
    }

    public void guardar()
    {
        /*print("BREAK GUARDAR " + datos.fallos[0][2]);
        datos.aciertosd = datos.aciertos[datos.dificultad];
        datos.fallosd = datos.fallos[datos.dificultad];*/
        string estadojson = JsonUtility.ToJson(datos);
        print("JSON GUARDAR: " + estadojson);
        PlayerPrefs.SetString("EstadoJuego", estadojson);
    }

    public void cargar()
    {
        string estadojson = PlayerPrefs.GetString("EstadoJuego");
        print("JSON CARGAR: " + estadojson);
        if (!string.IsNullOrEmpty(estadojson))
        {
            datos = JsonUtility.FromJson<Datos>(estadojson);
            /*datos.aciertosd = datos.aciertos[datos.dificultad];
            datos.fallosd = datos.fallos[datos.dificultad];*/

        }
    }

    public void reset()
    {
        datos.dificultad = 0;
        datos.ultima_escena = null;
        datos.aciertos = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        datos.fallos = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        guardar();
    }

    public int getAciertos(int emocion)
    {
        return datos.aciertos[(datos.dificultad *3)+emocion];
    }

    public void incrementarAciertos(int emocion)
    {
        datos.aciertos[(datos.dificultad * 3) + emocion]++;
    }

    public int getFallos(int emocion)
    {
        return datos.fallos[(datos.dificultad * 3) + emocion];
    }

    public void incrementarFallos(int emocion)
    {
        datos.fallos[(datos.dificultad * 3) + emocion]++;
    }
}

[System.Serializable]
public class Datos
{
    public int dificultad;

    public string ultima_escena;

    public int[] total = { 1, 1, 1 };

    public int[] aciertos = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public int[] fallos = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    /*public int[][] aciertos = new int[][] { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };
    public int[][] fallos = new int[][] { new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 }, new int[] { 0, 0, 0 } };*/

}
