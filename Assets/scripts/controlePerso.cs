// using UnityEngine;

// public class controlePerso : MonoBehaviour
// {
//     public float vitesseMarche = 3f;
//     public float vitesseCourse = 6f;
//     public float vitesseAccroupi = 3f;
//     public float forceDeplacement = 10f;
//     public float forceSaut = 5f;

//     public float vitesseRotation = 120f;

//     public Rigidbody rb;
//     public Animator animator;

//     public float distanceSol = 1.2f;
//     public LayerMask masqueSol;

//     private bool accroupi = false;
//     private bool auSol;

//     RaycastHit hitSol;

//     void Start()
//     {
//         rb.useGravity = false;
//         rb.freezeRotation = true;
//         rb.linearDamping = 2f;
//     }

//     void Update()
//     {
//         float v = Input.GetAxis("Vertical");
//         float h = Input.GetAxis("Horizontal");

//         if (Input.GetKeyDown(KeyCode.LeftControl))
//             accroupi = !accroupi;

//         bool courir = Input.GetKey(KeyCode.LeftShift);

//         // DETECTION SOL + NORMALE
//         if (Physics.Raycast(transform.position, Vector3.down, out hitSol, distanceSol, masqueSol))
//         {
//             auSol = true;
//         }
//         else
//         {
//             auSol = false;
//         }

//         // SAUT
//         if (Input.GetKeyDown(KeyCode.Space) && auSol && !accroupi)
//         {
//             rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
//             rb.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);

//             animator.SetTrigger("Sauter");
//         }

//         // VITESSE
//         float vitesse;

//         if (accroupi)
//             vitesse = vitesseAccroupi;
//         else if (courir)
//             vitesse = vitesseCourse;
//         else
//             vitesse = vitesseMarche;

//         // DEPLACEMENT SUR PENTE
//         if (auSol)
//         {
//             Vector3 direction = Vector3.ProjectOnPlane(transform.forward, hitSol.normal).normalized;
//             Vector3 force = direction * v * vitesse * forceDeplacement;

//             rb.AddForce(force);
//         }

//         // ROTATION
//         transform.Rotate(0, h * vitesseRotation * Time.deltaTime, 0);

//         // ANIMATIONS
//         bool enMouvement = Mathf.Abs(v) > 0.1f;

//         animator.SetBool("Accroupir", accroupi);
//         animator.SetBool("Courir", enMouvement && courir && !accroupi);
//         animator.SetBool("Marcher", enMouvement && !courir && !accroupi);
//     }

//     void FixedUpdate()
//     {
//         Vector3 pos = rb.position;

//         // pos.x = Mathf.Clamp(pos.x, 176f, 342f);
//         pos.x = Mathf.Clamp(pos.x, 275f, 342f);
//         pos.z = Mathf.Clamp(pos.z, 62f, 267f);

//         rb.position = pos;
//     }

//     void OnDrawGizmos()
//     {
//         Gizmos.color = Color.red;
//         Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanceSol);
//     }
// }

using UnityEngine;

public class controlePerso : MonoBehaviour
{
    [Header("Vitesses")]
    public float vitesseMarche = 3f;
    public float vitesseCourse = 9f;
    public float vitesseAccroupi = 1f;
    public float vitesseRotation = 37f;

    [Header("Saut")]
    public float forceSaut = 2f;
    public float gravite = -9.81f;
    public bool boolSaut;

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
        // pos.x = Mathf.Clamp(pos.x, 176f, 342f);
        futurePos.x = Mathf.Clamp(futurePos.x, 275f, 342f);
        futurePos.z = Mathf.Clamp(futurePos.z, 62f, 267f);

        Vector3 finalMove = futurePos - transform.position;
        controller.Move(finalMove);

        // ROTATION
        transform.Rotate(0, h * vitesseRotation * Time.deltaTime, 0);

        // ANIMATIONS
        bool enMouvement = Mathf.Abs(v) > 0.1f;

        animator.SetBool("Accroupir", accroupi);
        animator.SetBool("Courir", enMouvement && courir && !accroupi);
        animator.SetBool("Marcher", enMouvement && !courir && !accroupi);
        animator.SetBool("Idle", !enMouvement && !accroupi);
    }

    public void FinSaut()
    {
        boolSaut = false;
    }
}