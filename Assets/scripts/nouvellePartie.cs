using UnityEngine;
using UnityEngine.SceneManagement;

public class nouvellePartie : MonoBehaviour
{
    public void nouvellescene()
    {
        SceneManager.LoadScene("sceneJeu");
    }
}