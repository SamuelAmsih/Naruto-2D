using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer.Search;
using UnityEngine;

public class DeathhBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            GameManager.Instance.ResetLevel(5f);
        } else {
            Destroy(other.gameObject);
        }
    }
}
