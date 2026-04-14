using UnityEngine;
using UnityEngine.UI;

/* Script a appliquer sur les objets qui peuvent etre interactif */
public class ItemObject : MonoBehaviour, IInteractable
{
    public string itemName;
    public string InteractionPrompt => $"X {itemName}";

    public GameObject objetInteractif;

    public void Interact()
    {
        objetInteractif = gameObject; // L'objet interactif est celui sur lequel on a clique

        //Debug.Log($"Tu viens d'obtenir un {itemName}!");
    }

    public void CompteurCanettes()
    {
        // Sauvegarder le nouveau nombre de canettes collectees
        int compteObjets = PlayerPrefs.GetInt("CanetteCollectes", 0);
        PlayerPrefs.SetInt("CanetteCollectes", compteObjets + 1);
        PlayerPrefs.Save();
        print(PlayerPrefs.GetInt("CanetteCollectes"));
    }
}