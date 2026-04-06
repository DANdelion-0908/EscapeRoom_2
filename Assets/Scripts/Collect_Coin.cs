using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(UniqueID))]
public class Collect_Coin : MonoBehaviour
{
    AudioSource audioSource;
    UniqueID myID;

    void Start()
    {
        audioSource = GameObject.Find("CoinSFX").GetComponent<AudioSource>();
        myID = GetComponent<UniqueID>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindFirstObjectByType<LevelManager>().IncreaseCoinScore();
            audioSource.Play();

            FindFirstObjectByType<LevelManager>().RegisterCoin(myID.ID);
            Debug.Log("Coin collected with ID: " + myID.ID);

            Destroy(gameObject);
        }
    }
}