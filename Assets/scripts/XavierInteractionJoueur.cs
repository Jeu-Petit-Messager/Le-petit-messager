using UnityEngine;
using UnityEngine.UI;

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

    public GameObject objetInteractif; // Objet interactif detecte


    public GameObject imageUIObjet; // Image UI pour afficher l'objet dans l'inventaire
    public GameObject imageUIInterne; // Game object avec la source a changer

    public Sprite sourceImageCanette;

    /* Bool qui determine lorsque le garcon peut prendre un objet */
    public bool peutPrendre;

    /* lorsque le joueur possede une canette */
    public bool possedeCanette;

    public void Start()
    {
        // Le nombre de canettes collectees est remis a 0 au debut de la scene
        XavierZoneCanetteProg.canetteCollectees = 0;

        imageUIObjet.gameObject.SetActive(!imageUIObjet.activeSelf);

        /* le garcon ne possede aucun objet au depart*/
        peutPrendre = true;
        possedeCanette = false;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
        PlayerPrefs.SetInt("CanetteCollectes", 0);
    }

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

                    objetInteractif = hitColliders[0].gameObject;

                    if (objetInteractif.CompareTag("Canette"))
                    {
                        if (peutPrendre)
                        {
                            peutPrendre = false;
                            objetInteractif.GetComponent<ItemObject>().CompteurCanettes();
                            possedeCanette = true;
                            imageUIObjet.SetActive(!imageUIObjet.activeSelf);
                            imageUIInterne.GetComponent<Image>().sprite = sourceImageCanette;
                        }

                    }
                    else if (objetInteractif.CompareTag("ZoneCanette"))
                    {
                        if (possedeCanette)
                        {
                            XavierZoneCanetteProg.canetteCollectees++;

                            // Enlever l'image de la canette de l'inventaire
                            imageUIObjet.SetActive(!imageUIObjet.activeSelf);
                            imageUIInterne.GetComponent<Image>().sprite = null;

                            /* le joueur depose la canette */
                            possedeCanette = false;
                            peutPrendre = true;
                        }
                    }
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
