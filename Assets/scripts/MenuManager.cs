using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject controls;
    public GameObject credits;
    public GameObject quitter;

    public GameObject[] hoverImages;

    void Start()
    {
        menu.SetActive(true);
        controls.SetActive(false);
        credits.SetActive(false);
        quitter.SetActive(false);
    }

    void Update()
    {
        // Permet de revenir au menu principal en appuyant sur la touche "Echap" depuis les sous-menus
        if (controls.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            RetourMenu();
        }
        else if (credits.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            RetourMenu();
        }
        else if (quitter.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            RetourMenu();
        }
    }
       
    // Méthodes pour ouvrir l'interface des contrôles
    public void OpenControls()
    {
        ResetHover(); 

        menu.SetActive(false);
        controls.SetActive(true);
    }
    // Méthodes pour ouvrir l'interface des crédits
    public void OpenCredits()
    {
        ResetHover(); 

        menu.SetActive(false);
        credits.SetActive(true);
    }
    // Méthodes pour ouvrir l'interface de quitter le jeu
    public void OpenQuitter()
    {
        ResetHover();

        menu.SetActive(false);
        quitter.SetActive(true);
    }
    // Méthode pour revenir au menu principal depuis les sous-menus
    public void RetourMenu()
    {
        quitter.SetActive(false);
        credits.SetActive(false);
        controls.SetActive(false);
        menu.SetActive(true);
    }
    // Méthode pour réinitialiser les images de survol (hover) en les désactivant
    void ResetHover()
    {
        foreach (GameObject img in hoverImages)
        {
            img.SetActive(false);
        }
    }
}