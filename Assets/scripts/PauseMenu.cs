using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "sceneJeuJour" && SceneManager.GetActiveScene().name != "sceneJeuNuit")
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && pauseMenuUI.activeSelf)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        // Désélectionne tout élément sélectionné pour éviter les problèmes de navigation
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        // Désélectionne tout élément sélectionné pour éviter les problèmes de navigation
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void QuitGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("SceneIntro");
    }
}