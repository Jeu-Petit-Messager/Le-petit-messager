using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SonsJoueurXX : MonoBehaviour
{
    [Header("Sons lies au gars")]
    public GameObject gameObjectJoueur;
    public AudioSource sonMarcheBoucle;
    public bool sonMarcheEnCours;

    public AudioClip audioTest;

    private void Start()
    {
        sonMarcheEnCours = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObjectJoueur.GetComponent<controlePerso>().auSol)
        {
            
            // Joueur avance et recule
            if ((Input.GetKey(KeyCode.W))
                || (Input.GetKey(KeyCode.S)))
            {
                if(!sonMarcheEnCours)
                {
                    sonMarcheBoucle.gameObject.SetActive(true);
                    sonMarcheBoucle.loop = true; // On active la boucle
                    sonMarcheBoucle.Play();      // On lance la lecture

                }
            }
            else
            {
                sonMarcheEnCours = false;
                sonMarcheBoucle.Stop(); // On arrête le son
                sonMarcheBoucle.loop = false; // Par sécurité, on coupe la boucle
                sonMarcheBoucle.gameObject.SetActive(false);
            }
        }
        else
        {
            sonMarcheEnCours = false;
            sonMarcheBoucle.Stop(); // On arrête le son
            sonMarcheBoucle.loop = false; // Par sécurité, on coupe la boucle
            sonMarcheBoucle.gameObject.SetActive(false);
        }

    }
}
