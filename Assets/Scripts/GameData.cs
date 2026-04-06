using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int coinsCount;            // 1. int (Cantidad de monedas)
    public float playerStamina;       // 2. float (Estamina del jugador)
    public List<string> collectedIDs; // 3. List<string> (IDs de objetos recogidos)
    public string saveTimestamp;      // 4. string (Marca de tiempo del guardado)
    public bool isPaused;             // 5. bool (Indica si el juego está pausado)
    public float pPositionX;            // 6. float (Posición X del jugador)
    public float pPositionY;            // 7. float (Posición Y del jugador)
    public float pPositionZ;            // 8. float (Posición Z del jugador)

    public GameData()
    {
        collectedIDs = new List<string>();
    }
}