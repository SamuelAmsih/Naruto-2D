using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singelton instance
    public static GameManager Instance {get; private set; }

    public int World {get; private set; } 
    public int Stage {get; private set; }
    public int Lives {get; private set; }
    public int Scrolls {get; private set; }

    private void Awake()
    {
        // Check if instance already exists
        if (Instance != null)
        {
            DestroyImmediate(gameObject); // Destroy this object if it is not the first instance
        } else {
            Instance = this; // Set the instance to this object
            DontDestroyOnLoad(gameObject); // Make this object persistent across scenes
        }
    }

    private void OnDestroy()
    {
        // Clean up the instance when this object is destroyed
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        Lives = 5;
        Scrolls = 0;

        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int stage)
    {
        this.World = world;
        this.Stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(World, Stage + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        Lives--;

        if (Lives > 0) {
            LoadLevel(World, Stage);
        } else {
            GameOver();
        }
    }

    private void GameOver()
    {
        NewGame();
    }

    public void AddScroll()
    {

      Scrolls++;
      
      if (Scrolls == 25)
      {
         AddLife();
         Scrolls = 0;
      }

    }

    public void AddLife()
    {
        Lives++;
    }
}