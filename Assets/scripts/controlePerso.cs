using UnityEngine;

public class controlePerso : MonoBehaviour
{
    public float vitesseMarche = 3f;
    public float vitesseCourse = 6f;
    public float vitesseAccroupi = 3f;
    public float forceDeplacement = 10f;
    public float forceSaut = 5f;

    public float vitesseRotation = 120f;

    public Rigidbody rb;
    public Animator animator;

    public float distanceSol;
    public LayerMask masqueSol;

    private bool accroupi = false;
    public bool auSol;

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        // ACCROUPI
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            accroupi = !accroupi;
        }

        bool courir = Input.GetKey(KeyCode.LeftShift);

        // DETECTION SOL
        auSol = Physics.Raycast(transform.position, Vector3.down, distanceSol, masqueSol);

        // SAUT (fix)
        if (Input.GetKeyDown(KeyCode.Space) && auSol && !accroupi)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * forceSaut, ForceMode.Impulse);

            animator.SetTrigger("Sauter");
        }

        // VITESSE
        float vitesse;

        if (accroupi)
            vitesse = vitesseAccroupi;
        else if (courir)
            vitesse = vitesseCourse;
        else
            vitesse = vitesseMarche;

        // DEPLACEMENT
        Vector3 force = transform.forward * v * vitesse * forceDeplacement;
        rb.AddForce(force);

        // ROTATION
        transform.Rotate(0, h * vitesseRotation * Time.deltaTime, 0);

        // ANIMATIONS
        bool enMouvement = Mathf.Abs(v) > 0.1f;

        animator.SetBool("Accroupir", accroupi);
        animator.SetBool("Courir", enMouvement && courir && !accroupi);
        animator.SetBool("Marcher", enMouvement && !courir && !accroupi);
    }

    void FixedUpdate()
    {
        // LIMITE SIMPLE (version propre avec Rigidbody)
        Vector3 pos = rb.position;

        // pos.x = Mathf.Clamp(pos.x, 176f, 342f);
        pos.x = Mathf.Clamp(pos.x, 275f, 342f);
        pos.y = Mathf.Clamp(pos.y, 90f, 110f);
        pos.z = Mathf.Clamp(pos.z, 62f, 267f);

        rb.position = pos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanceSol);
    }
}