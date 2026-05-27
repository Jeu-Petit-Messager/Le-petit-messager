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

    // Cle pour rentrer dans la pharmacie
    public static bool possedeClePharma;

    public static bool enigmeLampadaire;
    public bool chargerLamp;
    public static bool enigmePharma;
    public bool chargerPharma;
    public static bool courseFinale;
    public bool chargerCourse;


    // Bloc qui disparait pour enigme lampadaire
    public GameObject blocCorridorLampadaire;

    public void Start()
    {
        // Le nombre de canettes collectees est remis a 0 au debut de la scene
        XavierZoneCanetteProg.canetteCollectees = 0;

        imageUIObjet.gameObject.SetActive(!imageUIObjet.activeSelf);

        /* le garcon ne possede aucun objet au depart*/
        interactionFonctionnelle = false;
        nomObjetInteract = "";
        peutPrendre = true;
        possedeCanette = false;
        possedeClePharma = false;

        // Le joueur possede le medicament au depart
        if (SceneManager.GetActiveScene().name == "sceneJeuJour" || SceneManager.GetActiveScene().name == "sceneXavierEnigmesPrototype")
        {
            enigmeLampadaire = false;
            enigmePharma = true;
        }

        // Le joueur possede le medicament au depart
        if (SceneManager.GetActiveScene().name == "sceneJeuNuit" || SceneManager.GetActiveScene().name == "sceneXavierNuitMedic")
        {
            enigmeLampadaire = false;
            enigmePharma = false;
            courseFinale = false;
            peutPrendre = false;
            imageUIObjet.SetActive(!imageUIObjet.activeSelf);
            imageUIInterne.GetComponent<Image>().sprite = sourceImageMedicament;
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
            if (!enigmeLampadaire) enigmeLampadaire = true;
            print(enigmeLampadaire);
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
                            print("fin");
                        }

                        // Prendre la cle de la pharmacie
                        else if(nomObjetInteract == "clePharma")
                        {
                            possedeClePharma = true;
                            imageUIObjet.SetActive(!imageUIObjet.activeSelf);
                            imageUIInterne.GetComponent<Image>().sprite = sourceImageClePharma;

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
