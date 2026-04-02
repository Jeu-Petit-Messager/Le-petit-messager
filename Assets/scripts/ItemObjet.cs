using UnityEngine;

/* Script a appliquer sur les objets qui peuvent etre interactif */
public class ItemObject : MonoBehaviour, IInteractable
{
    public string itemName;
    public string InteractionPrompt => $"X {itemName}";

    public void Interact()
    {
        Debug.Log($"Ceci est un test {itemName}!");
        SaveInteraction();
        //Destroy(gameObject); // Or disable it
    }

    void SaveInteraction()
    {
        // Sauvegarder l'objet
        int currentCount = PlayerPrefs.GetInt("ItemsCollected", 0);
        PlayerPrefs.SetInt("ItemsCollected", currentCount + 1);
        PlayerPrefs.Save();
    }
}