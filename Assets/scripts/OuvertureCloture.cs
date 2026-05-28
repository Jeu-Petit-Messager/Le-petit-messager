using UnityEngine;
using UnityEngine.Events;

public class OuvertureCloture : MonoBehaviour
{
    public KeyCode toucheInteraction = KeyCode.E;
    public UnityEvent quandOnAppuieSurE;
    private bool joueurDansLaZone = false;

    void Update()
    {
        // Si le joueur est là et appuie sur E, on déclenche l'événement visuel
        if (joueurDansLaZone && Input.GetKeyDown(toucheInteraction))
        {
            quandOnAppuieSurE.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null) joueurDansLaZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null) joueurDansLaZone = false;
    }
}