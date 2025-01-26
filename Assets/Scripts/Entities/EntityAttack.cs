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

    [SerializeField]
    private LazerCoolDownLabel lazerCoolDownLabel;

    [HideInInspector]
    public bool attacking;

    private Player player;

    private const float LAZERCOOLDOWN = 30f;

    private float currentLazerCoolDown = LAZERCOOLDOWN ;

    private void Awake()
    {
        player = GetComponent<Player>();

        if (player != null)
        {
            lazerCoolDownLabel.SetCoolDown(LAZERCOOLDOWN);
        } 
    }

    private void Update()
    {
        if (player != null && currentLazerCoolDown < LAZERCOOLDOWN)
        {
            currentLazerCoolDown += Time.deltaTime;
            currentLazerCoolDown = Mathf.Clamp(currentLazerCoolDown, 0, LAZERCOOLDOWN);
            lazerCoolDownLabel.UpdateCoolDown(currentLazerCoolDown);
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

                if (currentLazerCoolDown >= LAZERCOOLDOWN)
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

        newProjetileMovement.entityDamage = (int)GetComponent<EntityFSM>().entityProprieties.attackDamage;

        newProjetileMovement.playerThrown = GetComponent<EntityFSM>().entityProprieties is Player;

        if (GetComponent<Enemy>() != null) {

            newProjetileMovement.isBoss = GetComponent<Enemy>().isBoss;
        }
        
        HandleAttackCooldown(attackCoolDown);
    }

    private void LazerAttack(Vector2 attackDirection, float attackCoolDown)
    {
        float lazerWidth = lazer.transform.localScale.x;
        float lazerHeight = lazer.transform.localScale.y;
        Vector2 lazerPosition = (Vector2)player.transform.position + (attackDirection * lazerWidth / 1.8f);
        GameObject newLazer = Instantiate(lazer, lazerPosition, Quaternion.identity);
        SetLazerRotation(newLazer, attackDirection);

        Vector2 raycastOrigin =lazerPosition;

        Vector2 raycastSize;

        if (attackDirection == Vector2.up || attackDirection == Vector2.down)
        {
            raycastSize = new Vector2(lazerHeight, lazerWidth);
        }
        else
        {
            raycastSize = new Vector2(lazerWidth, lazerHeight);
        }

        LayerMask playerLayer = LayerMask.GetMask("Default");

        RaycastHit2D[] hits = Physics2D.BoxCastAll(raycastOrigin, raycastSize, 0f, attackDirection, lazer.transform.localScale.x, playerLayer);
        
        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.laser);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider);
                if (hit.collider.CompareTag("Enemy"))
                {
                    Debug.Log("Enemy hit");
                    hit.collider.GetComponent<EntityFSM>().entitycurrentHealth -= (int)player.attackDamage * 5;
                }
                else if (hit.collider.CompareTag("Object"))
                {
                    HandleAttackCooldown(attackCoolDown);
                    Destroy(newLazer, 0.05f);
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
