using System.Runtime.CompilerServices;
using UnityEngine;

public class SideScrolling : MonoBehaviour
{
   private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    //movements of the camera settings
    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); // the camera only goes to the right and never to the left
        transform.position = cameraPosition;
    }
}
