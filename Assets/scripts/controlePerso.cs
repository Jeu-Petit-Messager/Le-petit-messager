using UnityEngine;
using UnityEngine.Audio;

public class controlePerso : MonoBehaviour
{
    [Header("Vitesses")]
    public float vitesseMarche = 3f;
    public float vitesseCourse = 9f;
    public float vitesseAccroupi = 1f;
    public float vitesseRotation = 75f;

    [Header("Saut")]
    public float forceSaut = 1f;
    public float gravite = -9f;
    public bool boolSaut;
    public AudioSource sonSaut;

    public Animator animator;

    private CharacterController controller;

    private float vitesseY;
    private bool accroupi;
    public bool auSol;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        boolSaut = false;
    }

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        // ACCROUPI
        if (Input.GetKeyDown(KeyCode.LeftControl))
            accroupi = !accroupi;

        bool courir = Input.GetKey(KeyCode.LeftShift);

        // DETECTION SOL
        auSol = controller.isGrounded;

        if (auSol && vitesseY < 0)
            vitesseY = -2f; // colle au sol

        // SAUT
        if (Input.GetKeyDown(KeyCode.Space) && auSol && !accroupi)
        {
            boolSaut = true;
            Invoke("FinSaut", 1.0f);
            vitesseY = Mathf.Sqrt(forceSaut * -2f * gravite);
            animator.SetTrigger("Sauter");
        }

        // GRAVITÉ
        vitesseY += gravite * Time.deltaTime;

        // VITESSE
        float vitesse;

        if (accroupi)
            vitesse = vitesseAccroupi;
        else if (courir)
            vitesse = vitesseCourse;
        else
            vitesse = vitesseMarche;

        // MOUVEMENT
        Vector3 move = transform.forward * v * vitesse;

        // appliquer Y
        move.y = vitesseY;
        
        Vector3 futurePos = transform.position + move * Time.deltaTime;

        // CLAMP POSITION
        futurePos.x = Mathf.Clamp(futurePos.x, 176f, 342f);
        futurePos.z = Mathf.Clamp(futurePos.z, 64f, 267f);

        Vector3 finalMove = futurePos - transform.position;
        controller.Move(finalMove);

        // ROTATION
        transform.Rotate(0, h * vitesseRotation * Time.deltaTime, 0);

       // ANIMATIONS
        bool enMouvement = Mathf.Abs(v) > 0.1f;

        // MODE ACCROUPI
        if (accroupi)
        {
            animator.SetBool("IdleAccroupi", !enMouvement);
            animator.SetBool("Accroupir", enMouvement);

            // désactiver animations normales
            animator.SetBool("Idle", false);
            animator.SetBool("Marcher", false);
            animator.SetBool("Courir", false);
        }
        else
        {
            // animations normales
            animator.SetBool("Idle", !enMouvement);

            animator.SetBool("Marcher", enMouvement && !courir);

            animator.SetBool("Courir", enMouvement && courir);

            // désactiver accroupi
            animator.SetBool("IdleAccroupi", false);
            animator.SetBool("Accroupir", false);
        }
    }

    public void FinSaut()
    {
        // Jouer le son du saut
        sonSaut.Play();
        boolSaut = false;
    }
}