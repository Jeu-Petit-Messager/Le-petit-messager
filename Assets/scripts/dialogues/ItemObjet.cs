using UnityEngine;
using UnityEngine.UI;

/* Script a appliquer sur les objets qui peuvent etre interactif */
public class ItemObject : MonoBehaviour, IInteractable
{
    public string itemName;
    public string InteractionPrompt => $"X {itemName}";

    public GameObject objetInteractif;

    int layerDefaut;
    int layerInteractif;

    public void Start()
    {
        objetInteractif = gameObject; // L'objet interactif est celui sur lequel on a clique

        layerDefaut = LayerMask.NameToLayer("Default");
        layerInteractif = LayerMask.NameToLayer("Interactif");

        // Aucun objet est interactif au debut
        gameObject.layer = layerDefaut;

    }

    public void Update()
    {
        /* Section du tutoriel */
        if(XavierAffichageTextes.affichageTextesTuto)
        {
            /* Lorsque le joueur est autorise a interagir */
            if (!XavierAffichageTextes.retireInteractionJoueur)
            {
                if (gameObject.tag == "Canette" || gameObject.tag == "ZoneCanette")
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

        /* Fin tutoriel */
        else
        {
            // Tout objet desactive devient interactif
            if (gameObject.layer != layerInteractif)
                gameObject.layer = layerInteractif;
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
}