using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuPage
{
    MAIN,
    SETTINGS
}

public class Menu_Main : MonoBehaviour
{
    public GameObject mainMenu, optionMenu;

    [SerializeField]
    private TMPro.TextMeshProUGUI bowHandDisplay;

    void Start()
    {
        mainMenu.SetActive(true);
        PlayerControllerProperties.CrossSceneInformation="Left";
        bowHandDisplay.text = PlayerControllerProperties.CrossSceneInformation;
    }

    public void OnClick_Option()
    {
        ChangeMenu(MenuPage.SETTINGS, mainMenu);
    }

    public void ChangeMenu(MenuPage menu, GameObject callingMenu)
    {
        switch (menu)
        {
            case MenuPage.MAIN:
                mainMenu.SetActive(true);
                break;
            case MenuPage.SETTINGS:
                optionMenu.SetActive(true);
                break;
        }
        bowHandDisplay.text = PlayerControllerProperties.CrossSceneInformation;
        callingMenu.SetActive(false);
    }

    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
