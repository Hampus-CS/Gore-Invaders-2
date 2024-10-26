using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int gameScene;

    [Header("Music Manager")]
    public MainMenuMusicManager musicManager;
    
    // Stänger av huvudmeny musiken när spelaren trycker play, samt byter till game scene.
    public void Play()
    {

        if (musicManager != null)
        {

            musicManager.StopMainMenuMusic();
            SceneManager.LoadScene(gameScene);

        }

    }

    // Spelaren går ut spelet.
    public void Quit()
    {

        Debug.Log("Player has quit the game");
        Application.Quit();

    }

}