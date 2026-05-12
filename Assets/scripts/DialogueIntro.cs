using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueIntro : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public Animator animator;

    [TextArea]
    public string message = "Vous pouvez cliquer droit la souris pour voir autour !";

    [TextArea]
    public string message2 = "Veuillez placer les 6 canettes de nourriture dans la zone indiquée";

    public float delayAvantAffichage = 5f;
    public float vitesseEcriture = 0.05f;

    private bool estEnTrainDEcrire = false;
    private bool deuxiemeMessage = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        dialogueText.text = "";

         if (SceneManager.GetActiveScene().name == "sceneJeuJour" || SceneManager.GetActiveScene().name == "sceneJeuNuit")
        {
            StartCoroutine(LancerDialogue());
        }
    }
    // Coroutine pour lancer le dialogue après un délai, puis écrire le texte lettre par lettre
    IEnumerator LancerDialogue()
    {
        yield return new WaitForSeconds(delayAvantAffichage);

        dialogueBox.SetActive(true);

        animator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        StartCoroutine(EcrireTexte(message));
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

        // MESSAGE 2 → fade direct après 5 sec
        if (deuxiemeMessage)
        {
            yield return new WaitForSeconds(5f);

            animator.SetTrigger("FadeOut");

            yield return new WaitForSeconds(0.5f);

            dialogueBox.SetActive(false);
            dialogueText.text = "";
        }
    }

    void Update()
    {
        if (deuxiemeMessage) return;
        if (dialogueBox.activeSelf && Input.GetMouseButtonDown(1))
        {
            if (estEnTrainDEcrire)
            {
                StopAllCoroutines();
                dialogueText.text = message;
                estEnTrainDEcrire = false;
            }
            else if (!estEnTrainDEcrire)
            {
                StartCoroutine(FermerEtLancerMessage2());
            }
        }
    }
    // Coroutine pour fermer le dialogue et lancer le deuxième message
    IEnumerator FermerEtLancerMessage2()
    {
        animator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(0.5f);

        dialogueBox.SetActive(false);
        dialogueText.text = "";

        // Lancer le deuxième message après un délai
        if (!deuxiemeMessage)
        {
            deuxiemeMessage = true;

            yield return new WaitForSeconds(2f);

            dialogueBox.SetActive(true);

            animator.SetTrigger("FadeIn");

            yield return new WaitForSeconds(1f);

            StartCoroutine(EcrireTexte(message2));
        }
    }
}