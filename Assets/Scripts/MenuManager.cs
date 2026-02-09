using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
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
    void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    void CloseGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
