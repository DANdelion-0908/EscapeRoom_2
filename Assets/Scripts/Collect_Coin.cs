using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Collect_Coin : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GameObject.Find("CoinSFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<LevelManager>().IncreaseCoinScore();
            audioSource.Play();
            Destroy(gameObject);
        }
    }
}
