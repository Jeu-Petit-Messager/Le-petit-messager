using UnityEngine;
 
public class XavierCameraJoueur : MonoBehaviour
{
    public float vitesseRotation = 3f;
    public float amortissement = 5f;
 
    private float rotX = 0f;
    private float rotY = 0f;
 
    private float rotXInitial;
    private float rotYInitial;
 
    void Start()
    {
        // sauvegarde position initiale
        Vector3 angles = transform.localEulerAngles;
        rotX = angles.x;
        rotY = angles.y;
 
        rotXInitial = rotX;
        rotYInitial = rotY;
    }
 
    void Update()
    {
        if (Input.GetMouseButton(1)) // clic droit pour rotation libre
        {
            // cacher souris
            Cursor.visible = false;

            // verrouiller souris au centre
            Cursor.lockState = CursorLockMode.Locked;

            // rotation libre souris
            float sourisX = Input.GetAxis("Mouse X");
            float sourisY = Input.GetAxis("Mouse Y");
 
            rotY += sourisX * vitesseRotation;
            rotX -= sourisY * vitesseRotation;
 
            // limite verticale (évite bug tête à l’envers)
            rotX = Mathf.Clamp(rotX, -60f, 60f);
        }
        else
        {
            // montrer souris
            Cursor.visible = true;

            // débloquer souris
            Cursor.lockState = CursorLockMode.None;

            // retour automatique à position initiale
            rotX = Mathf.Lerp(rotX, rotXInitial, Time.deltaTime * amortissement);
            rotY = Mathf.LerpAngle(rotY, rotYInitial, Time.deltaTime * amortissement);
        }
 
        // appliquer rotation
        transform.localRotation = Quaternion.Euler(rotX, rotY, 0f);
    }
}