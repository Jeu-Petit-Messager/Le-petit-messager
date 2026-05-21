using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ProfChass : MonoBehaviour
{
    public Transform joueur;

    public float vitesseChasse = 9f;

    // attendre avant commencer chasse
    public float tempsAvantChasse = 5f;

    private NavMeshAgent agent;

    private bool chasseCommence;

    // public Animator animator;

    // fade
    public GameObject canvasFade;
    public Animator fadeAnimator;

    // distance attraper
    public float distanceAttrape = 2f;

    private bool joueurAttrape;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // désactiver fade au début
        if (canvasFade != null)
            canvasFade.SetActive(false);

        // le prof attend
        agent.isStopped = true;

        StartCoroutine(CommencerChasse());
    }

    IEnumerator CommencerChasse()
    {
        // attendre avant poursuite
        yield return new WaitForSeconds(tempsAvantChasse);

        chasseCommence = true;

        agent.isStopped = false;

        agent.speed = vitesseChasse;
    }

    void Update()
    {
        if (chasseCommence && !joueurAttrape)
        {
            // suivre joueur
            agent.SetDestination(joueur.position);

            // distance avec joueur
            float distance =
                Vector3.Distance(transform.position, joueur.position);

            // attraper joueur
            if (distance <= distanceAttrape)
            {
                joueurAttrape = true;

                StartCoroutine(MauvaiseFin());
            }
        }

        // animations
        // bool marche = agent.velocity.magnitude > 0.1f;

        // animator.SetBool("Marcher", marche);
        // animator.SetBool("Idle", !marche);
    }

    IEnumerator MauvaiseFin()
    {
        // arrêter prof
        agent.isStopped = true;

        // activer fade
        if (canvasFade != null)
            canvasFade.SetActive(true);

        // animation fade
        if (fadeAnimator != null)
            fadeAnimator.SetTrigger("FadeIn");

        // attendre animation
        yield return new WaitForSeconds(1.5f);

        // charger mauvaise fin
        SceneManager.LoadScene("sceneMauvaiseFin");
    }
}