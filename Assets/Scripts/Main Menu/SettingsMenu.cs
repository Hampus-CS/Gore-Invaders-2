using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [Header("Audio Mixers")]
    public AudioMixer menuAudioMixer;
    public AudioMixer gameAudioMixer;
    
    private const string MainMenuVolumePrefsKey = "MainMenuVolume";
    private const string GameVolumePrefsKey = "GameVolume";

    void Start()
    {

        // The game and menu volume.
        float savedVolume = PlayerPrefs.GetFloat(MainMenuVolumePrefsKey, 1.0f);
        float savedGameVolume = PlayerPrefs.GetFloat(GameVolumePrefsKey, 1.0f);
        SetVolume(savedVolume);
        SetGameVolume(savedGameVolume);

    }

    public void SetVolume(float MainMenuVolume)
    {
        if (menuAudioMixer != null)
        {
            menuAudioMixer.SetFloat("MainMenuVolume", MainMenuVolume);
        }
        PlayerPrefs.SetFloat(MainMenuVolumePrefsKey, MainMenuVolume);
    }

    public void SetGameVolume(float gameVolume)
    {
        if (gameAudioMixer != null)
        {
            gameAudioMixer.SetFloat("GameVolume", gameVolume);
        }
        PlayerPrefs.SetFloat(GameVolumePrefsKey, gameVolume);
    }

}
