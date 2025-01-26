using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField]
    private SpawnHorde bossHorde;

    [SerializeField]
    private BossObserver bossObserver;

    [SerializeField]
    private GameObject bossRoomDoor;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetPlayerSpawnPoint(collision.gameObject);

            bossRoomDoor.SetActive(true);

            bossHorde.enabled = true;
            bossHorde.SpawnBoss();

            bossObserver.enabled = true;
            
            Destroy(gameObject);
        }
    }

    private void SetPlayerSpawnPoint(GameObject player)
    {
        Player playerProprieties = (Player)player.GetComponent<EntityFSM>().entityProprieties;
        playerProprieties.spawnPoint = transform.position;
    }
}
