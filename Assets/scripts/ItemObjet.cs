using UnityEngine;

/* Script a appliquer sur les objets qui peuvent etre interactif */
public class ItemObject : MonoBehaviour, IInteractable
{
    public string itemName;
    public string InteractionPrompt => $"X {itemName}";

    GameObject objetInteractif;

    public void Start()
    {
        objetInteractif = gameObject;
    }

    public void Interact()
    {
        if (objetInteractif.CompareTag("Canette"))
        {
            CompteurCanettes();
        }

        Debug.Log($"Tu viens d'obtenir un {itemName}!");
        
    }

    void CompteurCanettes()
    {
        // Sauvegarder le nouveau nombre de canettes collectees
        int compteObjets = PlayerPrefs.GetInt("CanetteCollectes", 0);
        PlayerPrefs.SetInt("CanetteCollectes", compteObjets + 1);
        PlayerPrefs.Save();
        print(PlayerPrefs.GetInt("CanetteCollectes"));
    }
}