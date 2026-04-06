using UnityEngine;

public class UniqueID : MonoBehaviour
{
    public string ID;

    [ContextMenu("Generar ID")]
    public void GenerateID()
    {
        ID = System.Guid.NewGuid().ToString();

        #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    } 
}