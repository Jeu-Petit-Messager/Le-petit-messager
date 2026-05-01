using UnityEngine;

public class XavierCameraJoueur : MonoBehaviour
{
    public Transform target;

    public float vitesseRotation = 3f;
    public float amortissement = 4f;

    public Vector3 offset = new Vector3(0f, 1.4f, -2f);

    private float rotX = 0f;
    private float rotY = 0f;

    private float rotXInitial;

    void Start()
    {
        rotY = target.eulerAngles.y;
        rotX = transform.localEulerAngles.x;

        rotXInitial = rotX;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            float sourisX = Input.GetAxis("Mouse X");
            float sourisY = Input.GetAxis("Mouse Y");

            rotY += sourisX * vitesseRotation;
            rotX -= sourisY * vitesseRotation;

            rotX = Mathf.Clamp(rotX, -60f, 60f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            rotY = Mathf.LerpAngle(rotY, target.eulerAngles.y, Time.deltaTime * amortissement);
            rotX = Mathf.Lerp(rotX, rotXInitial, Time.deltaTime * amortissement);
        }

        // 🔥 rotation caméra
        Quaternion rotation = Quaternion.Euler(rotX, rotY, 0f);

        // 🔥 direction stable (distance FIXE)
        Vector3 direction = rotation * Vector3.back;

        // 🔥 position FIXE = joueur + direction * distance
        Vector3 desiredPosition = target.position + new Vector3(0f, offset.y, 0f) + direction * Mathf.Abs(offset.z);

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * amortissement);

        transform.rotation = rotation;
    }
}