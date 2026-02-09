using UnityEngine;

public class Collect_Coin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<LevelManager>().IncreaseCoinScore();
            Destroy(gameObject);
        }
    }
}
