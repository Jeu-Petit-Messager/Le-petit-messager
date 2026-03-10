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
    }

    void Update()
    {
        if (controls.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            RetourMenu();
        }else if (credits.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            RetourMenu();
        }
    }

    public void OpenControls()
    {
        ResetHover(); 

        menu.SetActive(false);
        controls.SetActive(true);
    }

    public void OpenCredits()
    {
        ResetHover(); 

        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void OpenQuitter()
    {
        ResetHover();

        menu.SetActive(false);
        quitter.SetActive(true);
    }

    public void RetourMenu()
    {
        quitter.SetActive(false);
        credits.SetActive(false);
        controls.SetActive(false);
        menu.SetActive(true);
    }

    void ResetHover()
    {
        foreach (GameObject img in hoverImages)
        {
            img.SetActive(false);
        }
    }
}