using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/* Script se chargeant du nouveau message de tutoriel */
public class XavierAffichageTextes : MonoBehaviour
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

    /* Variables de conditions du tutoriel*/
    Vector3 sourisPos;
    Vector3 maintientPos;
    float compteAccroupi;

    // "Appuyez sur E pour interagir avec des objets et des personnes. Essayez de prendre une cannette pour voir",

    /* Stockage de listes : */
    // Liste du tutoriel
    List<string> consignesTuto = new List<string> {
        "Appuyez sur CTRL pour vous accroupir,\net appuyez de nouveau pour vous redresser",
        "Enfoncez la barre d'espace pour sauter par dessus des obstacles",
        "Appuyez sur E pour interagir avec des objets ou des personnes!",
        "Maintenez le bouton SHIFT pour courir",
        "Maintenant, essayez de placez votre cannette devant votre maison",
        "Il y a 6 cannettes qui ne font que vous attendre. Bonne chance!"
    };

    // Liste 2
    List<string> diagProf1 = new List<string> {
        "Qu’est ce qui ne va pas? Petit...as-tu besoin de mon aide?",
        "Je suis à la recherche d'un monsieur...\nmais ma maman m’a dit de ne pas parler aux monsieurs bizarres...",
        "Attends...je suis qu’un gentil homme je t’assure..! Tu sais je suis...ou du moins j’étais...un grand professeur avant, et je passais mes journées à aider des petits garçons tout comme toi, alors...n’hésite pas à tout me dire.",
        "... Bon d’accord. Ma mère vit toute seule et elle m’a demandé d’aller donner ce bout de papier vite vite à un grand monsieur tout blanc avant la nuit...mais le problème est que je ne me rappelle plus de lui... ",
        "Mhmmm...je pense bien voir de qui tu parles, et je vais de ce pas te le révéler mais à une seule petite condition...",
        "Je suis très malchanceux en ce moment...et à force d’aider les gens partout je me retrouve sans endroit pour dormir ce soir. Et toi et ta mère...vous dormez bien chaque soir...non ? Vous pourriez surement accueillir un pauvre professeur sans emploi comme moi, non ?",
        "Pour te prouver ma bienveillance voici un petit indice sur son identité : Il possède un magasin important et tu le trouveras probablement derrière son comptoir."
    };

    /* Les differentes listes */
    // Variable indiquant la fin de cette partie
    public static bool affichageTextesTuto;

    // Retire temporairement la possibilite d'interagir
    public static bool retireInteractionJoueur;

    // 

    // Statut configuration perso
    public bool typePerso;

    void Start()
    {
        typePerso = false;

        dialogueText.gameObject.SetActive(true);
        dialogueBox.SetActive(false);
        dialogueText.text = "";

        // Le compteur de dialogue commence a zero
        indexListeDiag = 0;

        // Le joueur inititie le tutoriel des le debut
        affichageTextesTuto = false;
        retireInteractionJoueur = false;

        StartCoroutine(LancerDialogue());
        estEnTrainDEcrire = true;

        compteAccroupi = 0f;

        listeDiag.Clear();
        listeDiag.AddRange(diagProf1);

        dialogueText.alignment = TextAnchor.MiddleLeft;
    }

    /* Coroutine pour lancer le dialogue après un delay, puis ecrire le texte lettre par lettre */
    IEnumerator LancerDialogue()
    {
        // le texte charge
        estEnTrainDEcrire = true;

        // Animation apparition textbox
        yield return new WaitForSeconds(delayAvantAffichage);
        dialogueBox.SetActive(true);
        animator.SetTrigger("FadeIn");
        yield return new WaitForSeconds(1f);

        // Ecrire le texte
        StartCoroutine(EcrireTexte(listeDiag[indexListeDiag]));
    }

    /* Coroutine pour ecrire le texte lettre par lettre */
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

                /* Conditions speciales du tuto */
                if(affichageTextesTuto)
                {
                    // 0. Test camera
                    //if (indexListeDiag == 0)
                    //{
                    //    if (Input.GetMouseButtonDown(0))
                    //    {
                    //        maintientPos = Input.mousePosition;

                    //        if (maintientPos != sourisPos)
                    //        {
                    //            ChargerTexteEntierEarly();
                    //            StartCoroutine(FermerEtLancerMessageAuto());
                    //            indexListeDiag++;
                    //        }

                    //    }
                    //    else sourisPos = Input.mousePosition;

                    //}

                    // 1. Test accroupissement
                    if (indexListeDiag == 0)
                    {
                        if (Input.GetKeyDown(KeyCode.LeftControl))
                        {
                            compteAccroupi++;
                            ChargerTexteEntierEarly();

                        }
                    }

                    // 2. Test saut
                    else if (indexListeDiag == 1)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            ChargerTexteEntierEarly();
                            StartCoroutine(FermerEtLancerMessageAuto());
                            indexListeDiag++;
                            // Le joueur peut interagir pour le prochain test
                            retireInteractionJoueur = false;
                    }
                    }

                    // 3. Test interact
                    else if (indexListeDiag == 2)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            ChargerTexteEntierEarly();
                        }

                        if(XavierScriptInteraction.interactionFonctionnelle)
                        {
                            StartCoroutine(FermerEtLancerMessageAuto());
                            indexListeDiag++;

                            // Le joueur peut plus interagir pour le prochain test
                            retireInteractionJoueur = true;
                    }

                    }

                    // 4. Test course
                    else if (indexListeDiag == 3)
                    {
                            if (Input.GetKeyDown(KeyCode.LeftShift))
                            {
                                ChargerTexteEntierEarly();
                            }
                    }

                    if (indexListeDiag != 4 && indexListeDiag != 5)
                    {
                        /* Condition generale, juste clicker */
                        if (Input.GetMouseButtonDown(0))
                        {
                            ChargerTexteEntierEarly();
                        }
                    }

                }

                else
                {
                    /* Condition generale, juste clicker */
                    if (Input.GetMouseButtonDown(0))
                    {
                        ChargerTexteEntierEarly();
                    }
                }

                // Le texte finit d'ecrire lorsqu'il devient pareil a la valeur de la liste
                if(dialogueText.text == listeDiag[indexListeDiag]) estEnTrainDEcrire = false;

            }
            
            /* Lorsque le texte a fini d'ecrire, voici des conditions des textes */
            else
            {

                // Pour le texte du tutoriel
                if(affichageTextesTuto)
                {

                    // 1. verif de l'accroupissement
                    if (indexListeDiag == 0)
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

                    // 2. verif du saut
                    else if (indexListeDiag == 1)
                    {
                        // Le joueur doit appuyez ESPACE
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            StartCoroutine(FermerEtLancerMessageAuto());
                            indexListeDiag++;
                            // Le joueur peut interagir pour le prochain test
                            retireInteractionJoueur = false;
                        }
                    }

                    // 3. Test interact
                    else if (indexListeDiag == 2)
                    {
                        if (XavierScriptInteraction.interactionFonctionnelle)
                        {
                            StartCoroutine(FermerEtLancerMessageAuto());
                            indexListeDiag++;

                            // Le joueur peut plus interagir pour le prochain test
                            retireInteractionJoueur = true;
                        }
                    }

                    // 4. verif du sprint
                    else if (indexListeDiag == 3)
                    {
                        // Le joueur doit appuyez sur SHIFT
                        if (Input.GetKeyUp(KeyCode.LeftShift))
                        {
                            StartCoroutine(FermerEtLancerMessageAuto());
                            indexListeDiag++;

                            // Le joueur peut interagir
                            retireInteractionJoueur = false;
                    }
                    }

                    else if(indexListeDiag == 4 || indexListeDiag == 5)
                    {
                        if (dialogueText.text == listeDiag[indexListeDiag])
                        {
                            StartCoroutine(FermerEtLancerMessageAuto());
                            if (indexListeDiag < listeDiag.Count) indexListeDiag++;
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

                /* Sinon, le clic est suffisant pour passer du texte de dialogue */
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

        /* Valider toute interaction du joueur */
        if (XavierScriptInteraction.interactionFonctionnelle)
        {
            XavierScriptInteraction.interactionFonctionnelle = false;
        }
    }
    // Coroutine pour fermer le dialogue et lancer l'autre message
    IEnumerator FermerEtLancerMessageAuto()
    {

        animator.SetTrigger("FadeOut");

        // Vitesse effacer texte
        yield return new WaitForSeconds(1f);

        dialogueBox.SetActive(false);
        dialogueText.text = "";

        if (indexListeDiag < listeDiag.Count)
        {
            StartCoroutine(LancerDialogue());
        }
        // Lorsque l'affichage atteint sa fin, le statut de tuto prend fin
        else if(affichageTextesTuto == true)
        {
            affichageTextesTuto = false;
            indexListeDiag = 0;
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