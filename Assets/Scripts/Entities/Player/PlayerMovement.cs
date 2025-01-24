using UnityEngine;

/// <summary>
/// The PlayerMovement class is responsible for handling the player's movement.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// The player property is responsible for storing the player's Rigidbody2D component.
    /// </summary>
    private Rigidbody2D player;

    /// <summary>
    /// The speed property is responsible for storing the player's speed.
    /// </summary>
    [HideInInspector]
    public float speed;

    /// <summary>
    /// The speed property is responsible for storing the player's speed.
    /// </summary>
    [HideInInspector]
    public Vector2 speedVector;

    /// <summary>
    /// The Start method is called before the first frame update (Unity Method).
    /// </summary>
    private void Start()
    {
        player = GetComponent<Rigidbody2D>();

        speed = GetComponent<Entity>().speed;

        EntityFSM entityFSM = GetComponent<Player>().entityFSM;

        entityFSM.ChangeState(new EntityIdleState(entityFSM));
    }

    /// <summary>
    /// The MovePlayer method is responsible for moving the player.
    /// </summary>
    public void MovePlayer()
    {
        // Create a movement vector and normalize it
        speedVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Apply velocity directly to the Rigidbody2D
        player.velocity = speed * speedVector;
    }

    /// <summary>
    /// The StopPlayer method is responsible for stopping the player.
    /// </summary>
    public void StopPlayer()
    {
        if (speedVector == Vector2.zero)
        {
            player.velocity = Vector2.zero;
        }
    }
}
