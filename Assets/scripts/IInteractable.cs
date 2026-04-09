/* Script d'interfaces a utiliser sur les interragibles */
public interface IInteractable
{
    string InteractionPrompt { get; }
    void Interact();
}
