using UnityEngine;

/// <summary>
/// The EntityAttack class is responsible for handling the enemy's ranged attack.
/// </summary>
public class EntityAttack : MonoBehaviour
{
    /// <summary>
    /// The projetile property is responsible for storing the projetile prefab.
    /// </summary>
    [SerializeField]
    private GameObject projetile, lazer;

    [HideInInspector]
    public bool attacking;


    private Player player;


    private float currentLazerCoolDown;


    private const float LAZERCOOLDOWN = 2f;


    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player != null && currentLazerCoolDown < 2f)
        {
            currentLazerCoolDown += Time.deltaTime;
            currentLazerCoolDown = Mathf.Clamp(LAZERCOOLDOWN, 0, 2f);
        }
    }
    /// <summary>
    /// The HandleAttackCooldown method is responsible for handling the enemy's attack cooldown.
    /// If the entity is attacking, a coroutine is started to wait 1.5 seconds and then the entity stops attacking.
    /// It overrides the HandleAttackCooldown method from the base class (EntityAttack).
    /// <param name="attackCoolDown">The attack cooldown of entity.</param>""
    /// </summary>
    private void HandleAttackCooldown(float attackCoolDown)
    {
        if (attacking)
        {
            StartCoroutine(Utils.Wait(attackCoolDown, () =>
            {
                attacking = false;
            }));
        }
    }

    /// <summary>
    /// The Attack method is responsible for handling the enemy's attack.
    ///  It overrides the Attack method from the base class (EntityAttack).
    ///  In this method, the entity's projetile is instantiated and its movement is set.
    /// </summary>
    /// <param name="attackDirection">The vector's attack direction of entity.</param>
    /// <param name="attackCoolDown">The attack cooldown of entity.</param>
    public void Attack(Vector2 attackDirection, float attackCoolDown)
    {
        if (!attacking)
        {
            attacking = true;

            if (player != null && player.lazerUsed)
            {
                player.lazerUsed = false;

                if (currentLazerCoolDown == LAZERCOOLDOWN)
                {
                    LazerAttack(attackDirection, attackCoolDown);
                    currentLazerCoolDown = 0f;
                }
                else
                {
                    attacking = false;
                    return;
                }

            }
            else
            {
                ProjetileAttack(attackDirection, attackCoolDown);
            }
        }
    }


    private void ProjetileAttack(Vector2 attackDirection, float attackCoolDown)
    {
        Rigidbody2D entityRigidBody2D = GetComponent<Rigidbody2D>();

        Vector2 projetilePosition = entityRigidBody2D.position + attackDirection;

        GameObject newProjetile = Instantiate(projetile, projetilePosition, Quaternion.identity);

        ProjetileMovement newProjetileMovement = newProjetile.GetComponent<ProjetileMovement>();

        newProjetileMovement.attackDirection = attackDirection;

        newProjetileMovement.playerThrown = GetComponent<EntityFSM>().entityProprieties is Player;

        HandleAttackCooldown(attackCoolDown);
    }

    private void LazerAttack(Vector2 attackDirection, float attackCoolDown)
    {
        float rayCastDistance = lazer.transform.localScale.x;

        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();

        // Calcula a posição na borda do collider do jogador, na direção do ataque
        Vector3 lazerPosition = playerCollider.bounds.center + (Vector3)(attackDirection.normalized * (playerCollider.bounds.extents.magnitude));

        // Cria o novo laser na posição ajustada
        GameObject newLazer = Instantiate(lazer, lazerPosition, Quaternion.identity);

        // Define a rotação do laser
        SetLazerRotation(newLazer, attackDirection);

        Vector2 raycastOrigin = (Vector2)lazerPosition;

        LayerMask playerLayer = LayerMask.GetMask("Default");

        RaycastHit2D[] hits = Physics2D.RaycastAll(raycastOrigin, attackDirection, rayCastDistance, playerLayer);

        // Linha para debug visual
        Debug.DrawRay(raycastOrigin, attackDirection.normalized * rayCastDistance, Color.blue);

        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy hit");
                    hit.collider.GetComponent<EntityFSM>().entitycurrentHealth -= (int)player.attackDamage;
                }
                else if (hit.collider.CompareTag("Object"))
                {
                    HandleAttackCooldown(attackCoolDown);
                    Destroy(newLazer, 0.1f);
                    return;
                }
            }
        }

        Destroy(newLazer, 0.1f);

        HandleAttackCooldown(attackCoolDown);
    }



    private void SetLazerRotation(GameObject newLazer,Vector2 attackDirection)
    {
        if (attackDirection == Vector2.up)
        {
            newLazer.transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (attackDirection == Vector2.down)
        {
            newLazer.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (attackDirection == Vector2.right)
        {
            newLazer.transform.rotation = Quaternion.Euler(0, 0, 180);
        }

    }
}
