using UnityEngine;

/// <summary>
/// The EntityDeadState class is responsible for handling the logic of the dead state of the entity.
/// An entity can be an enemy or the player.+
/// </summary>
public class EntityDeadState : EntityStateBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityDeadState"/> class.
    /// </summary>
    /// <param name="entityFSM">The entity's finite state machine.</param>
    public EntityDeadState(EntityFSM entityFSM) : base(entityFSM) { }

    /// <summary>
    /// The Enter method is responsible for entering the entity's dead state.
    /// It overrides the Enter method from the base class (EntityStateBase).
    /// It is responsible for setting the entity's animator and executing the logic of the entity according to its type (enemy or player).
    /// </summary>
    public override void Enter()
    {
        // Debug.Log("Entering Dead State");

        entityFSM.entityProprieties.entityRigidBody.velocity = Vector2.zero;

        UpdateAnimator();

        if (entityFSM.entityProprieties is Enemy)
        {
            ExecuteEnemyLogic();
        }
        else
        {
            ExecutePlayerLogic();
        }
    }

    /// <summary>
    /// The Execute method is responsible for executing the logic of the entity's dead state.
    /// It overrides the Execute method from the base class (EntityStateBase).
    /// </summary>
    public override void Execute()
    {
        // Debug.Log("Executing Dead State");
    }

    /// <summary>
    /// The Exit method is responsible for executing the logic when the entity's dead state is exited.
    /// It overrides the Exit method from the base class (EntityStateBase).
    /// </summary>
    public override void Exit()
    {
        // Debug.Log("Exiting Dead State");
    }

    /// <summary>
    /// The ExecuteEnemyLogic method is responsible for executing the logic of the enemy entity.
    /// It overrides the ExecuteEnemyLogic method from the base class (EntityStateBase).
    /// It starts to increment the number of skeletons kill and if the enemy is a boss it increments the number of boss killed and drops its item.
    /// After thath a coroutine is started to wait for the end of the death animation and create the enemy's dead body.
    /// </summary>
    protected override void ExecuteEnemyLogic()
    {
        Enemy enemyClass = (Enemy)entityFSM.entityProprieties;

        if (enemyClass.isBoss)
        {    
            if (enemyClass.bossDropItem != null)
            {
                entityFSM.InstantiateItem(enemyClass.bossDropItem, enemyClass.transform.position);
            }
        }

        entityFSM.DestroyGameObject(enemyClass.gameObject);
    }

    /// <summary>
    /// The ExecutePlayerLogic method is responsible for executing the logic of the player entity.
    /// It overrides the ExecutePlayerLogic method from the base class (EntityStateBase).
    /// It increments the number of deaths of the player and calls the RespawnPlayer  method to respawn the player.
    /// </summary>
    protected override void ExecutePlayerLogic()
    {  
        RespawnPlayer();
    }

    /// <summary>
    /// The RespawnPlayer method is responsible for respawning the player.
    /// It waits for 4 seconds and then sets the player's health to the maximum value, updates the health bar, and sets the player's position to the spawn point.
    /// </summary>
    private void RespawnPlayer()
    {
        entityFSM.StartCoroutine(Utils.Wait(3f, () =>
        {    
            Player player = entityFSM.entityProprieties as Player;

            player.entityFSM.entitycurrentHealth = player.maxHealth;

            player.GetComponent<SpriteRenderer>().color = Color.white;

            player.entityRigidBody.transform.position = player.spawnPoint;

            entityFSM.ChangeState(new EntityIdleState(entityFSM));
        }));
    }
}
