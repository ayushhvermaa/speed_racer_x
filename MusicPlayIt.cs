using UnityEngine;

public class MusicPlayIt : MonoBehaviour
{
    public AudioSource audioSource;

    private static MusicPlayIt instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (audioSource != null && audioSource.enabled && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}


