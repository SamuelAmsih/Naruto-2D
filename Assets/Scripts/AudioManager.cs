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

}
