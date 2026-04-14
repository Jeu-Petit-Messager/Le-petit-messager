using UnityEngine;
using UnityEngine.UI;

/* Script a appliquer sur les objets qui peuvent etre interactif */
public class ItemObject : MonoBehaviour, IInteractable
{
    public string itemName;
    public string InteractionPrompt => $"X {itemName}";

    public GameObject imageUIObjet; // Image UI pour afficher l'objet dans l'inventaire

    public Sprite sourceImageCanette;

    /* Bool qui determine lorsque le garcon peut prendre un objet */
    public bool peutPrendre;

    /* lorsque le joueur possede une canette */
    public bool possedeCanette;

    public GameObject objetInteractif;

    public void Start()
    {
        objetInteractif = gameObject;

        //print(imageUIObjet);
        imageUIObjet.gameObject.SetActive(!imageUIObjet.activeSelf);
        print(imageUIObjet);

        /* le garcon ne possede aucun objet au depart*/
        peutPrendre = true;
        possedeCanette = false;
    }

    public void Interact()
    {
        if (objetInteractif.CompareTag("Canette"))
        {
            if (peutPrendre)
            {
                possedeCanette = true;
                imageUIObjet.SetActive(!imageUIObjet.activeSelf);
                imageUIObjet.GetComponent<Image>().sprite = sourceImageCanette;
                CompteurCanettes();
            }
        }
        else if (objetInteractif.CompareTag("ZoneCanette"))
        {
            print("Tu es dans la zone de depot de canette");

            if (possedeCanette)
            {
                // Enlever l'image de la canette de l'inventaire
                //imageUIObjet.SetActive(!imageUIObjet.activeSelf);
                //imageUIObjet.GetComponent<Image>().sprite = null;

                /* le joueur depose la canette */
                possedeCanette = false;
                peutPrendre = true;
            }
        }

        //Debug.Log($"Tu viens d'obtenir un {itemName}!");
    }

    void CompteurCanettes()
    {
        /* le garcon possede un objet */
        peutPrendre = false;

        // Sauvegarder le nouveau nombre de canettes collectees
        int compteObjets = PlayerPrefs.GetInt("CanetteCollectes", 0);
        PlayerPrefs.SetInt("CanetteCollectes", compteObjets + 1);
        PlayerPrefs.Save();
        print(PlayerPrefs.GetInt("CanetteCollectes"));
    }
}