using UnityEngine;
using UnityEngine.SceneManagement;

public class nouvellePartie : MonoBehaviour
{
    public void nouvellescene()
    {
        SceneManager.LoadScene("sceneJeu");
    }
    public void changerCamera()
    {
        DesactiveCameraIntro.instance.introCamera.gameObject.SetActive(false);
        DesactiveCameraIntro.instance.jeuCamera.gameObject.SetActive(true);
    }
}