using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueIntro : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public Animator animator; // l'animator

    [TextArea]
    public string message = "Vous pouvez cliquer la souris pour voir autour !";

    public float delayAvantAffichage = 5f;
    public float vitesseEcriture = 0.05f;

    private bool texteFini = false;
    private bool estEnTrainDEcrire = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.text = "";

        StartCoroutine(LancerDialogue());
    }

    IEnumerator LancerDialogue()
    {
        yield return new WaitForSeconds(delayAvantAffichage);

        dialogueBox.SetActive(true);

        // Fade IN
        animator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(0.5f); // temps de l'animation

        StartCoroutine(EcrireTexte());
    }

    IEnumerator EcrireTexte()
    {
        estEnTrainDEcrire = true;
        dialogueText.text = "";

        foreach (char lettre in message)
        {
            dialogueText.text += lettre;
            yield return new WaitForSeconds(vitesseEcriture);
        }

        estEnTrainDEcrire = false;
        texteFini = true;
    }

    void Update()
    {
        if (dialogueBox.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (estEnTrainDEcrire)
            {
                StopAllCoroutines();
                dialogueText.text = message;
                estEnTrainDEcrire = false;
                texteFini = true;
            }
            else if (texteFini)
            {
                StartCoroutine(FermerDialogue());
            }
        }
    }

    IEnumerator FermerDialogue()
    {
        // Fade OUT
        animator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(0.5f); // durée animation

        dialogueBox.SetActive(false);
        dialogueText.text = "";
    }
}