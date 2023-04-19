using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float volume = 1f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.volume = volume;
    }

    public void SetVolume(float newVolume)
    {
        volume = newVolume;
    }
}

public class LocalVolume : MonoBehaviour
{
    public float localVolume = 1f;
    public float maxDistance = 10f;

    private AudioManager audioManager;
    private Transform player;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        float distancePercent = Mathf.Clamp01(distance / maxDistance);
        float newVolume = localVolume * (1 - distancePercent);
        audioManager.SetVolume(newVolume);
    }
}