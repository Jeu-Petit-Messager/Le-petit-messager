using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NpcMarche : MonoBehaviour
{
    public Transform[] points;
    private int index;

    private NavMeshAgent agent;
    public Animator animator;

    // attendre avant repartir
    private bool attendre;

    // temps idle random
    public float tempsMinIdle = 2f;
    public float tempsMaxIdle = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        AllerPointAleatoire();
    }

    void Update()
    {
        // animation marche / idle
        bool marche = agent.velocity.magnitude > 0.1f;

        animator.SetBool("Marcher", marche);
        animator.SetBool("Idle", !marche);

        // si arrivé au point
        if (!attendre &&
            !agent.pathPending &&
            agent.remainingDistance < 0.5f)
        {
            StartCoroutine(AttendreEtRepartir());
        }
    }

    IEnumerator AttendreEtRepartir()
    {
        attendre = true;

        // stop NPC
        agent.isStopped = true;

        // temps random idle
        float tempsIdle = Random.Range(tempsMinIdle, tempsMaxIdle);

        yield return new WaitForSeconds(tempsIdle);

        // repartir
        agent.isStopped = false;

        AllerPointAleatoire();

        attendre = false;
    }

    void AllerPoint()
    {
        agent.SetDestination(points[index].position);
    }

    void AllerPointAleatoire()
    {
        index = Random.Range(0, points.Length);

        agent.SetDestination(points[index].position);
    }
}

/** 
using UnityEngine;
using UnityEngine.AI;

public class NpcMarche : MonoBehaviour
{
    public Transform[] points;
    private int index;

    private NavMeshAgent agent;
    public Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        AllerPoint();
    }

    void Update()
    {
        // 🎯 animation marche / idle
        bool marche = agent.velocity.magnitude > 0.1f;
        animator.SetBool("Marcher", marche);
        animator.SetBool("Idle", !marche);

        // 🎯 si arrivé au point
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            AllerPointAleatoire();
        }
    }

    void AllerPoint()
    {
        agent.SetDestination(points[index].position);
    }

    void AllerPointAleatoire()
    {
        index = Random.Range(0, points.Length);
        agent.SetDestination(points[index].position);
    }
}**/