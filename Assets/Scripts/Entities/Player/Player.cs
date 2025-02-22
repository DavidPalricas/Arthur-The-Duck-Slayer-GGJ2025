using UnityEngine;

/// <summary>
/// The Player class is responsible for handling the player's attributes.
/// </summary>
public class Player : Entity
{
    /// <summary>
    /// The spawnPoint property is responsible for storing the player's spawn point.
    /// This property is hidden in the inspector, so it can't be modified in the Unity Editor.
    /// </summary>
    [HideInInspector]
    public Vector2 spawnPoint;

    /// <summary>
    /// The movement property is responsible for storing the player's PlayerMovement component.
    /// </summary>
    [HideInInspector]
    public PlayerMovement movement;

    public HealthBar healthBar;

    [HideInInspector]
    public bool lazerUsed = false;

    /// <summary>
    /// The Start method is called before the first frame update (Unity Method).
    /// In this method, we are setting the player's spawn point, and initializing the player's attributes.
    /// </summary>
    private void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);

        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<EntityAttack>();
        entityRigidBody = GetComponent<Rigidbody2D>();

        healthBar.SetHealthBar(maxHealth);

        animator = GetComponent<Animator>();

        entityFSM.entityProprieties = this;

        entityFSM.entitycurrentHealth = maxHealth;

      
      
    }

    
}
