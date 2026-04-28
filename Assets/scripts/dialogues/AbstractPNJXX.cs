using UnityEngine;
using UnityEngine.InputSystem;

public abstract class XXPNJ : MonoBehaviour, IInteractable
{
    // Lorsqu'un script utilise IInteractable, il doit comporter tous les elements de cette interface
    public string InteractionPrompt => throw new System.NotImplementedException();

    [SerializeField] private SpriteRenderer spriteIndicPNJ;

    public Transform detectTransformGars;

    const float distanceInteraction = 5f;

    private void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }

        if(spriteIndicPNJ && !isWithinInteractDistance())
        {
            // Visuel UI se cache lorsque le gars sort
            spriteIndicPNJ.gameObject.SetActive(false);
        }
        else if(!spriteIndicPNJ && isWithinInteractDistance())
        {
            // Visuel UI se montre lorsque le gars rentre a bonne distance
            spriteIndicPNJ.gameObject.SetActive(true);

        }
    }

    public abstract void Interact();

    // Une fonction retournant le bool
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