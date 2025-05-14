using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Type
    {
        Scroll,
        Power,
    }

    public Type type;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Collect(other.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (type)
        {
            case Type.Scroll:
               GameManager.Instance.AddScroll();
               break;

            case Type.Power:
               player.GetComponent<Player>().Grow();
               
               break;
        }

        Destroy(gameObject);
    }
}

