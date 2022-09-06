using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUtility : MonoBehaviour
{
    [SerializeField]
    private bool canPause;

    [SerializeField]
    private GameObject PauseMenu;

    [SerializeField]
    private KeyCode keyPause;
    private bool isPaused;

    private void Update()
    {
        if(canPause)
        if (Input.GetKeyDown(keyPause))
        {
            SetPause();
        }
    }

    public void SetPause()
    {
        isPaused = !isPaused;
        PauseMenu.SetActive(isPaused);
        if (isPaused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;        
    }

    public void LaunchScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
