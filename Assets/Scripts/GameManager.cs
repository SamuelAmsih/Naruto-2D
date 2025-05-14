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

// You don't need to modify your GameManager's ResetLevel methods,
// but simply add a call to restart music when loading a level:

    public void LoadLevel(int world, int stage)
    {
        this.World = world;
        this.Stage = stage;
        
        // Restart music when loading a new level
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic();
        }
        
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

    // Add these new methods to your GameManager.cs

// For pausing/freezing all game elements
    public void PauseGame()
    {
        // Find and disable all enemy movement scripts using the new method
        EntityMovement[] enemies = Object.FindObjectsByType<EntityMovement>(FindObjectsSortMode.None);
        foreach (EntityMovement enemy in enemies)
        {
            enemy.enabled = false;
        }
    
        // Freeze physics objects using the new method
        Rigidbody2D[] rigidbodies = Object.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);
        foreach (Rigidbody2D rb in rigidbodies)
        {
            // Skip any specific rigidbodies you don't want to freeze
            if (rb.gameObject.CompareTag("Player") == false)
            {
                rb.linearVelocity = Vector2.zero;  // Use linearVelocity instead of velocity
                rb.angularVelocity = 0f;
                rb.simulated = false;
            }
        }
    }

// Similarly, update the ResumeGame method
    public void ResumeGame()
    {
        // Re-enable enemy scripts using the new method
        EntityMovement[] enemies = Object.FindObjectsByType<EntityMovement>(FindObjectsSortMode.None);
        foreach (EntityMovement enemy in enemies)
        {
            enemy.enabled = true;
        }
        
        // Unfreeze physics objects using the new method
        Rigidbody2D[] rigidbodies = Object.FindObjectsByType<Rigidbody2D>(FindObjectsSortMode.None);
        foreach (Rigidbody2D rb in rigidbodies)
        {
            if (rb.gameObject.CompareTag("Player") == false)
            {
                rb.simulated = true;
            }
        }
    }

// Optional method if you want to resume game state (not needed for death sequence)
    /*
    public void ResumeGame()
    {
        // Re-enable enemy scripts
        EntityMovement[] enemies = FindObjectsOfType<EntityMovement>();
        foreach (EntityMovement enemy in enemies)
        {
            enemy.enabled = true;
        }
        
        // Unfreeze physics objects
        Rigidbody2D[] rigidbodies = FindObjectsOfType<Rigidbody2D>();
        foreach (Rigidbody2D rb in rigidbodies)
        {
            if (rb.gameObject.CompareTag("Player") == false)
            {
                rb.simulated = true;
            }
        }
    }
    */
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