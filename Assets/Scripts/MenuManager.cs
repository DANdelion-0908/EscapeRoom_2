using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    [SerializeField] private Button myButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private string levelName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myButton.onClick.AddListener(() => LoadLevel(levelName));
        closeButton.onClick.AddListener(CloseGame);
    }

    // Update is called once per frame
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    void CloseGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
