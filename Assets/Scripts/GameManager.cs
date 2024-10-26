using UnityEngine;
using System.Collections.Generic;
using TMPro;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;
    public GameObject victoryPrefab;
    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    public GameObject deathScreen;
    public TextMeshProUGUI[] scoreText;
    public List<GameObject> hearts;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
        mysteryShip = FindObjectOfType<MysteryShip>();
        bunkers = FindObjectsOfType<Bunker>();

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        deathScreen.gameObject.SetActive(false);
        SetScore(0);
        SetLives(3);
        mysteryShip.msLives = 3;
        NewRound();
    }

    private void NewRound()
    {
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);

        for (int i = 0; i < bunkers.Length; i++)
        {
            bunkers[i].ResetBunker();
        }

        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver()
    {
        invaders.gameObject.SetActive(false);
    }

    private void SetScore(int playerScore)
    {
        score = playerScore;
        scoreText[0].text = $"Score: {score}"; // score under spelets gång.
        scoreText[1].text = $"Score: {score}"; // score i death screen.
        Debug.Log($"Score: {score}");

        if (score > 0 && score % 100 == 0)
        {
            invaders.IncreaseSpeed();
        }
    }

    private void SetLives(int lives)
    {
        // Activate lives.
        hearts[0].SetActive(true);
        hearts[1].SetActive(true);
        hearts[2].SetActive(true);
        Debug.Log($"Lives: {lives}");
    }

    public void Health()
    {
        // Liv funktion för spelaren.
        {
            lives -= 1;

            if (lives == 2)
            {
                hearts[0].SetActive(false);
            }
            else if (lives == 1)
            {
                hearts[1].SetActive(false);
            }
            else if (lives == 0)
            {
                hearts[2].SetActive(false);
                OnPlayerKilled(player);
            }

            Debug.Log($"Player lives remaining: {lives}");
        }
    }

    public void OnPlayerKilled(Player player)
    {
        
        player.gameObject.SetActive(false);
        invaders.gameObject.SetActive(false);
        mysteryShip.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(true);
        Time.timeScale = 0f;

    }

    public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);
        
        if (invader.invaderType == 1)
        {
            SetScore(score + 10);
        }
        else if (invader.invaderType == 2)
        {
            SetScore(score + 20);
        }
        else if (invader.invaderType == 3)
        {
            SetScore(score + 30);
        }
        
        if (invaders.GetInvaderCount() == 0)
        {
            //Instantiate(victoryPrefab);
            NewRound();
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        mysteryShip.gameObject.SetActive(false);
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);
            OnPlayerKilled(player);
            Time.timeScale = 0f;
        }
    }

}
