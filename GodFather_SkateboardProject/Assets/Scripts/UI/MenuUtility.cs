using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuUtility : MonoBehaviour
{
    public static MenuUtility instance;
    [SerializeField]
    private bool canPause;

    [SerializeField]
    private GameObject PauseMenu;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    

    public void SetPause()
    {
        if (!canPause) return;
        
        IsPaused = !IsPaused;
        print(IsPaused);
        MultiplayerManager.instance.SetPause(IsPaused);
        PauseMenu.SetActive(IsPaused);
        Time.timeScale = IsPaused ? 0 : 1;
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
