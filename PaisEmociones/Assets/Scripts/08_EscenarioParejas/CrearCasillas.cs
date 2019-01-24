using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearCasillas : MonoBehaviour {

    public GameObject CasillaPrefab;
    public float col;
    public float fil;
    public float ancho_real;
    public float alto_real;

    public Material material;

	void Start () {
        Crear();
	}

    public void Crear()
    {
        int cont = 0;

        float x_factor = ancho_real / col;
        float x_norm = -((col / 2)/*casillas ancho*/ - (CasillaPrefab.transform.localScale[0] / 2.0f) /*mitad ancho casilla*/- 0.1f /*mitad espacio entre casillas*/ ); // medidas normalizadas (respecto a unidad)
        float x_inicio = x_norm * x_factor;

        float y_factor = alto_real / fil;
        float y_norm = ((fil / 2.0f)/*casillas ancho*/ - (CasillaPrefab.transform.localScale[1] / 2.0f) /*mitad ancho casilla*/- 0.1f /*mitad espacio entre casillas*/ ); // medidas normalizadas (respecto a unidad)
        float y_inicio = (y_norm * y_factor) + 0.26f /*para centrar*/;

        for (int i=0; i < fil; i++){
            for(int j=0; j<col; j++){
                GameObject casillaTemp = Instantiate(CasillaPrefab, new Vector3(((x_factor*j)/*+x_inicio*/), (y_factor*-i)/*+y_inicio*/, -0.5f), Quaternion.identity);
                casillaTemp.transform.localScale = new Vector3(casillaTemp.transform.localScale[0] * x_factor, casillaTemp.transform.localScale[1] * y_factor, casillaTemp.transform.localScale[2]);
                casillaTemp.transform.position = new Vector3(casillaTemp.transform.position[0] + x_inicio, casillaTemp.transform.position[1] + y_inicio, casillaTemp.transform.position[2]);
                casillaTemp.GetComponent<Casilla>().NumCasila = cont;
                cont++;
            }
        }
    }

}
