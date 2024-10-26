using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public List<HighScoreEntry> highScores;
    private const string fileName = "highscores.xml";
    private const int maxScores = 5;
    public static HighScoreManager Instance { get; private set; }


    // Använder DontDestroyOnLoad för att behålla denna objektet mellan scenbyten.
    void Awake()
    {
        Debug.Log(Application.persistentDataPath);
        // Singleton pattern to ensure only one instance of HighScoreManager.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object between scenes.
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances.
        }
    }

    // Befintliga poäng laddas från filen när spelet startar.
    void Start()
    {
        LoadScores();
    }

    /// <summary>
    /// Metod som laddar high scores från en XML-fil.
    /// Kontrollera om filen med high scores existerar.
    /// Skapa en serializer för att kunna läsa in listan från XML-format.
    /// Öppnar filen och deserialisera innehållet till en lista med high scores.
    /// </summary>

    public void LoadScores()
    {

        if (File.Exists(GetFilePath()))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<HighScoreEntry>));
            using (FileStream stream = new FileStream(GetFilePath(), FileMode.Open))
            {
                highScores = (List<HighScoreEntry>)serializer.Deserialize(stream);
            }
            Debug.Log("no list found");
        }
        else
        {
            highScores = new List<HighScoreEntry>();
        }
    }

    /// <summary>
    /// Följande är en metod för att spara nuvarande high scores till en XML-fil.
    /// Skapar eller skriver över filen med high scores.
    /// Serialisera (skriv) listan till filen.
    /// </summary>

    public void SaveScores()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<HighScoreEntry>));
        using (FileStream stream = new FileStream(GetFilePath(), FileMode.Create))
        {
            serializer.Serialize(stream, highScores);
        }
    }

    /// <summary>
    /// Metod för att lägga till en ny poäng och sortera listan med high scores.
    /// Skapar en ny high score-post med poäng och namn. För att sedan lägga till posten i listan.
    /// Sortera listan i fallande ordning (högsta poängen först).
    /// Om listan överstiger max antal (5), ta bort den sista posten.
    /// Spara de uppdaterade high scores till filen.
    /// </summary>

    public void AddScore(int newScore, string nickname)
    {
        HighScoreEntry newEntry = new HighScoreEntry(newScore, nickname);
        highScores.Add(newEntry);
        highScores.Sort((x, y) => y.Score.CompareTo(x.Score));

        if (highScores.Count > maxScores)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        SaveScores();
    }

    /// <summary>
    /// Metod för att kontrollera om en ny poäng kvalificerar sig som en high score.
    /// Kollar om listan har färre än maxScores eller om poängen är högre än den lägsta high scoren.
    /// En check för om poängen kvalificerar eller inte.
    /// </summary>


    public bool IsHighScore(int score)
    {
        if (highScores.Count < maxScores || score > highScores[maxScores - 1].Score)
        {
            return true;
        }
        return false;
    }

    // Privat metod för att hämta filens sökväg där high scores sparas.
    private string GetFilePath()
    {
        // Kombinerar spelets persistenta datamapp och filnamnet för att få filens fulla sökväg.
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}