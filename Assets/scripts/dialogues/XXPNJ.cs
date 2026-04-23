using UnityEngine;

public abstract class XXPNJ : MonoBehaviour, IInteractable
{
    public string InteractionPrompt => throw new System.NotImplementedException();

    public abstract void Interact();
}