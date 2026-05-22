using UnityEngine;
using UnityEngine.AI;

public class ProfSuivre : MonoBehaviour
{
    public Transform joueur;

    public float distanceMinimum = 10f;

    private NavMeshAgent agent;

    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        }
        else
        {
            // arrêter
            agent.ResetPath();

            animator.SetBool("Courir", false);
        }
    }
}