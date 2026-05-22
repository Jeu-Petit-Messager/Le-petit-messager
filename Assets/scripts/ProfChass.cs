using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ProfChass : MonoBehaviour
{
    public Transform joueur;

    public float vitesseChasse = 10f;

    // attendre avant commencer chasse
    public float tempsAvantChasse = 5f;

    private NavMeshAgent agent;

    private bool chasseCommence;

    public Animator animator;

    // fade
    public GameObject canvasFade;
    public Animator fadeAnimator;

    // distance attraper
    public float distanceAttrape = 2f;

    private bool joueurAttrape;

     [Header("Audio")]
    public AudioSource sonCourse;

    public AudioSource sonBizarre;

    public AudioClip[] sonsBizarres;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // désactiver fade au début
        if (canvasFade != null)
            canvasFade.SetActive(false);

        // le prof attend
        agent.isStopped = true;

        StartCoroutine(CommencerChasse());
        StartCoroutine(BruitAleatoire());
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
        bool courir = agent.velocity.magnitude > 0.1f;

        animator.SetBool("Courir", courir);
        // son course
        if (courir)
        {
            if (!sonCourse.isPlaying)
            {
                sonCourse.Play();
            }
        }
        else
        {
            sonCourse.Stop();
        }
    }
    IEnumerator BruitAleatoire()
    {
        while (true)
        {
            float attente =
                Random.Range(10f, 25f);

            yield return new WaitForSeconds(attente);

            if (sonsBizarres.Length > 0)
            {
                int randomSon =
                    Random.Range(0, sonsBizarres.Length);

                sonBizarre.PlayOneShot(
                    sonsBizarres[randomSon]
                );
            }
        }
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