using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
 
public class QuitterPharmacie : MonoBehaviour
{
    public Transform joueur;

    public GameObject imageBoutonE;

    public float distanceActivation = 10f;

    public Animator fadeAnimator;

    public string nomScene = "sceneJeuNuit";

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
                StartCoroutine(QuitterLePharmacie());
            }
        }
        else
        {
            imageBoutonE.SetActive(false);
        }
    }
    IEnumerator QuitterLePharmacie()
    {
        // activer canvas fade
        fadeAnimator.gameObject.SetActive(true);

        // lancer animation
        fadeAnimator.SetTrigger("FadeIn");

        // attendre fade
        yield return new WaitForSeconds(1.5f);

        // sauvegarder sceneJeuNuit
        SaveJeu.SauvegarderScene();

        // changer scène
        SceneManager.LoadScene(nomScene);
    }
}