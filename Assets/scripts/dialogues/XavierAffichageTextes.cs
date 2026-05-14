using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/* Script se chargeant du syteme de dialogues */
public class XavierAffichageTextes : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public Animator animator;

    // Liste dynamique de textes
    public List<string> listeDiag = new List<string>();
    public int indexListeDiag;

    public float delayAvantAffichage = 0f;
    public float vitesseEcriture;

    // Statut determinantlorsque le texte est en ecriture
    public bool estEnTrainDEcrire;

    /* Variables de conditions du tutoriel*/
    Vector3 sourisPos;
    Vector3 maintientPos;
    float compteAccroupi;

    /* Couleurs et fontes de texte */
    public Color couleurPNJ;
    public Font fontPNJ;
    public Color couleurGarcon;
    public Font fontGarcon;
    public Color couleurProf;
    public Font fontProf;
    public Color couleurPharma;
    public Font fontPharma;


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

    // Interaction 1 Prof
    List<string> diagProf1 = new List<string> {
        "Qu’est ce qui ne va pas? Petit...as-tu besoin de mon aide?",
        "Je suis à la recherche d'un monsieur...\nmais ma maman m’a dit de ne pas parler aux monsieurs bizarres...",
        "Attends...je suis qu’un gentil homme je t’assure..! Tu sais je suis...ou du moins j’étais...un grand professeur avant, et je passais mes journées à aider des petits garçons tout comme toi, alors...n’hésite pas à tout me dire.",
        "... Bon d’accord. Ma mère vit toute seule et elle m’a demandé d’aller donner ce bout de papier vite vite à un grand monsieur tout blanc avant la nuit...mais le problème est que je ne me rappelle plus de lui... ",
        "Mhmmm...je pense bien voir de qui tu parles, et je vais de ce pas te le révéler mais à une seule petite condition...",
        "Je suis très malchanceux en ce moment...et à force d’aider les gens partout je me retrouve sans endroit pour dormir ce soir. Et toi et ta mère...vous dormez bien chaque soir...non ? Vous pourriez surement accueillir un pauvre professeur sans emploi comme moi, non ?",
        "Pour te prouver ma bienveillance voici un petit indice sur son identité : Il possède un magasin important et tu le trouveras probablement derrière son comptoir.",
        "D’accord...je vais demander à ma maman après. Mais dites-moi en premier parce que je dois aller vite.",
        "Ha ha ha ! Je comprends totalement ce que tu veux dire, mais si tu pars aussi vite que tu es arrivé qu’est ce qui ne m’assure pas que je te manquerai ? Allez, passons rapidement vers chez toi et ce sera vite fait!",
        "Monsieur vous êtes très bizarre et je dois m’en aller... je vais venir vous voir après. ",
        "Allons, viens-ici... Ça ne sera pas long...rapproche-toi un peu que je te puisse t’accompagner...",
        "...Allez-vous-en !"
    };

    // Interaction Pharmacien
    List<string> diagPharma = new List<string> {
        "Bonjour monsieur...est-ce que vous voulez mon papier?",
        "Oh, bonjour toi. Tu me sembles tout essoufflé, tout va bien ?",
        "Oui oui ça va, mais il y avait beaucoup de gens bizarre dehors. Voulez-vous mon papier?",
        "Mais oui, montre-moi ça...mhmm...D’accord, je vois que cela semble pressant. Pauvre toi... Tiens, voici ce que tu voulais.",
        "Ce que je voulais ? Je ne comprends pas...ma maman ne m’a pas parlé de ça pourtant. Qu’est-ce-que c’est ?",
        "Bon...je me présente. Je suis le pharmacien de ce quartier, et je m’occupe d’aider les autres comme je le peux lorsqu’ils sont malades. Et ce papier est ce que ta mère m’achète, ce que je lui prescris. Ne pouvait-elle pas venir le chercher elle-même?",
        "Non, elle m’a dit qu’elle se sentait fatiguée aujourd’hui et allait dormir plus tôt.",
        "Oula ! Malheureusement, si c’est rendu au point où elle n’a pas pu se rendre d’elle-même, c'est que sa situation s’est beaucoup aggravée, permet moi de m’inquiéter grandement pour vous. Vite ! Retourne la voir et donne-lui ça à tout prix !"
    };

    // Interaction PNJ1
    List<string> diagPNJ1Jeu = new List<string> {
        "Auriez-vous vu un monsieur important qui porte du blanc ?",
        "Je ne sais pas de qui tu parles, et je suis occupé. Et toi, n’as-tu pas un jeu auquel jouer au lieu de t’attarder à cette activité futile ?"
    };

    // Interaction PNJ2
    List<string> diagPNJ2Mock = new List<string> {
        "Bonjour monsieur, auriez-vous vu un grand homme blanc et sérieux qui peut recevoir des papiers ?",
        "Ma foi, tu sembles complètement perdu, petit gamin, tes parents ne t’ont-t-il pas bien éduqué ?",
        "Est-ce que vous connaissez le monsieur tout blanc qui vit près d’ici ? Je dois lui donner un papier.",
        "Un monsieur tout blanc ? Parles-tu d’un fantôme ? Tu es drôle toi, non je n’en ai pas vu, raconte-moi si tu en trouve un ha ha."
    };

    // Interaction PNJ3
    List<string> diagPNJ3Noir = new List<string> {
        "Sans blague ! Un autre accident désastreux et une nouvelle panne ! Quand est-ce que ce cauchemar va s'arrêter !"
    };


    /* Les differentes listes */
    // Variable indiquant la fin de cette partie
    public static bool affichageTextesTuto;

    // Retire temporairement la possibilite d'interagir
    public static bool retireInteractionJoueur;

    public static int compteurInteracProf = 0;

    // Statut pour un texte d'un personnage
    public bool typePerso;

    void Start()
    {

        typePerso = false;
        dialogueText.text = "";
        listeDiag.Clear();

        // Le compteur de dialogue commence a zero
        indexListeDiag = 0;

        if (SceneManager.GetActiveScene().name == "sceneXavierTuto")
        {

            // Le joueur inititie le tutoriel des le debut
            affichageTextesTuto = true;
            retireInteractionJoueur = true;

            StartCoroutine(LancerDialogue());

            compteAccroupi = 0f;

            listeDiag.AddRange(consignesTuto);
        }
        else
        {
            affichageTextesTuto= false;
        }
    }

    /* Coroutine pour lancer le dialogue après un delay, puis ecrire le texte lettre par lettre */
    IEnumerator LancerDialogue()
    {
        // le texte charge
        estEnTrainDEcrire = true;

        /* Pour les consignes, le chargement se fait a chaque instruction */
        if(!typePerso)
        {
            // Animation apparition textbox
            yield return new WaitForSeconds(delayAvantAffichage);

            if (!dialogueText.gameObject.activeSelf)
                dialogueText.gameObject.SetActive(true);

            dialogueBox.SetActive(true);
            animator.SetTrigger("FadeIn");
            yield return new WaitForSeconds(1f);
        }

        /* Dans une sequence de dialogue, le chargement de la boite ne se fait qu'au debut */
        else
        {
            if(indexListeDiag == 0)
            {
                // Animation apparition textbox
                yield return new WaitForSeconds(delayAvantAffichage);

                if (!dialogueText.gameObject.activeSelf)
                    dialogueText.gameObject.SetActive(true);

                dialogueBox.SetActive(true);
                animator.SetTrigger("FadeIn");
                yield return new WaitForSeconds(1f);
            }

            /* Changements de style adaptes aux lignes de dialogue */
            // Dialogue prof 1
            if(listeDiag[indexListeDiag] == diagProf1[indexListeDiag])
            {
                StylesDiagProf1();
            }

            if (listeDiag[indexListeDiag] == diagPharma[indexListeDiag])
            {
                StylesDiagPharma();
            }
        }

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
        /* Lorsque le dialogue represente des persos */
        if(typePerso)
        {
            // Aligner le texte gauche
            if(dialogueText.alignment != TextAnchor.MiddleLeft)
                dialogueText.alignment = TextAnchor.MiddleLeft;

            if (vitesseEcriture != 0.03f) vitesseEcriture = 0.03f;

        }
        // Pour toute autre situation, le texte est centre
        else
        {
            if (dialogueText.alignment != TextAnchor.MiddleCenter)
                dialogueText.alignment = TextAnchor.MiddleCenter;

            // Le texte centre se lit plus lentement
            if (vitesseEcriture != 0.05f) vitesseEcriture = 0.05f;

            if (dialogueText.color != couleurPNJ) dialogueText.color = couleurPNJ;
            if (dialogueText.font != fontPNJ) dialogueText.font = fontPNJ;

        }

            /* Determiner conditions pour skip dialogue */
            if (estEnTrainDEcrire)
            {

                if (listeDiag != null && listeDiag.Count != 0)
                {
                    // Le texte finit d'ecrire lorsqu'il devient pareil a la valeur de la liste
                    if (dialogueText.text == listeDiag[indexListeDiag]) estEnTrainDEcrire = false;
                }

                /* Conditions speciales du tuto */
                if (affichageTextesTuto)
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
                                estEnTrainDEcrire = false;

                        }
                        }

                        // 2. Test saut
                        else if (indexListeDiag == 1)
                        {
                            if (Input.GetKeyDown(KeyCode.Space))
                            {
                                ChargerTexteEntierEarly();
                                estEnTrainDEcrire = false;
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
                                estEnTrainDEcrire= false;
                            }

                            if(XavierScriptInteraction.interactionFonctionnelle)
                            {
                                StartCoroutine(FermerEtLancerMessageAuto());
                                estEnTrainDEcrire = false;
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
                                    estEnTrainDEcrire = false;
                                }
                        }

                        if (indexListeDiag != 4 && indexListeDiag != 5)
                        {
                            /* Condition generale, juste clicker */
                            if (Input.GetMouseButtonDown(0))
                            {
                                ChargerTexteEntierEarly();
                                estEnTrainDEcrire = false;
                            }
                        }

                }

                else
                {
                     /* Condition generale, juste clicker */
                     if (Input.GetMouseButtonDown(0) && dialogueBox.activeSelf)
                     {
                        ChargerTexteEntierEarly();
                        estEnTrainDEcrire = false;
                     }
                }

            }
            
            /* Lorsque le texte a fini d'ecrire, voici des conditions des textes */
            else
            {

                // Pour le texte du tutoriel
                if (affichageTextesTuto)
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
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Le message se ferme, et l'index augmente s'il y a un autre texte
                        if (indexListeDiag < listeDiag.Count) indexListeDiag++;
                        StartCoroutine(FermerEtLancerMessageAuto());
                    }
                }

            }

        /* Valider toute interaction du joueur */
        if (XavierScriptInteraction.interactionFonctionnelle)
        {
            XavierScriptInteraction.interactionFonctionnelle = false;

            /* Activer dialogues specifiques selon l'interaction */
            if(XavierScriptInteraction.nomObjetInteract == "prof")
            {
                if(compteurInteracProf == 0)
                {
                    listeDiag.AddRange(diagProf1);
                    typePerso = true;
                    retireInteractionJoueur = true;
                    StartCoroutine(LancerDialogue());
                    compteurInteracProf++;
                }
            }

            if(XavierScriptInteraction.nomObjetInteract == "pharmacien")
            {
                listeDiag.AddRange(diagPharma);
                typePerso = true;
                retireInteractionJoueur = false;
                StartCoroutine(LancerDialogue());
            }

            // Apres verification, reinitialiser la valeur de l'interaction
            XavierScriptInteraction.nomObjetInteract = "";
        }
    }
    // Coroutine pour fermer le dialogue et lancer l'autre message
    IEnumerator FermerEtLancerMessageAuto()
    {
        if(!typePerso)
        {
            animator.SetTrigger("FadeOut");
            // Vitesse effacer texte
            yield return new WaitForSeconds(1f);

            dialogueBox.SetActive(false);
            dialogueText.text = "";
        }
        else
        {
            dialogueText.text = "";
        }

        if (indexListeDiag < listeDiag.Count)
        {
            StartCoroutine(LancerDialogue());
        }
        // Lorsque l'affichage atteint sa fin, le statut de tuto prend fin
        else if (affichageTextesTuto == true)
        {
            indexListeDiag = 0;
            animator.SetTrigger("FadeOut");
            // Vitesse effacer texte
            yield return new WaitForSeconds(1f);

            dialogueBox.SetActive(false);
            dialogueText.text = "";

            affichageTextesTuto = false;
            indexListeDiag = 0;
            listeDiag.Clear();
        }
        else
        {
            indexListeDiag = 0;
            animator.SetTrigger("FadeOut");
            // Vitesse effacer texte
            yield return new WaitForSeconds(1f);

            dialogueBox.SetActive(false);
            dialogueText.text = "";
            retireInteractionJoueur = false;
            listeDiag.Clear();
        }
    }

    /* Fonction pour charger l'entierete d'un texte apres un clic */
    void ChargerTexteEntierEarly()
    {
        StopAllCoroutines();
        dialogueText.text = listeDiag[indexListeDiag];
    }

    /* Fonction d'alternance de styles des repliques DiagProf1 */
    void StylesDiagProf1()
    {
        // couleur prof
        if(indexListeDiag == 0 ||
            indexListeDiag == 2 ||
            indexListeDiag == 4 ||
            indexListeDiag == 5 ||
            indexListeDiag == 6 ||
            indexListeDiag == 8 ||
            indexListeDiag == 10)
        {
            if (dialogueText.color != couleurProf) dialogueText.color = couleurProf;
            if (dialogueText.font != fontProf) dialogueText.font = fontProf;
        }
        // couleur garcon
        else 
        if (indexListeDiag == 1 ||
            indexListeDiag == 3 ||
            indexListeDiag == 7 ||
            indexListeDiag == 9 ||
            indexListeDiag == 11)
        {
             if (dialogueText.color != couleurGarcon) dialogueText.color = couleurGarcon;
             if (dialogueText.font != fontGarcon) dialogueText.font = fontGarcon;
        }
    }

    /* Fonction d'alternance de styles des repliques DiagPharma */
    void StylesDiagPharma()
    {
        // couleur prof
        if (indexListeDiag == 0 ||
            indexListeDiag == 2 ||
            indexListeDiag == 4 ||
            indexListeDiag == 6)
        {
            if (dialogueText.color != couleurGarcon) dialogueText.color = couleurGarcon;
            if (dialogueText.font != fontGarcon) dialogueText.font = fontGarcon;
        }
        // couleur garcon
        else
        if (indexListeDiag == 1 ||
            indexListeDiag == 3 ||
            indexListeDiag == 5 ||
            indexListeDiag == 7)
        {
            if (dialogueText.color != couleurPharma) dialogueText.color = couleurPharma;
            if (dialogueText.font != fontPharma) dialogueText.font = fontPharma;
        }
    }
}