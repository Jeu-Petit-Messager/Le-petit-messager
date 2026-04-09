using UnityEngine;

/* Script placee sur le garcon pour interagir avec des objets */
public class XavierScriptInteraction : MonoBehaviour
{
    public Transform leGarcon; // Objet de reference pour determiner la position du scan
    public float offsetPosition; // Distance devant le garcon
    public float distanceScanee; // Portee de la zone d'interaction
    public Vector3 positionScan;
    public LayerMask interactif;

    /* Le bouton pour interagir est E */
    public KeyCode interactKey = KeyCode.E;

    void Update()
    {

        
        positionScan = leGarcon.position + (leGarcon.forward * offsetPosition);

        // Trouver tous les colliders de choses interactives
        Collider[] hitColliders = Physics.OverlapSphere(positionScan, distanceScanee, interactif);

        // Lorsqu'un objet est detecte
        if (hitColliders.Length > 0)
        {
            // Get the closest interactable
            IInteractable interactable = hitColliders[0].GetComponent<IInteractable>();

            if (interactable != null)
            {
                // Le joueur peut interagir avec l'objet
                if (Input.GetKeyDown(interactKey))
                {
                    interactable.Interact();
                }
            }
        }
    }

    // Visualiser la zone d'interaction in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(positionScan, distanceScanee);
    }
}
