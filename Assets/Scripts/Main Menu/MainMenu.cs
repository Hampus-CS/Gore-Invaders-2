using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int gameScene;

    [Header("Music Manager")]
    public MainMenuMusicManager musicManager;
    
    // St�nger av huvudmeny musiken n�r spelaren trycker play, samt byter till game scene.
    public void Play()
    {

        if (musicManager != null)
        {

            musicManager.StopMainMenuMusic();
            SceneManager.LoadScene(gameScene);

        }

    }

    // Spelaren g�r ut spelet.
    public void Quit()
    {

        Debug.Log("Player has quit the game");
        Application.Quit();

    }

}