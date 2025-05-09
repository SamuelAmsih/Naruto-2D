using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singelton instance
    public static GameManager Instance {get; private set; }

    public int world {get; private set; } 
    public int stage {get; private set; }
    public int lives {get; private set; }
    private void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
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
        lives = 2;

        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if (lives > 0) {
            LoadLevel(world, stage);
        } else {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("Game Over");

        Invoke(nameof(NewGame), 5f);
    }
}