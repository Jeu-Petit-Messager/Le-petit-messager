using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/* Script a appliquer sur les objets qui peuvent etre interactif */
public class ItemObject : MonoBehaviour, IInteractable
{
    public string itemName;
    public string InteractionPrompt => $"X {itemName}";

    public GameObject objetInteractif;

    // Pour les objets ayant un son
    public AudioSource audioSource;

    int layerDefaut;
    int layerInteractif;

    public void Start()
    {
        objetInteractif = gameObject; // L'objet interactif est celui sur lequel on a clique

        layerDefaut = LayerMask.NameToLayer("Default");
        layerInteractif = LayerMask.NameToLayer("Interactif");

        // Aucun objet est interactif au debut
        gameObject.layer = layerDefaut;

        if(audioSource != null)
            gameObject.GetComponent<AudioSource>().enabled = false;

    }

    public void Update()
    {
        //print(XavierScriptInteraction.enigmeLampadaire);

        /* Restriction dans la section du tutoriel */
        if(XavierAffichageTextes.affichageTextesTuto)
        {
            /* Lorsque le joueur est autorise a interagir */
            if (!XavierAffichageTextes.retireInteractionJoueur)
            {
                if (gameObject.name == "cloture")
                {
                    if(gameObject.layer != layerInteractif)
                        gameObject.layer = layerInteractif;
                }
            }

            else
            {
                // Desactiver les interactions pour tous
                if (gameObject.layer != layerDefaut)
                    gameObject.layer = layerDefaut;
            }
        }

        else if(XavierAffichageTextes.retireInteractionJoueur)
        {
            // Desactiver les interactions pour tous
            if (gameObject.layer != layerDefaut)
                gameObject.layer = layerDefaut;
        }

        /* Fin tutoriel */
        else
        {
            if(gameObject.name == "prof" && XavierAffichageTextes.compteurInteracProf == 1)
            {
                if (gameObject.layer != layerDefaut)
                    gameObject.layer = layerDefaut;
            }
            else
            {
                if(gameObject.name != "clePharma" && gameObject.name != "prise")
                {
                    // Tout objet desactive devient interactif
                    if (gameObject.layer != layerInteractif)
                        gameObject.layer = layerInteractif;
                }

            }
        }

        if (gameObject.name == "prise")
        {

            if (!XavierScriptInteraction.enigmeLampadaire)

            {
                if (gameObject.layer == layerInteractif)
                {

                    // On lance la routine qui va gerer la destruction de l'objet
                    audioSource.Play();

                    gameObject.layer = layerDefaut;

                }
            }
            else if (XavierScriptInteraction.enigmeLampadaire)
            {
                if (gameObject.layer != layerInteractif)
                    gameObject.layer = layerInteractif;
            }
        }

        if (gameObject.name == "clePharma")
        {
            if(!XavierScriptInteraction.enigmePharma)
            {
                if (gameObject.layer == layerInteractif)
                {

                    // On lance la routine qui va gerer la destruction de l' objet
                    StartCoroutine(JouerEtDetruire());

                    gameObject.layer = layerDefaut;

                    gameObject.GetComponent<AudioSource>().enabled = true;

                }
            }
            else if (XavierScriptInteraction.enigmePharma)
            {
                if (gameObject.layer != layerInteractif)
                    gameObject.layer = layerInteractif;
            }
        }

    }

    public void Interact()
    {
        //Debug.Log($"Tu viens d'obtenir un {itemName}!");
    }



    /* Fonction incrementant le compte des canettes */
    public void CompteurCanettes()
    {
        // Sauvegarder le nouveau nombre de canettes collectees
        int compteObjets = PlayerPrefs.GetInt("CanetteCollectes", 0);
        PlayerPrefs.SetInt("CanetteCollectes", compteObjets + 1);
        PlayerPrefs.Save();

        Destroy(objetInteractif);
    }

    /* Fonction pour detruire un objet apres qu'il a fini de jouer un son */
    IEnumerator JouerEtDetruire()
    {
        // 1. On lance le son
        audioSource.Play();

       // 2. On attend la durée exacte du clip audio (en secondes)
        yield return new WaitForSeconds(audioSource.clip.length);

        // 3. Le son est fini, on détruit ce GameObject
        Destroy(gameObject);
    }
}