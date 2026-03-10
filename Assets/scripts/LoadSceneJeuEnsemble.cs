using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneJeuEnsemble : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("sceneJeu", LoadSceneMode.Additive);
    }
}