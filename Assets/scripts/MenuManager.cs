using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject controls;

    void Start()
    {
        menu.SetActive(true);
        controls.SetActive(false);
    }

    void Update()
    {
        // Si on appuie sur Échap et que le menu contrôle est ouvert
        if (controls.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            RetourMenu();
        }
    }

    // Quand on clique sur le bouton "Contrôles"
    public void OpenControls()
    {
        menu.SetActive(false);
        controls.SetActive(true);
    }

    // Quand on clique sur "Retourner"
    public void RetourMenu()
    {
        controls.SetActive(false);
        menu.SetActive(true);
    }
}