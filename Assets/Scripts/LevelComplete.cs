using System.Collections;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public int nextWorld = 1;
    public int nextStage = 1;
    
    // Add this to detect when player enters the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Start the level complete sequence when player enters
            StartCoroutine(LevelCompletesequence(collision.transform));
        }
    }
    
    private IEnumerator LevelCompletesequence(Transform player)
    {
        player.GetComponent<PlayerMovments>().enabled = false;
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.LoadLevel(nextWorld, nextStage);
    }
}