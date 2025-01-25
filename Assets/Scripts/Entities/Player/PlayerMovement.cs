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
    /// The speedVector property is responsible for storing the player's movement vector.
    /// </summary>
    [HideInInspector]
    public Vector2 speedVector;


    private const float DASHCOOLDOWN = 1f;
    private float currentDashCoolDown = DASHCOOLDOWN; 
    private readonly float dashDuration = 0.2f; 
    private bool isDashing = false;
    private float dashTimer = 0f; 

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

    private void Update()
    {
        if (!isDashing && currentDashCoolDown < DASHCOOLDOWN)
        {
            currentDashCoolDown += Time.deltaTime;
            currentDashCoolDown = Mathf.Clamp(currentDashCoolDown, 0, DASHCOOLDOWN);
        }

        if (isDashing)
        {
            dashTimer += Time.deltaTime;

            if (dashTimer >= dashDuration)
            {
                isDashing = false; 
                dashTimer = 0f;
            }
        }
    }

    /// <summary>
    /// The MovePlayer method is responsible for moving the player.
    /// </summary>
    public void MovePlayer()
    {
        speedVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKey(KeyCode.LeftShift) && currentDashCoolDown >= DASHCOOLDOWN && speedVector != Vector2.zero && !isDashing)
        {
            isDashing = true; 
            currentDashCoolDown = 0f; 
            player.velocity = 4 * speed * speedVector; 
        }
        else if (!isDashing)
        {
            player.velocity = speed * speedVector;
        }
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
