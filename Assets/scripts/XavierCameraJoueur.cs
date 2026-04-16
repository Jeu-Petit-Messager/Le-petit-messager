using UnityEngine;

public class XavierCameraJoueur : MonoBehaviour
{
    // Enregistrer la position du joueur
    public Transform positionJoueur;

    // La distance de la camera
    public Vector3 distance;

    // Determiner le degre d'amortissement
    public float amortissement;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Position pour suivre le joueur
        Vector3 positionFin = positionJoueur.TransformPoint(distance);

        // Mettre a jour la position de la cam
        transform.position = Vector3.Lerp(transform.position, positionFin, amortissement);

        // Maintenir l'angle de vue sur le joueur
        transform.LookAt(positionJoueur);
    }
}
