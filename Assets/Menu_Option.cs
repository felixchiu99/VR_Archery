using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu_Option : MonoBehaviour
{
    public Menu_Main controller;

    public TMP_Dropdown dropDown;
    public void OnClick_Return()
    {
        PlayerControllerProperties.CrossSceneInformation = dropDown.options[dropDown.value].text;
        controller.ChangeMenu(MenuPage.MAIN, gameObject);
    }
}
