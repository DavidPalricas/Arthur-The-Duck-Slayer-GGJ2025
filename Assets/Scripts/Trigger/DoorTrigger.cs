using UnityEngine;
/// <summary>
/// The DoorTrigger class is used to open the door when the player presses the E key.
/// </summary>
public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    private bool playerEntered;


    private void Update()
    {
        if (playerEntered && Input.GetKeyDown(KeyCode.E)) {
            AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            audioManager.PlaySFX(audioManager.doorBreak);
            Destroy(door);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEntered = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerEntered = true;
        }
    }
}
