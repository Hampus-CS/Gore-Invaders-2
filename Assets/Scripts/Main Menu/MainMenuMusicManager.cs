using UnityEngine;

public class MainMenuMusicManager : MonoBehaviour
{

    AudioSource mainMenuMusic;

    // Spelar musik när spelaren går in i spelet
    void Start()
    {
        PlayMainMenuMusic();
    }

    public void PlayMainMenuMusic()
    {
        if (mainMenuMusic != null && !mainMenuMusic.isPlaying)
        {
            mainMenuMusic.Play();
        }
    }

    public void StopMainMenuMusic()
    {
        if (mainMenuMusic != null && mainMenuMusic.isPlaying)
        {
            mainMenuMusic.Stop();
        }
    }

}