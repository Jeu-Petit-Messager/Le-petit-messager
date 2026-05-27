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

    [Header("Interaction")]
    public GameObject imageE;

    public float distanceInteraction = 15f;

    private bool interactionFaite = false;

    private bool suivreJoueur = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        imageE.SetActive(false);
        StartCoroutine(BruitAleatoire());
    }

    void Update()
    {
        // distance joueur
        float distance = Vector3.Distance(transform.position,joueur.position);

        // interaction prof
        if (!interactionFaite &&
            !XavierAffichageTextes.affichageTextesTuto &&
            distance <= distanceInteraction)
        {
            imageE.SetActive(true);

            // touche E
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactionFaite = true;

                imageE.SetActive(false);

                XavierScriptInteraction.interactionFonctionnelle = true;

                XavierScriptInteraction.nomObjetInteract = "prof";
            }
        }
        else if (!interactionFaite)
        {
            imageE.SetActive(false);
        }

        // suivre joueur après dialogue
        if (suivreJoueur)
        {
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

                // arrêter son marche
                sonMarche.Stop();
            }
        }
    }
    public void ActiverSuivi()
    {
        suivreJoueur = true;
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