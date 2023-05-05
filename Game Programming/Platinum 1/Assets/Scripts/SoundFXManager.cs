using UnityEngine;

public class SoundFXManager : MonoBehaviour {
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume) {
        //Spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //Assign the audioClip
        audioSource.clip = audioClip;

        //Assign volume
        audioSource.volume = volume;

        //Play sound
        audioSource.Play();

        //get length of sound FX clip
        float clipLength = audioSource.clip.length;

        //destroy the clip after its done playing
        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlayRandomSoundFXClip(AudioClip[] audioClip, Transform spawnTransform, float volume) {
        //asssign a random index
        int random = Random.Range(0, audioClip.Length);

        //Spawn in gameObject
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        //Assign the audioClip
        audioSource.clip = audioClip[random];

        //Assign volume
        audioSource.volume = volume;

        //Play sound
        audioSource.Play();

        //get length of sound FX clip
        float clipLength = audioSource.clip.length;

        //destroy the clip after its done playing
        Destroy(audioSource.gameObject, clipLength);
    }
}