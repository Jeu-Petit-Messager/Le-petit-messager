using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/* Script se chargeant du nouveau message de tutoriel */
public class DialogueIntro : MonoBehaviour
{

    public GameObject dialogueBox;
    public Text dialogueText;
    public Animator animator;

    // Liste dynamique de textes
    public List<string> listeDiag = new List<string>();
    public int indexListeDiag;


    public float delayAvantAffichage = 0f;
    public float vitesseEcriture = 0.05f;

    private bool estEnTrainDEcrire = false;

    public bool messageAutoDisparition;

    public bool deuxiemeMessage = false;
    public bool troisiemeMessage;

    void Start()
    {
        indexListeDiag = 0;

        messageAutoDisparition = false;

        troisiemeMessage = false;

        dialogueBox.SetActive(false);
        dialogueText.text = "";

        StartCoroutine(LancerDialogue());
    }
    // Coroutine pour lancer le dialogue après un delay, puis ecrire le texte lettre par lettre
    IEnumerator LancerDialogue()
    {
        yield return new WaitForSeconds(delayAvantAffichage);

        dialogueBox.SetActive(true);

        animator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        StartCoroutine(EcrireTexte(messageCamera));
    }
    // Coroutine pour écrire le texte lettre par lettre
    IEnumerator EcrireTexte(string msg)
    {
        estEnTrainDEcrire = true;
        dialogueText.text = "";

        foreach (char lettre in msg)
        {
            dialogueText.text += lettre;
            yield return new WaitForSeconds(vitesseEcriture);
        }

        estEnTrainDEcrire = false;

        // MESSAGES AUTO → fade direct apres 5 sec
        if (messageAutoDisparition)
        {
            messageAutoDisparition = false;

            yield return new WaitForSeconds(3f);

            animator.SetTrigger("FadeOut");

            yield return new WaitForSeconds(1.0f);

            dialogueBox.SetActive(false);
            dialogueText.text = "";
            StartCoroutine(FermerEtLancerMessageAuto());
        }

    }

    void Update()
    {
        if (troisiemeMessage) return;
        if (deuxiemeMessage) return;

        if (dialogueBox.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (estEnTrainDEcrire)
            {
                StopAllCoroutines();
                dialogueText.text = messageCamera;
                estEnTrainDEcrire = false;
            }
            else
            {
                StartCoroutine(FermerEtLancerMessageAuto());
            }

        }
    }
    // Coroutine pour fermer le dialogue et lancer l'autre message
    IEnumerator FermerEtLancerMessageAuto()
    {
        animator.SetTrigger("FadeOut");

        // Vitesse effacer texte
        yield return new WaitForSeconds(1.0f);

        dialogueBox.SetActive(false);
        dialogueText.text = "";

        // Lancer le deuxieme message après un delai
        if (!deuxiemeMessage)
        {
            deuxiemeMessage = true;

            messageAutoDisparition = true;

            yield return new WaitForSeconds(2f);

            dialogueBox.SetActive(true);

            animator.SetTrigger("FadeIn");

            yield return new WaitForSeconds(1.0f);

            StartCoroutine(EcrireTexte(messageCanInit));

            yield break;

        } else if(!troisiemeMessage)
        {
            troisiemeMessage = true;

            messageAutoDisparition = true;

            yield return new WaitForSeconds(2f);

            dialogueBox.SetActive(true);

            animator.SetTrigger("FadeIn");

            yield return new WaitForSeconds(1f);

            StartCoroutine(EcrireTexte(messageCanInit));

            yield break;
        }

    }
}