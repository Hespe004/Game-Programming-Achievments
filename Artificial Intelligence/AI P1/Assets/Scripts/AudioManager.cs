using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip winMusic;
    [SerializeField] private AudioClip loseMusic;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Play the background music by default
        PlayBackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is won or lost and play the corresponding music
        if (GameManager.Instance.gameWon)
        {
            PlayWinMusic();
        }
        else if (GameManager.Instance.gameLost)
        {
            PlayLoseMusic();
        }
        else
        {
            PlayBackgroundMusic();
        }
    }

    void PlayBackgroundMusic()
    {
        if (audioSource.clip != backgroundMusic)
        {
            audioSource.Stop();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void PlayWinMusic()
    {
        if (audioSource.clip != winMusic)
        {
            audioSource.Stop();
            audioSource.clip = winMusic;
            audioSource.loop = false;
            audioSource.Play();
        }
    }

    void PlayLoseMusic()
    {
        if (audioSource.clip != loseMusic)
        {
            audioSource.Stop();
            audioSource.clip = loseMusic;
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}
