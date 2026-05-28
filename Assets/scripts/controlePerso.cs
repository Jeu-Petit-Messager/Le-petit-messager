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

    public static bool entrerLampadaire = false;
    public GameObject boite;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        boolSaut = false;

        entrerLampadaire = false;
    }

    void Update()
    {
        if (XavierAffichageTextes.bloqueDeplacement)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Marcher", false);
            animator.SetBool("Courir", false);
            animator.SetBool("IdleAccroupi", false);
            animator.SetBool("Accroupir", false);
            return;
        }
        
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if(XavierAffichageTextes.lampeTexteAffiche == false)
        {
            if(boite.name != "bloc") boite.name = "bloc";
        }

        // ACCROUPI
        if (Input.GetKeyDown(KeyCode.LeftControl))
            accroupi = !accroupi;

        // changer CharacterController
        if (accroupi)
        {
            controller.height = 1f;
            controller.center = new Vector3(0f, 0.67f, 0f);
        }
        else
        {
            controller.height = 1.55f;
            controller.center = new Vector3(0f, 0.9f, 0f);
        }

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
        futurePos.z = Mathf.Clamp(futurePos.z, 66f, 267f);

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

    /* pour le bloc lampadaire */
    public void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (hit.gameObject.name == "bloc")
        {

            if (entrerLampadaire == false)
            {
                if(!XavierAffichageTextes.lampeTexteAffiche) entrerLampadaire = true;
                hit.gameObject.name = "bloc2";
            }
            
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "canetteTour")
        {
            if(XavierZoneCanetteProg.canetteCollectees < 1)
            {
                if (!XavierAffichageTextes.estEnTrainDEcrire)
                {
                    XavierAffichageTextes.canTexteAffiche = true;
                }
            }
        }
    }
}