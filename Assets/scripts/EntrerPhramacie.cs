using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EntrerPhramacie : MonoBehaviour
{
    public Transform joueur;

    public GameObject imageBoutonE;

    public float distanceActivation = 10f;

    public Animator fadeAnimator;

    public string nomScene = "scenePharmacie";

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