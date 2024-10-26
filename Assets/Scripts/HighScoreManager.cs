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


    // Anv�nder DontDestroyOnLoad f�r att beh�lla denna objektet mellan scenbyten.
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

    // Befintliga po�ng laddas fr�n filen n�r spelet startar.
    void Start()
    {
        LoadScores();
    }

    /// <summary>
    /// Metod som laddar high scores fr�n en XML-fil.
    /// Kontrollera om filen med high scores existerar.
    /// Skapa en serializer f�r att kunna l�sa in listan fr�n XML-format.
    /// �ppnar filen och deserialisera inneh�llet till en lista med high scores.
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
    /// F�ljande �r en metod f�r att spara nuvarande high scores till en XML-fil.
    /// Skapar eller skriver �ver filen med high scores.
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
    /// Metod f�r att l�gga till en ny po�ng och sortera listan med high scores.
    /// Skapar en ny high score-post med po�ng och namn. F�r att sedan l�gga till posten i listan.
    /// Sortera listan i fallande ordning (h�gsta po�ngen f�rst).
    /// Om listan �verstiger max antal (5), ta bort den sista posten.
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
    /// Metod f�r att kontrollera om en ny po�ng kvalificerar sig som en high score.
    /// Kollar om listan har f�rre �n maxScores eller om po�ngen �r h�gre �n den l�gsta high scoren.
    /// En check f�r om po�ngen kvalificerar eller inte.
    /// </summary>


    public bool IsHighScore(int score)
    {
        if (highScores.Count < maxScores || score > highScores[maxScores - 1].Score)
        {
            return true;
        }
        return false;
    }

    // Privat metod f�r att h�mta filens s�kv�g d�r high scores sparas.
    private string GetFilePath()
    {
        // Kombinerar spelets persistenta datamapp och filnamnet f�r att f� filens fulla s�kv�g.
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}