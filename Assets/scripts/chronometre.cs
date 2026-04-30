using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class chronometre : MonoBehaviour
{
    public float tempsRestant = 300f;// 10 minutes
    public Text texteChrono;
    public GameObject canvasChrono;

    private bool estActif = false;
    private bool estEnPause = false;

    void Start()
    {
       // Vérifie si on est dans SceneJeu
        if (SceneManager.GetActiveScene().name == "sceneJeu"
            || SceneManager.GetActiveScene().name == "sceneJeuCopy"
            || SceneManager.GetActiveScene().name == "sceneXavierTuto")
        {
            ActiverChrono();
        }
        else
        {
            // cacher le chrono si pas dans la bonne scène
            if (canvasChrono != null)
                canvasChrono.SetActive(false);
        }
    }

    void Update()
    {
        if (!estActif || estEnPause) return;

        tempsRestant -= Time.deltaTime;

        if (tempsRestant <= 0)
        {
            tempsRestant = 0;
            estActif = false;

            Debug.Log("Temps écoulé !");
        }

        AfficherTemps(tempsRestant);
    }

    void AfficherTemps(float temps)
    {
        int minutes = Mathf.FloorToInt(temps / 60);
        int secondes = Mathf.FloorToInt(temps % 60);
        // Affiche le temps au format mm:ss 
        texteChrono.text = string.Format("{0:00}:{1:00}", minutes, secondes);
    }

    void ActiverChrono()
    {
        if (canvasChrono != null)
            canvasChrono.SetActive(true);

        estActif = true;
    }
    public void MettreEnPause()
    {
        estEnPause = true;
    }

    public void Reprendre()
    {
        estEnPause = false;
    }
}