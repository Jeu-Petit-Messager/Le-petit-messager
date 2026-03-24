using UnityEngine;

/* Script controles joueuer */
public class XavierControlesJoueur : MonoBehaviour
{
    [Header("Parametres de mouvement")]
    public float vitesseMouvement = 5f;
    public float drag = 5f;
    public float forceSaut = 7f;
    public float airMultiplier = 0.4f;

    [Header("Parametres verifier si au sol")]
    public float hauteurJoueur = 2f;
    public LayerMask calqueSol;
    bool auSol;

    public Transform orientation; // An empty GameObject child that dictates "forward"

    float horizontalInput;
    float verticalInput;
    Vector3 directionDeplacement;
    Rigidbody rigidbodyJoueur;

    void Start()
    {
        /* Saisir le rigidbody */
        rigidbodyJoueur = GetComponent<Rigidbody>();
        rigidbodyJoueur.freezeRotation = true;
    }

    void Update()
    {
        // Raycast verification sol
        auSol = Physics.Raycast(transform.position, Vector3.down, hauteurJoueur * 0.5f + 0.2f, calqueSol);

        InputsJoueur();
        GestionVelocite();

        // Configurer le drag en fonction de si le joueur est au sol ou non
        if (auSol)
            rigidbodyJoueur.linearDamping = drag;
        else
            rigidbodyJoueur.linearDamping = 0;
    }

    void FixedUpdate()
    {
        // Appeler la fonction configurant les forces de deplacement du joueur
        DeplacementJoueur();
    }

    void InputsJoueur()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && auSol)
        {
            AppliquerForceSaut();
        }
    }

    /* Fonction configurant les forces de deplacement du joueur */
    void DeplacementJoueur()
    {
        // Saisir la direction de deplacement en fonction de l'orientation du joueur
        directionDeplacement = orientation.forward * verticalInput + orientation.right * horizontalInput;


        if (auSol)
            rigidbodyJoueur.AddForce(directionDeplacement.normalized * vitesseMouvement * 10f, ForceMode.Force);
        else
            rigidbodyJoueur.AddForce(directionDeplacement.normalized * vitesseMouvement * 10f * airMultiplier, ForceMode.Force);
    }

    void GestionVelocite()
    {
        Vector3 flatVel = new Vector3(rigidbodyJoueur.linearVelocity.x, 0f, rigidbodyJoueur.linearVelocity.z);

        // Limit velocity if it goes faster than vitesseMouvement
        if (flatVel.magnitude > vitesseMouvement)
        {
            Vector3 limitedVel = flatVel.normalized * vitesseMouvement;
            rigidbodyJoueur.linearVelocity = new Vector3(limitedVel.x, rigidbodyJoueur.linearVelocity.y, limitedVel.z);
        }
    }

    void AppliquerForceSaut()
    {
        // Reset y velocity for consistent jump height
        rigidbodyJoueur.linearVelocity = new Vector3(rigidbodyJoueur.linearVelocity.x, 0f, rigidbodyJoueur.linearVelocity.z);
        rigidbodyJoueur.AddForce(transform.up * forceSaut, ForceMode.Impulse);
    }
}