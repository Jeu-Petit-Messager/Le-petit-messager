using UnityEngine;

/* Script des controles du joueur */
public class XavierControlesJoueur : MonoBehaviour
{
    [Header("Reglages du mouvement")]
    public float vitesseAvant;
    public float vitesseAvantMax;
    public float forceAcceleration;
    public float vitesseRotation;
    public float forceRotation;

    Rigidbody rigidbodyJoueur;

    void Start()
    {
        rigidbodyJoueur = GetComponent<Rigidbody>();
    }

    void Update()
    {

        /* Gerer le d placement avant/arriere du joueur */
        if (Input.GetKey(KeyCode.W) && vitesseAvant < vitesseAvantMax)
        {
            
            vitesseAvant += forceAcceleration;
        }
        if (Input.GetKey(KeyCode.S) && vitesseAvant > -vitesseAvantMax)
        {
            
            vitesseAvant -= forceAcceleration;
        }

        /* Gerer la rotation du joueur */
        vitesseRotation = Input.GetAxis("Horizontal") * forceRotation;
    }

    void FixedUpdate()
    {
        // Appeler la fonction de gestion du mouvement
        //BougerJoueur();

        // Avancer et reculer le tank
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddRelativeForce(0f, 0f, vitesseAvant);

        }
        // Aucun input = vitesseAvant est reinitialise
        else
        {
            if (vitesseAvant != 0) vitesseAvant = 0f;
        }

        // Tourner le joueur
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddRelativeTorque(0f, vitesseRotation, 0f);
        }

    }

    void Sauter()
    {
        print("Sauter !");

    }
}