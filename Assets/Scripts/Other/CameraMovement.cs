using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject player;


    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
