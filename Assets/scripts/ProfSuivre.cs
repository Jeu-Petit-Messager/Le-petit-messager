using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ProfSuivre : MonoBehaviour
{
    public Transform joueur;

    public float distanceMinimum = 10f;

    private NavMeshAgent agent;

    public Animator animator;

    [Header("Audio")]
    public AudioSource sonMarche;
    public AudioSource sonBizarre;

    public AudioClip[] sonsBizarres;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(BruitAleatoire());
    }

    void Update()
    {
        float distance =
            Vector3.Distance(transform.position, joueur.position);

        // suivre joueur
        if (distance > distanceMinimum)
        {
            agent.SetDestination(joueur.position);

            animator.SetBool("Courir", true);

            // son marche
            if (!sonMarche.isPlaying)
            {
                sonMarche.Play();
            }
        }
        else
        {
            // arrêter
            agent.ResetPath();

            animator.SetBool("Courir", false);
            // Arrêter son marche
            sonMarche.Stop();
        }
    }

     IEnumerator BruitAleatoire()
    {
        while (true)
        {
            // attendre temps aléatoire
            float attente =
                Random.Range(10f, 25f);

            yield return new WaitForSeconds(attente);

            // jouer son aléatoire
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
}