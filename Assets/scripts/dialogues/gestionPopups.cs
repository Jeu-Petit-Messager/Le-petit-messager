using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/* Script se chargeant du nouveau message de tutoriel */
public class gestionPopups : MonoBehaviour
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

    //public bool messageAutoDisparition;

    void Start()
    {
        indexListeDiag = 0;

        //messageAutoDisparition = false;

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

        StartCoroutine(EcrireTexte(listeDiag[indexListeDiag]));
    }
    // Coroutine pour écrire le texte lettre par lettre
    IEnumerator EcrireTexte(string msg)
    {
        print(indexListeDiag);

        estEnTrainDEcrire = true;
        dialogueText.text = "";

        foreach (char lettre in msg)
        {
            dialogueText.text += lettre;
            yield return new WaitForSeconds(vitesseEcriture);
        }

        estEnTrainDEcrire = false;
        //StartCoroutine(FermerEtLancerMessageAuto());

    }

    void Update()
    {

        if (dialogueBox.activeSelf && Input.GetMouseButtonDown(0))
        {
            if (estEnTrainDEcrire)
            {
                StopAllCoroutines();
                dialogueText.text = listeDiag[indexListeDiag];
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
        yield return new WaitForSeconds(0.5f);

        dialogueBox.SetActive(false);
        dialogueText.text = "";

        indexListeDiag++;
        print(indexListeDiag);

        if (indexListeDiag < listeDiag.Count)
            StartCoroutine(LancerDialogue());
    }
}