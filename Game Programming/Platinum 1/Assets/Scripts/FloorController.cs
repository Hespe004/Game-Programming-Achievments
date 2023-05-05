using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private AudioClip gameOverClip;
    [SerializeField] private AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        backgroundMusic.mute = true;
        SoundFXManager.instance.PlaySoundFXClip(gameOverClip, transform, 1f);
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);
    }
}
