using TMPro;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Current coin score of the player")]
    private int coinScore = 0;

    [Tooltip("Current stamina of the player")]
    public float stamina = 100;

    [Tooltip("Is the game currently paused?")]
    public bool isPaused = false;

    private List<string> currentCollectedIDs = new();

    public int GetCoinScore() => coinScore;

    [SerializeField] private TextMeshProUGUI coinScoreText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private ThirdPersonController tpCon;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button SaveButton;
    [SerializeField] private Button LoadButton;
    [SerializeField] private PersistenceManager persistence;

    void Start()
    {
        coinScoreText.text = "Coins: " + coinScore;
        staminaText.text = "Stamina: " + stamina;
        ResumeButton.onClick.AddListener(TogglePause);
        SaveButton.onClick.AddListener(OnSaveButton);
        LoadButton.onClick.AddListener(OnLoadButton);

        if (MenuManager.Instance != null && MenuManager.Instance.isLoading)
        {
            OnLoadButton();
            MenuManager.Instance.isLoading = false;
        }
    }

    
    void Update()
    {
        if(Keyboard.current.pKey.wasPressedThisFrame)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0.0f : 1.0f;  
        PauseMenu.SetActive(isPaused);
        tpCon.enabled = !isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1.0f;  
        SceneManager.LoadScene("MainMenu");
    }

    public void IncreaseCoinScore()
    {
        coinScore++;
        coinScoreText.text = "Coins: " + coinScore;
        Debug.Log("Coin collected! Current score: " + coinScore);

        if (coinScore >= 5)
        {
            Destroy(GameObject.Find("Exit"));
            Debug.Log("You win!");
            SceneManager.LoadScene("WinScene");
        }
    }

    public void DecreaseStamina(float amount)
    {
        if (stamina > 0 && stamina - amount >= 0)
        {
            stamina -= amount;
            staminaText.text = "Stamina: " + (int)stamina;
        
        } else
        {
            stamina = 0;
            staminaText.text = "Stamina: " + (int)stamina;
        }
    }

    public void IncreaseStamina(float amount)
    {
        if (stamina < 100)
        {
            stamina += amount;
            staminaText.text = "Stamina: " + (int)stamina;
        }
    }

    public void RegisterCoin(string coinID)
    {
        if (!currentCollectedIDs.Contains(coinID))
        {
            currentCollectedIDs.Add(coinID);
        }
    }

    public void OnSaveButton()
    {
        GameData data = new()
        {
            
            coinsCount = GetCoinScore(),
            playerStamina = stamina,
            collectedIDs = new List<string>(currentCollectedIDs),
            isPaused = isPaused,
            pPositionX = tpCon.transform.position.x,
            pPositionY = tpCon.transform.position.y,
            pPositionZ = tpCon.transform.position.z
        };

        persistence.SaveGame(data);
        Debug.Log("Progreso guardado desde el Menú de Pausa");
    }

        public void OnLoadButton()
    {
        GameData data = persistence.LoadGame();
        if (data == null) return;

        LoadDataToUI(data.coinsCount, data.playerStamina);
        
        currentCollectedIDs = new List<string>(data.collectedIDs);

        UniqueID[] allCoins = FindObjectsByType<UniqueID>(FindObjectsSortMode.None);
        foreach (var coin in allCoins)
        {
            if (currentCollectedIDs.Contains(coin.ID))
            {
                Destroy(coin.gameObject);
            }
        }
        
        if(isPaused) TogglePause();

        tpCon.enabled = false; 
        tpCon.transform.position = new Vector3(data.pPositionX, data.pPositionY, data.pPositionZ);
        tpCon.enabled = true;

        Debug.Log("Progreso cargado desde el Menú de Inicio");
    }

    public void LoadDataToUI(int score, float stam)
    {
        coinScore = score;
        stamina = stam;
        coinScoreText.text = "Coins: " + coinScore;
        staminaText.text = "Stamina: " + (int)stamina;
    }
}
