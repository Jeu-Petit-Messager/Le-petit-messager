using UnityEngine;

public class controleMenu : MonoBehaviour
{
    public GameObject controlsUI;
    public GameObject pauseMenuUI;

    void Update()
    {
        // ESC → retour au menu pause
        if (controlsUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            RetourMenu();
        }
    }

    public void OpenControls()
    {
        controlsUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void RetourMenu()
    {
        controlsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}