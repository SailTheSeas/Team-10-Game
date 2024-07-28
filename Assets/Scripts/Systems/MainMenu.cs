using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject HTP;
    [SerializeField] private GameObject Credits;


    public void LoadGame()
    {
        SceneManager.LoadScene("WorldScene");
    }

    public void ToggleHTP(bool state)
    {
        HTP.SetActive(state);
    }

    public void ToggleCredits(bool state)
    {
        Credits.SetActive(state);       
    }


    public void Quit()
    {
        Application.Quit();
    }
}
