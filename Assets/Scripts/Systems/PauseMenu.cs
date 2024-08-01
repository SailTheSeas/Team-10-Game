using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private string menuScene, combatScene;

    private DataHolder DH;
    bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        DH = FindAnyObjectByType<DataHolder>();
        Time.timeScale = 1;
        isPaused = false;
        pauseScreen.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {

            PauseGame();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(combatScene);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        isPaused = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        isPaused = false;
    }

    public void LoadMainMenu()
    {
        if (DH != null)
            DH.ResetData();
        SceneManager.LoadScene(menuScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
