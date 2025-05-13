using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}
    
    [Header("---------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------- Audio Clip------------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip kagenojutsu;
    public AudioClip rasengan;
    public AudioClip rasengan2;
    public AudioClip jump;
    public AudioClip powerdown_1;
    public AudioClip powerdown_1_2;
    public AudioClip powerup;

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();

    }

    public void PlaySFX(AudioClip Clip)
    {
        SFXSource.PlayOneShot(Clip);
    }

   // Replace the existing PauseMusic() method with this:
    public void StopMusic()
    {
        // Completely stop the music instead of pausing it
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    // Replace the existing RestartMusic() method with this:
    public void PlayMusic()
    {
        // Start the appropriate music track from the beginning
        if (musicSource != null)
        {
            musicSource.Play();
        }
    }

    #if UNITY_EDITOR
    public void SetBackgroundManually()
    {
        musicSource.clip = background;
    }
    #endif


}
