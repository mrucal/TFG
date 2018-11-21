using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControler : MonoBehaviour
{
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Soleado(GameObject sol)
    {
        sol.GetComponent<Animator>().Play("SolAnimation");
    }

    void Nublado(GameObject sol)
    {
        sol.GetComponent<Animator>().Play("SolNubeAnimation");
    }

    public IEnumerator SiguienteEscena(string escena, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(escena);
    }
}
