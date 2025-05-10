using System.Collections;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public int nextWorld = 1;
    public int nextStage = 1;
    private IEnumerator LevelCompletesequence(Transform player)
    {
        player.GetComponent<PlayerMovments>().enabled = false;

        player.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);

        GameManager.Instance.LoadLevel(nextWorld, nextStage);
    }
}
