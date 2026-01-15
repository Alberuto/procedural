using UnityEngine;
public class AudioManager : MonoBehaviour  {

    public static AudioManager Instance;

    [Header("Fuentes")]
    public AudioSource musicSource, sfxSource;

    [Header("Clips")]
    public AudioClip keySound, hitSound, levelUpSound, victorySound;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);  //Persiste como GameManager
        }
        else {
            Destroy(gameObject);
        }
    }
    public void PlaySFX(AudioClip clip) {

        sfxSource.PlayOneShot(clip);
    }
}