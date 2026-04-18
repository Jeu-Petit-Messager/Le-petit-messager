using UnityEngine;

public class XavierControlesJoueur : MonoBehaviour
{
    [Header("Déplacement")]
    public float vitesseMax = 2f;
    public float rotationSpeed = 50f;

    [Header("Accroupi")]
    public float hauteurNormal = 2f;
    public float hauteurAccroupi = 1f;

    private Rigidbody rb;
    private CapsuleCollider col;
    private Animator anim;

    private bool estAccroupi;
    private bool enCourant;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        col.height = hauteurNormal;

        rb.useGravity = true;
        rb.isKinematic = false;
    }

    void Update()
    {
        // Accroupi
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            estAccroupi = !estAccroupi;
            col.height = estAccroupi ? hauteurAccroupi : hauteurNormal;
        }

        // Course
        enCourant = Input.GetKey(KeyCode.LeftShift) && !estAccroupi;

        GérerAnimations();
    }

    void FixedUpdate()
    {
        float inputZ = Input.GetAxis("Vertical");
        float inputX = Input.GetAxis("Horizontal");

        float vitesse = vitesseMax;

        if (enCourant)
            vitesse *= 1.5f;

        if (estAccroupi)
            vitesse *= 0.5f;

        // Mouvement
        Vector3 move = transform.forward * inputZ * vitesse * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // Rotation
        transform.Rotate(Vector3.up * inputX * rotationSpeed * Time.fixedDeltaTime);
    }

    void GérerAnimations()
    {
        float vitesse = Mathf.Abs(Input.GetAxis("Vertical"));

        anim.SetBool("Marcher", vitesse > 0.1f && !enCourant && !estAccroupi);
        anim.SetBool("Courir", vitesse > 0.1f && enCourant);
        anim.SetBool("Accroupir", estAccroupi);
    }
}