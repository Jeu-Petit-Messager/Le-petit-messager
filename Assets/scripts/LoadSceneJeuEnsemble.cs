using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneJeuEnsemble : MonoBehaviour
{
    // Méthode appelée au démarrage du jeu pour charger la scène de jeu en mode additive
    void Start()
    {
        SceneManager.LoadScene("sceneJeuNuit", LoadSceneMode.Additive);
    }
}