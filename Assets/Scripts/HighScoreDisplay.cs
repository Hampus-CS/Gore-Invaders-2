using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreDisplay : MonoBehaviour
{
    private HighScoreManager highScoreManager;

    // Definiera TextMeshPro-fält för varje poäng och smeknamn.
    public TMP_Text[] scoreTexts;  // Array för score rutorna.
    public TMP_Text[] nameTexts;   // Array för nickname rutorna.

    private void Awake()
    {
        highScoreManager = FindObjectOfType<HighScoreManager>();

        // Ger en varning om ingen highScoreManager hittas.
        if (highScoreManager == null)
        {
            Debug.LogError("HighScoreManager not found in the scene!");
        }
        else
        {
            highScoreManager.LoadScores(); // Se till att poängen laddas vid start.
        }
    }

    void Start()
    {
        DisplayHighScores(); // Ladda in highscore och visa.
    }

    public void DisplayHighScores()
    {
        // Kollar om kraven finns för att resterande ska fungera.
        if (highScoreManager == null || highScoreManager.highScores == null)
        {
            Debug.LogWarning("HighScoreManager or highScores list is null.");
            return;
        }

        if (scoreTexts == null || nameTexts == null)
        {
            Debug.LogWarning("ScoreTexts or NameTexts arrays are not assigned.");
            return;
        }

        highScoreManager.LoadScores();

        // Laddar in score och nickname.
        for (int i = 0; i < highScoreManager.highScores.Count && i < scoreTexts.Length; i++)
        {
            if (scoreTexts[i] != null && nameTexts[i] != null)
            {
                scoreTexts[i].text = highScoreManager.highScores[i].Score.ToString("00000");
                nameTexts[i].text = highScoreManager.highScores[i].Nickname;
            }
            else
            {
                Debug.LogWarning($"Text element at index {i} is null.");
            }
        }
    }
}