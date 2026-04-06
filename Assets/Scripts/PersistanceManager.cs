using System.IO;
using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    private string saveFilePath;

    void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/savegame.json";
    }

    public void SaveGame(GameData data)
    {
        data.saveTimestamp = System.DateTime.Now.ToString();
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Juego guardado en: " + saveFilePath);
    }

    public GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Juego cargado correctamente.");
            return data;
        }
        
        else
        {
            Debug.LogWarning("No se encontró archivo de guardado en: " + saveFilePath);
            return null; 
        }
    }
}