using UnityEngine;

/* Script des controles du joueur */
public class XavierControlesJoueur : MonoBehaviour
{
    [Header("Reglages du mouvement")]
    public float vitesseMouvement = 5f;
    public float friction = 5f;
    public float forceSaut = 7f;
    public float multiplicateurAir = 0.4f;
    public float forceRotation;
    public float vitesseTourne;

    [Header("Detection du sol")]
    public float hauteurJoueur = 2f;
    public LayerMask coucheSol;
    bool auSol;

    float inputHorizontale;
    float inputVerticale;
    Vector3 directionMouvement;
    Rigidbody rigidbodyJoueur;

    void Start()
    {
        rigidbodyJoueur = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 1. Verification du sol (Raycast vers le bas)
        auSol = Physics.Raycast(transform.position, Vector3.down, hauteurJoueur * 0.5f + 0.2f, coucheSol);

        GererInputs();
        //ControlerVitesse();

        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * vitesseTourne);

        // Appliquer la friction (Drag) uniquement au sol
        if (auSol)
            rigidbodyJoueur.linearDamping = friction;
        else
            rigidbodyJoueur.linearDamping = 0;
    }

    void FixedUpdate()
    {
        // Appeler la fonction de gestion du mouvement
        BougerJoueur();

    }

    void GererInputs()
    {
        inputHorizontale = Input.GetAxisRaw("Horizontal");
        inputVerticale = Input.GetAxisRaw("Vertical");

        // Logique du saut
        if (Input.GetKeyDown(KeyCode.Space) && auSol)
        {
            Sauter();
        }
    }

    void BougerJoueur()
    {
        // 1. Calcul de la direction relative au monde (Z = devant, X = droite)
        directionMouvement = new Vector3(inputHorizontale, 0f, inputVerticale);

        //if (inputVerticale > 0)
        //{
        //    GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, vitesseMouvement);
        //}

        //else if (inputVerticale < 0)
        //{
        //    GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, -vitesseMouvement);
        //}

        if (inputVerticale < 0 || inputVerticale > 0)
        {
            // 2. Rotation : Faire face à la direction du mouvement
            // On calcule la rotation cible
            Quaternion rotationCible = Quaternion.LookRotation(directionMouvement);

            // On tourne progressivement vers cette cible (Slerp)
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationCible, vitesseTourne * Time.fixedDeltaTime);

            // 3. Déplacement : On avance toujours vers l'avant du transform actuel
            rigidbodyJoueur.AddForce(directionMouvement * vitesseMouvement * 10f, ForceMode.Force);
        }
    }

    void ControlerVitesse()
    {
        Vector3 vitesseHorizontale = new Vector3(rigidbodyJoueur.linearVelocity.x, 0f, rigidbodyJoueur.linearVelocity.z);

        // Limiter la vitesse si elle d�passe la vitesse maximum
        if (vitesseHorizontale.magnitude > vitesseMouvement)
        {
            Vector3 vitesseLimitee = vitesseHorizontale.normalized * vitesseMouvement;
            rigidbodyJoueur.linearVelocity = new Vector3(vitesseLimitee.x, rigidbodyJoueur.linearVelocity.y, vitesseLimitee.z);
        }
    }

    void Sauter()
    {
        print("Sauter !");
        // On reinitialise la vitesse Y pour que chaque saut ait la meme force
        rigidbodyJoueur.linearVelocity = new Vector3(rigidbodyJoueur.linearVelocity.x, 0f, rigidbodyJoueur.linearVelocity.z);

        rigidbodyJoueur.AddForce(transform.up * forceSaut, ForceMode.Impulse);
    }
}