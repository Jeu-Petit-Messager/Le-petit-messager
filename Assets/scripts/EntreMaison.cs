using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EntreMaison : MonoBehaviour
{
    public Transform joueur;

    public float distanceActivation = 15f;

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
                PlayerPrefs.DeleteKey("SauvegardeScene");
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

        // vérifier résultat pharmacie
        if(PlayerPrefs.GetInt("BonFin") == 1)
        {
            // bonne fin
            SceneManager.LoadScene("sceneBonFin");
        }
        else
        {
            // mauvaise fin
            SceneManager.LoadScene("sceneMauvaiseFin");
        }
    }
}