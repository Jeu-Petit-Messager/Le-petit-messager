using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering;

public class EntrerPhramacie : MonoBehaviour
{
    public Transform joueur;

    public GameObject imageBoutonE;

    public float distanceActivation = 15f;

    public Animator fadeAnimator;

    public string nomScene = "scenePharmacie";
    public Volume globalVolume;

    void Start()
    {
        imageBoutonE.SetActive(false);
    }

    void Update()
    {
        // distance
        float distance =
            Vector3.Distance(transform.position, joueur.position);

        // afficher image
        if (distance <= distanceActivation)
        {
            imageBoutonE.SetActive(true);

            // touche E
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(EntrerPharmacie());
            }
        }
        else
        {
            imageBoutonE.SetActive(false);
        }
    }

    IEnumerator EntrerPharmacie()
    {
        // vérifier si nuit commencée
        if(globalVolume != null && globalVolume.weight <= 0f)
        {
            // bonne fin possible
            PlayerPrefs.SetInt("BonFin", 1);
        }
        else
        {
            // mauvaise fin
            PlayerPrefs.SetInt("BonFin", 0);
        }

        PlayerPrefs.Save();
        // activer canvas fade
        fadeAnimator.gameObject.SetActive(true);

        // lancer animation
        fadeAnimator.SetTrigger("FadeIn");

        // attendre fade
        yield return new WaitForSeconds(1.5f);

        // changer scène
        SceneManager.LoadScene(nomScene);
    }
}