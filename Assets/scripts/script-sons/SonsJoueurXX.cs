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

    public AudioClip sonMarche;
    public AudioClip sonCourse;

    private void Start()
    {
        sonMarcheEnCours = false;
        sonMarcheBoucle.clip = sonMarche; // On assigne le clip de marche à l'AudioSource
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            sonMarcheBoucle.clip = sonCourse; // On assigne le clip de course à l'AudioSource
            sonMarcheBoucle.Stop(); // On arrete le son
            sonMarcheBoucle.Play(); // On lance le son de course
        }

        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            sonMarcheBoucle.clip = sonMarche; // On assigne le clip de marche à l'AudioSource
            sonMarcheBoucle.Stop(); // On arrete le son
            sonMarcheBoucle.Play(); // On lance le son de marche

        }

        if (!gameObjectJoueur.GetComponent<controlePerso>().boolSaut)
           
        {
            // Joueur avance et recule
            if ((Input.GetKey(KeyCode.W))
                    || (Input.GetKey(KeyCode.S)))
            {
                if (!sonMarcheEnCours)
                {
                    //sonMarcheBoucle.gameObject.SetActive(true);
                    sonMarcheEnCours = true;
                    sonMarcheBoucle.loop = true; // On active la boucle
                    sonMarcheBoucle.Play();      // On lance la lecture

                }
            }
            else
            {
                sonMarcheEnCours = false;
                sonMarcheBoucle.Stop(); // On arrête le son
                sonMarcheBoucle.loop = false; // Par sécurité, on coupe la boucle
            }
        }
        else
        {
            sonMarcheEnCours = false;
            sonMarcheBoucle.Stop(); // On arrête le son
            sonMarcheBoucle.loop = false; // Par sécurité, on coupe la boucle
        }

    }
}
