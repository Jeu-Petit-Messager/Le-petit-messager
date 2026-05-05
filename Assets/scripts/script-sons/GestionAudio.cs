using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionAudio: MonoBehaviour
{
    private string sceneActuel;
    public AudioSource musiqueFond;
    public AudioSource sonvent;

    // Update is called once per frame
    void Start()
    {
        sceneActuel = SceneManager.GetActiveScene().name;

        if (sceneActuel == "sceneJeu")
        {  
            musiqueFond.Play();
            sonvent.Play();
        }
        else
        {
            musiqueFond.Stop();
            sonvent.Stop();
        }
    }
}
