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

    // Denna metod anropas n�r spelet avslutas (vid spelard�d eller seger).
    public void GameOver(int score)
    {
        int playerScore = GameManager.Instance.score;

        // Kontrollera om spelarens po�ng �r tillr�ckligt h�g f�r att l�ggas till i listan �ver h�gsta po�ng.
        if (highScoreManager.IsHighScore(score))
        {
            highScoreInputUI.SetActive(true);
            nicknameInputField.text = "";
        }
        else
        {
            ReturnToMainMenu(); // G� direkt tillbaka till huvudmenyn om du inte f�r en h�g po�ng.
        }
    }

    // Metod f�r att skicka in smeknamnet och spara po�ngen.
    public void SubmitScore()
    {
        string nickname = nicknameInputField.text;

        // S�kerst�ll att spelarens smeknamne �r giltigt (allts� ej tomt och exakt 3 tecken l�ngt).
        if (!string.IsNullOrEmpty(nickname) && nickname.Length == 3)
        {
            highScoreManager.AddScore(GameManager.Instance.score, nickname);
            highScoreInputUI.SetActive(false);
            ReturnToMainMenu();
        }
        else
        {
            Debug.LogWarning("Nickname must be exactly 3 characters long."); // Om tid finns: L�gg till s� feedback f�r felaktig inmatning ges till spelaren.
        }
    }

    // Denna metod hanterar �terg�ng till huvudmenyn.
    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f;
    }
}