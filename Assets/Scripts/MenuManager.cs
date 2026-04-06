using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool isLoading = false;

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
    [SerializeField] private Button loadButton;
    void Start()
    {
        myButton.onClick.AddListener(() => {
            isLoading = false;
            LoadLevel("Level1");
        });

        loadButton.onClick.AddListener(() => {
            isLoading = true;
            LoadLevel("Level1");
        });
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    void CloseGame()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
