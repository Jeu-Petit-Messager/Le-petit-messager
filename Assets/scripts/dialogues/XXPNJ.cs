using UnityEngine;

public abstract class XXPNJ : MonoBehaviour, IInteractable
{
    // Lorsqu'un script utilise IInteractable, il doit comporter tous les elements de cette interface
    public string InteractionPrompt => throw new System.NotImplementedException();

    [SerializeField] private SpriteRenderer spriteIndicPNJ;

    public Transform detectTransformGars;

    const float distanceInteraction = 5f;

    private void Start()
    {
        
    }

    public abstract void Interact();

    private bool isWithinInteractDistance()
    {
        if (Vector3.Distance(detectTransformGars.position, transform.position) < distanceInteraction)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}