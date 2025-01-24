using UnityEngine;
/// <summary>
/// The DoorTrigger class is used to open the door when the player presses the E key.
/// </summary>
public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Destroy(door);
        }
    }
}
