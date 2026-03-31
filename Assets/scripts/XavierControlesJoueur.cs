using UnityEngine;

/* Script des controles du joueur */
public class XavierControlesJoueur : MonoBehaviour
{
    [Header("Reglages des deplacements")]
    public float vitesseAvant;
    public float vitesseAvantMax;
    float vitesseAvantMaxMarche;
    public float forceAcceleration;
    public float vitesseRotation;
    public float forceRotation;
    Rigidbody rigidbodyJoueur;

    [Header("Status mvmts")]
    public bool peutCourir;
    public bool estAccroupi;
    public bool peutSauter;

    [Header("Reglages du saut")]
    CapsuleCollider colliderJoueur;
    public float forceSaut;
    public LayerMask solCalque;
    public bool auSol;
    public float distanceAuSol;
    private float margeDetectionAuSol = 0.1f;

    [Header("Stats collider accroupi")]
    public float hauteurCollider;
    public float centreYCollider;
    public float hauteurColliderAccroupi;
    public float centreYColliderAccroupi;

    void Start()
    {
        /* Enregistrer donnees initiales */
        vitesseAvantMaxMarche = vitesseAvantMax;
        rigidbodyJoueur = GetComponent<Rigidbody>();
        colliderJoueur = GetComponent<CapsuleCollider>();
        peutCourir = true;
        estAccroupi = false;
        peutSauter = true;
        
        GetComponent<CapsuleCollider>().height = hauteurCollider;
        GetComponent<CapsuleCollider>().center = new Vector3(0f, centreYCollider, 0f);

    }

    void Update()
    {
        if(estAccroupi) peutCourir = false;
        else peutCourir = true;

        /* Detecter si le garcon est au sol */
        // On calcule la distance du pivot jusqu'au bas du collider
        // col.bounds.extents.y donne la moitié de la hauteur réelle du collider
        // col.center.y prend en compte le décalage du centre s'il y en a un
        //float distanceToGround = (col.height / 2) - col.center.y + margin;

        // On lance le rayon depuis le centre du collider, pas depuis le pivot
        Vector3 startPoint = transform.TransformPoint(colliderJoueur.center);
        auSol = Physics.Raycast(startPoint, Vector3.down, (colliderJoueur.height / 2) + margeDetectionAuSol, solCalque);



        /* Le joueur s'accroupit lorsque CTRL est appuye */
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!estAccroupi)
            {
                estAccroupi = true;
                GetComponent<CapsuleCollider>().height = hauteurColliderAccroupi;
                GetComponent<CapsuleCollider>().center = new Vector3(0f, centreYColliderAccroupi, 0f);

            }
            else
            {
                estAccroupi = false;
                GetComponent<CapsuleCollider>().height = hauteurCollider;
                GetComponent<CapsuleCollider>().center = new Vector3(0f, centreYCollider, 0f);
            }
        }

        /* Si le joueur n'est pas sous restriction de course */
        if (peutCourir)
        {
            /* Maintenir SHIFT pour courir */
            if (Input.GetKey(KeyCode.LeftShift))
            {
                vitesseAvantMax = 1.2f * vitesseAvantMaxMarche;
            }
            /* Si le joueur courait, le faire marcher */
            else
            {
                vitesseAvantMax = vitesseAvantMaxMarche;
            }
        }
        /* Si le joueur courait, le faire marcher */
        else
        {
            vitesseAvantMax = vitesseAvantMaxMarche;
        }


        /* Gerer le deplacement avant/arriere du joueur */
        if ((Input.GetKey(KeyCode.W) || (Input.GetKey(KeyCode.UpArrow)) && vitesseAvant < vitesseAvantMax))
        {

            vitesseAvant += forceAcceleration;
        }
        if ((Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.DownArrow)) && vitesseAvant > -vitesseAvantMax))
        {
            
            vitesseAvant -= forceAcceleration;
        }


        /* Gerer la rotation du joueur */
        vitesseRotation = Input.GetAxis("Horizontal") * forceRotation;
        vitesseAvant = Input.GetAxis("Vertical") * forceAcceleration;

        if (vitesseAvant > vitesseAvantMax) vitesseAvant = vitesseAvantMax;
        if (vitesseAvant < -vitesseAvantMax) vitesseAvant = -vitesseAvantMax;
    }

    void FixedUpdate()
    {
        // Appeler la fonction de gestion du mouvement
        //BougerJoueur();

        // Avancer et reculer le garcon
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, vitesseAvant);

        }
        // Aucun input = vitesseAvant est reinitialise
        else
        {
            if (vitesseAvant != 0) vitesseAvant = 0f;
        }

        // Tourner le joueur
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(0f, vitesseRotation, 0f);
        }

    }

    void statsCourse()
    {
        vitesseAvantMax *= 1.2f;
    }

    void statsMarche()
    {
        vitesseAvantMax /= 1.2f;
        vitesseAvant = vitesseAvantMax;
    }

    void Sauter()
    {
        print("Sauter !");

    }
}