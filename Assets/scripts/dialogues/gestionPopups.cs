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

    public bool estEnTrainDEcrire;

    // Variables de conditions
    Vector3 sourisPos;
    Vector3 maintientPos;
    public float compteAccroupi;

    void Start()
    {
        dialogueText.gameObject.SetActive(true);
        indexListeDiag = 0;


        dialogueBox.SetActive(false);
        dialogueText.text = "";

        StartCoroutine(LancerDialogue());
        estEnTrainDEcrire=true;

        compteAccroupi = 0f;
    }
    // Coroutine pour lancer le dialogue après un delay, puis ecrire le texte lettre par lettre
    IEnumerator LancerDialogue()
    {
        estEnTrainDEcrire = true;

        yield return new WaitForSeconds(delayAvantAffichage);

        dialogueBox.SetActive(true);

        animator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        StartCoroutine(EcrireTexte(listeDiag[indexListeDiag]));
    }
    // Coroutine pour écrire le texte lettre par lettre
    IEnumerator EcrireTexte(string msg)
    {
        dialogueText.text = "";

        foreach (char lettre in msg)
        {
            dialogueText.text += lettre;
            yield return new WaitForSeconds(vitesseEcriture);
        }

    }

    void Update()
    {

            if (estEnTrainDEcrire)
            {
                if(indexListeDiag == 1)
                {
                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        compteAccroupi++;
                        ChargerTexteEntierEarly();

                    } 
                }

                if (Input.GetMouseButtonDown(0))
                {
                    ChargerTexteEntierEarly();
                }

                if(dialogueText.text == listeDiag[indexListeDiag]) estEnTrainDEcrire = false;

            }
            
            /* Lorsque le texte a fini d'ecrire, voici des conditions de texte de tuto */
            else
            {
                // Au debut, faire la verif deplacement cam
                if(indexListeDiag == 0)
                {

                    // 0 correspond au clic gauche
                    if (Input.GetMouseButtonDown(0))
                    {
                        maintientPos = Input.mousePosition;
                        if(maintientPos != sourisPos)
                        {
                            StartCoroutine(FermerEtLancerMessageAuto());
                            indexListeDiag++;
                        }

                    }
                    else
                    {
                        sourisPos = Input.mousePosition;
                    }

                }

                else if (indexListeDiag == 1)
                {

                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        if (compteAccroupi < 2f) compteAccroupi++;
                    }

                    if (compteAccroupi == 2f)
                    {
                        StartCoroutine(FermerEtLancerMessageAuto());
                        indexListeDiag++;
                    }
                }
                else
                {
                    if (Input.GetMouseButtonDown(0) && dialogueText.text != "")
                    {
                        StartCoroutine(FermerEtLancerMessageAuto());
                        if(indexListeDiag < listeDiag.Count) indexListeDiag++;
                    }
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

        if (indexListeDiag < listeDiag.Count)
        {
            StartCoroutine(LancerDialogue());
        }
    }

    void ChargerTexteEntierEarly()
    {
        StopAllCoroutines();
        dialogueText.text = listeDiag[indexListeDiag];
        estEnTrainDEcrire = false;
    }
}