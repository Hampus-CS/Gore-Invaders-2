using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    public int mainMenu;
    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }

    }
    
    void Pause()
    {

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

    public void Resume ()
    {

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;

    }
    
    public void QuitToTitle()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);

    }

}
