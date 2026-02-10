using TMPro;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class LevelManager : MonoBehaviour
{
    [Tooltip("Current coin score of the player")]
    private int coinScore = 0;

    [Tooltip("Current stamina of the player")]
    public float stamina = 100;

    [Tooltip("Is the game currently paused?")]
    public bool isPaused = false;

    [SerializeField] private TextMeshProUGUI coinScoreText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private ThirdPersonController tpCon;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button QuitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinScoreText.text = "Coins: " + coinScore;
        staminaText.text = "Stamina: " + stamina;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.pKey.wasPressedThisFrame)
        {
            TogglePause();
        }

        ResumeButton.onClick.AddListener(TogglePause);
        QuitButton.onClick.AddListener(() => GameObject.Find("MenuManager").GetComponent<MenuManager>().LoadLevel("MainMenu"));
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0.0f : 1.0f;  
        PauseMenu.SetActive(isPaused);
        tpCon.enabled = !isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
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
}
