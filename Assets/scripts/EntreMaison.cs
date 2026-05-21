using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EntreMaison : MonoBehaviour
{
    public Transform joueur;

    public float distanceActivation = 15f;

    public string sceneBonneFin = "sceneBonFin";

    public GameObject imageBoutonE;

    public Animator fadeAnimator;

    private bool peutEntrer;

    void Start()
    {
        if (imageBoutonE != null)
            imageBoutonE.SetActive(false);
    }

    void Update()
    {
        float distance =
            Vector3.Distance(transform.position, joueur.position);

        // joueur proche
        if (distance <= distanceActivation)
        {
            peutEntrer = true;

            if (imageBoutonE != null)
                imageBoutonE.SetActive(true);

            // appuyer E
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(BonneFin());
            }
        }
        else
        {
            peutEntrer = false;

            if (imageBoutonE != null)
                imageBoutonE.SetActive(false);
        }
    }

    IEnumerator BonneFin()
    {
        // fade
        if (fadeAnimator != null)
        {
            fadeAnimator.gameObject.SetActive(true);

            fadeAnimator.SetTrigger("FadeIn");

            yield return new WaitForSeconds(1.5f);
        }

        // charger scène
        SceneManager.LoadScene(sceneBonneFin);
    }
}