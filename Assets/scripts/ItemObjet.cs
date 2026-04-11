using UnityEngine;

/* Script a appliquer sur les objets qui peuvent etre interactif */
public class ItemObject : MonoBehaviour, IInteractable
{
    public string itemName;
    public string InteractionPrompt => $"X {itemName}";

    public void Interact()
    {
        Debug.Log($"Tu viens d'obtenir un {itemName}!");
        SauvegarderInteraction();
        //Destroy(gameObject); // Or disable it
    }

    void SauvegarderInteraction()
    {
        // Sauvegarder l'objet
        int compteObjets = PlayerPrefs.GetInt("ItemsCollectes", 0);
        PlayerPrefs.SetInt("ItemsCollectes", compteObjets + 1);
        PlayerPrefs.Save();
        print(PlayerPrefs.GetInt("ItemsCollectes"));
    }
}