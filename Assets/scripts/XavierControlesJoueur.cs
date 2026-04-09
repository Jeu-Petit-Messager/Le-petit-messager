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

    [Header("Status du garcon")]
    public bool peutCourir;
    // devient true lorsque le joueur court
    public bool enCourant;
    public bool estAccroupi;
    public bool peutSauter;


    [Header("Reglages du saut")]
    CapsuleCollider colliderJoueur;
    public float forceSaut;
    public LayerMask solCalque;
    public bool auSol;
    public float distanceAuSol;
    public float margeDetectionAuSol;

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
        enCourant = false;
        estAccroupi = false;
        peutSauter = true;
        
        GetComponent<CapsuleCollider>().height = hauteurCollider;
        GetComponent<CapsuleCollider>().center = new Vector3(0f, centreYCollider, 0f);

    }

    void Update()
    {

        /* Detecter si le garcon est au sol */
        // On lance le rayon depuis le centre du collider, pas depuis le pivot
        Vector3 centreCollider = transform.TransformPoint(colliderJoueur.center);

        // On lance un spherecast vers le bas pour detecter le sol, en partant du centre du collider, avec un rayon legerement plus petit que le radius du collider pour eviter les faux positifs
        auSol = Physics.SphereCast(centreCollider, colliderJoueur.radius * 0.9f, Vector3.down, out _, (colliderJoueur.height / 2) - (colliderJoueur.radius * 0.9f) + margeDetectionAuSol, solCalque);


        /* le joueur est moins aerodynamique dans les airs */
        if(auSol) GetComponent<Rigidbody>().linearDamping = 0.4f;
        else GetComponent<Rigidbody>().linearDamping = 2f;

        /* Etre accroupi limite les mouvements du garcon*/
        if (estAccroupi)
        {
            peutCourir = false;
            peutSauter = false;

        }
        else
        {
            peutCourir = true;

            /* le joueur peut seulement sauter lorsqu'il est au sol */
            if (auSol) peutSauter = true;
            else peutSauter = false;

        }

        /// Gerer le saut du joueur
        if (peutSauter)
        {
            if (Input.GetKeyDown(KeyCode.Space) && auSol)
            {
                SautJoueur();
            }
        }

        /* Le joueur s'accroupit lorsque CTRL est appuye, au sol sans courir */
        if (Input.GetKeyDown(KeyCode.LeftControl) && auSol && !enCourant)
        {
            if (!estAccroupi)
            {
                estAccroupi = true;
                GetComponent<CapsuleCollider>().height = hauteurColliderAccroupi;
                GetComponent<CapsuleCollider>().center = new Vector3(0f, centreYColliderAccroupi, 0f);

                /* la vitesse du joueur est reduite lorsqu'il est accroupi */
                //vitesseAvantMax = 0;
                //print(vitesseAvantMax);

            }
            else
            {
                estAccroupi = false;
                GetComponent<CapsuleCollider>().height = hauteurCollider;
                GetComponent<CapsuleCollider>().center = new Vector3(0f, centreYCollider, 0f);
                //print(vitesseAvantMax);
            }
        }

        /* Si le joueur n'est pas sous restriction de course */
        if (peutCourir)
        {
            /* Maintenir SHIFT pour courir */
            if (Input.GetKey(KeyCode.LeftShift))
            {
                vitesseAvantMax = 1.2f * vitesseAvantMaxMarche;
                enCourant = true;

            }
            /* Si le joueur courait, le faire marcher */
            else
            {
                vitesseAvantMax = vitesseAvantMaxMarche;
                enCourant = false;
            }
        }
        /* Si le joueur courait, le faire marcher */
        else
        {
            vitesseAvantMax = vitesseAvantMaxMarche;
            enCourant = false;
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

        // Avancer et reculer le garcon
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (!estAccroupi)
            {
                GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, vitesseAvant);
            }
            else
            {
                GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, 0.98f*vitesseAvant);
            }

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

    void SautJoueur()
    {
        // R�initialise la vitesse verticale avant de sauter (optionnel, pour des sauts constants)
        rigidbodyJoueur.linearVelocity = new Vector3(rigidbodyJoueur.linearVelocity.x, 0f, rigidbodyJoueur.linearVelocity.z);

        rigidbodyJoueur.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);
    }

}