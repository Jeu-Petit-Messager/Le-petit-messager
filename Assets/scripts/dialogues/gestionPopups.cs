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

    // Statut determinantlorsque le texte est en ecriture
    public bool estEnTrainDEcrire;
    
    /* Variables de conditions */
    // Enregistrer positions souris
    Vector3 sourisPos;
    Vector3 maintientPos;
    public float compteAccroupi;

    void Start()
    {
        dialogueText.gameObject.SetActive(true);
        dialogueBox.SetActive(false);
        dialogueText.text = "";

        // Le compteur de dialogue commence a zero
        indexListeDiag = 0;


        StartCoroutine(LancerDialogue());
        estEnTrainDEcrire = true;

        compteAccroupi = 0f;
    }

    /* Coroutine pour lancer le dialogue après un delay, puis ecrire le texte lettre par lettre */
    IEnumerator LancerDialogue()
    {
        estEnTrainDEcrire = true;

        yield return new WaitForSeconds(delayAvantAffichage);

        dialogueBox.SetActive(true);

        animator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        StartCoroutine(EcrireTexte(listeDiag[indexListeDiag]));
    }

    /* Coroutine pour écrire le texte lettre par lettre */
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
            /* Determiner conditions pour skip dialogue */
            if (estEnTrainDEcrire)
            {
                // 1. Test camera
                if (indexListeDiag == 0)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        maintientPos = Input.mousePosition;

                        if (maintientPos != sourisPos)
                        {
                            ChargerTexteEntierEarly();
                            StartCoroutine(FermerEtLancerMessageAuto());
                            indexListeDiag++;
                        }

                    }
                    else sourisPos = Input.mousePosition;

                }

                // 2. Test accroupissement
                else if (indexListeDiag == 1)
                {
                    if (Input.GetKeyDown(KeyCode.LeftControl))
                    {
                        compteAccroupi++;
                        ChargerTexteEntierEarly();

                    } 
                }

                // 3. Test saut
                else if (indexListeDiag == 2)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ChargerTexteEntierEarly();
                        StartCoroutine(FermerEtLancerMessageAuto());
                        indexListeDiag++;
                    }
                }

                // 5. Test course
                else if(indexListeDiag == 4)
                {
                    if (Input.GetKeyDown(KeyCode.LeftShift))
                    {
                        ChargerTexteEntierEarly();
                    }
                }

                /* Condition generale, juste clicker */
                if (Input.GetMouseButtonDown(0))
                {
                    ChargerTexteEntierEarly();
                }

                // Le texte finit d'ecrire lorsqu'il devient pareil a la valeur de la liste
                if(dialogueText.text == listeDiag[indexListeDiag]) estEnTrainDEcrire = false;

            }
            
            /* Lorsque le texte a fini d'ecrire, voici des conditions de texte de tuto */
            else
            {
            // 1. faire la verif deplacement cam
            if (indexListeDiag == 0)
            {

                // 0 correspond au clic gauche
                if (Input.GetMouseButtonDown(0))
                {
                    // Le maintient est seulement calcule au hold
                    maintientPos = Input.mousePosition;

                    // Lorsque la valeur de maintient change de la derniere enregistree, passe au suivant
                    if (maintientPos != sourisPos)
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

            // 2. verif de l'accroupissement
            else if (indexListeDiag == 1)
            {
                // On veut que le joueur s'accroupit et se redresse
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    if (compteAccroupi < 2f) compteAccroupi++;
                }

                // Avec 2 clics, le joueur passe au suivant
                if (compteAccroupi == 2f)
                {
                    StartCoroutine(FermerEtLancerMessageAuto());
                    indexListeDiag++;
                }
            }

            // 3. verif du saut
            else if (indexListeDiag == 2)
            {
                // Le joueur doit appuyez ESPACE
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(FermerEtLancerMessageAuto());
                    indexListeDiag++;
                }
            }

            // 5. verif du sprint
            else if (indexListeDiag == 4)
            {
                // Le joueur doit appuyez sur SHIFT
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    StartCoroutine(FermerEtLancerMessageAuto());
                    indexListeDiag++;
                }
            }

            /* Dans tout autre cas, le clic est suffisant pour passer du texte de dialogue */
            else
            {
                // Lorsque le le joueur clic apres que le dialogue n'est pas nul
                if (Input.GetMouseButtonDown(0) && dialogueText.text != "")
                {
                    // Le message se ferme, et l'index augmente s'il y a un autre texte
                    StartCoroutine(FermerEtLancerMessageAuto());
                    if (indexListeDiag < listeDiag.Count) indexListeDiag++;
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

    /* Fonction pour charger l'entierete d'un texte apres un clic */
    void ChargerTexteEntierEarly()
    {
        StopAllCoroutines();
        dialogueText.text = listeDiag[indexListeDiag];
        estEnTrainDEcrire = false;
    }
}