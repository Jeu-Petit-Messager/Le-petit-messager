using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    /* Source des images qui peuvent etre prises */
    public Sprite sourceImageCanette;
    public Sprite sourceImageClePharma;
    public Sprite sourceImageMedicament;

    /* Bool qui determine lorsque le garcon peut prendre un objet */
    public bool peutPrendre;

    /* Bool reconnaissant une interaction de l'exterieur */
    public static bool interactionFonctionnelle;

    /* Objet sauvegarde avec interac */
    public static string nomObjetInteract;

    /* lorsque le joueur possede une canette */
    public bool possedeCanette;

    public static bool possedeClePharma;

    public bool chargerLamp;
    public static bool enigmePharma;
    public bool chargerPharma;
    public static bool courseFinale;
    public bool chargerCourse;


    // Bloc qui disparait pour enigme lampadaire
    public GameObject blocCorridorLampadaire;
    public GameObject lumLamp1;
    public GameObject lumLamp2;
    public GameObject lumLamp3;
    public GameObject lumLamp4;

    public void Start()
    {
        lumLamp1.gameObject.SetActive(false);
        lumLamp2.gameObject.SetActive(false);
        lumLamp3.gameObject.SetActive(false);
        lumLamp4.gameObject.SetActive(false);

        // Le nombre de canettes collectees est remis a 0 au debut de la scene
        XavierZoneCanetteProg.canetteCollectees = 0;

        if(imageUIObjet!=null)imageUIObjet.gameObject.SetActive(!imageUIObjet.activeSelf);

        /* le garcon ne possede aucun objet au depart*/
        interactionFonctionnelle = false;
        nomObjetInteract = "";
        peutPrendre = true;
        possedeCanette = false;
        possedeClePharma = false;

        // Le joueur possede le medicament au depart
        if (SceneManager.GetActiveScene().name == "sceneJeuJour")
        {

            enigmePharma = true;
        }

        // Le joueur possede le medicament au depart
        if (SceneManager.GetActiveScene().name == "sceneJeuNuit")
        {
            enigmePharma = false;
            courseFinale = false;
            peutPrendre = false;
            if(imageUIObjet!=null)imageUIObjet.SetActive(!imageUIObjet.activeSelf);
            if(imageUIInterne!=null)imageUIInterne.GetComponent<Image>().sprite = sourceImageMedicament;
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
        PlayerPrefs.SetInt("CanetteCollectes", 0);
    }

    void Update()
    {
        if(XavierAffichageTextes.compteurInteracProf == 1)
        {
            XavierAffichageTextes.compteurInteracProf++;
        }


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
                objetInteractif = hitColliders[0].gameObject;


                // Le joueur peut interagir avec l'objet
                if (Input.GetKeyDown(interactKey))
                {
                    interactable.Interact();

                    objetInteractif = hitColliders[0].gameObject;

                    interactionFonctionnelle = true;

                    nomObjetInteract = objetInteractif.name;

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
                    else if (objetInteractif.CompareTag("ObjetSpecial"))
                    {
                        // Prise de courant enigme lampadaires
                        if(nomObjetInteract == "prise")
                        {
                            blocCorridorLampadaire.gameObject.SetActive(false);
                            lumLamp1.gameObject.SetActive(true);
                            lumLamp2.gameObject.SetActive(true);
                            lumLamp3.gameObject.SetActive(true);
                            lumLamp4.gameObject.SetActive(true);

                        }

                        // Prendre la cle de la pharmacie
                        else if(nomObjetInteract == "clePharma")
                        {
                            possedeClePharma = true;
                            imageUIObjet.SetActive(!imageUIObjet.activeSelf);
                            imageUIInterne.GetComponent<Image>().sprite = sourceImageClePharma;

                            objetInteractif.GetComponent<AudioSource>().Play();
                            Destroy(objetInteractif, 0.1f);

                            // Enigme pharma prend fin
                            enigmePharma = false;
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
