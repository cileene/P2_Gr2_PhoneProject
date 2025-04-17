using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip sound)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.Play();
        Destroy(audioSource, sound.length);
    }
}