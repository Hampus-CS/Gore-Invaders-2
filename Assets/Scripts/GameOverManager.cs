using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private HighScoreManager highScoreManager;
    public GameObject highScoreInputUI;
    public TMP_InputField nicknameInputField;
    public int mainMenu;

    private void Awake()
    {
        highScoreManager = FindObjectOfType<HighScoreManager>();

        // Ger en varning om ingen highScoreManager hittas.
        if (highScoreManager == null)
        {
            Debug.LogError("HighScoreManager not found in the scene!");
        }
    }

    // Denna metod anropas när spelet avslutas (vid spelardöd eller seger).
    public void GameOver(int score)
    {
        int playerScore = GameManager.Instance.score;

        // Kontrollera om spelarens poäng är tillräckligt hög för att läggas till i listan över högsta poäng.
        if (highScoreManager.IsHighScore(score))
        {
            highScoreInputUI.SetActive(true);
            nicknameInputField.text = "";
        }
        else
        {
            ReturnToMainMenu(); // Gå direkt tillbaka till huvudmenyn om du inte får en hög poäng.
        }
    }

    // Metod för att skicka in smeknamnet och spara poängen.
    public void SubmitScore()
    {
        string nickname = nicknameInputField.text;

        // Säkerställ att spelarens smeknamne är giltigt (alltså ej tomt och exakt 3 tecken långt).
        if (!string.IsNullOrEmpty(nickname) && nickname.Length == 3)
        {
            highScoreManager.AddScore(GameManager.Instance.score, nickname);
            highScoreInputUI.SetActive(false);
            ReturnToMainMenu();
        }
        else
        {
            Debug.LogWarning("Nickname must be exactly 3 characters long."); // Om tid finns: Lägg till så feedback för felaktig inmatning ges till spelaren.
        }
    }

    // Denna metod hanterar återgång till huvudmenyn.
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}