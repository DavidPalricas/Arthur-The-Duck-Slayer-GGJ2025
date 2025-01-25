
using System.Linq;
using UnityEngine;

public class BossObserver : MonoBehaviour
{

    [SerializeField]
    private HealthBar bossHealthBar;

    private GameObject boss;
   
    private EntityFSM bossFSM;

    private void OnEnable()
    {   
        boss = FindBoss();

        bossHealthBar.gameObject.SetActive(true);

        bossFSM = boss.GetComponent<EntityFSM>();

        bossHealthBar.SetHealthBar(bossFSM.entityProprieties.maxHealth);
    }


    private void Update()
    {
        if (boss != null)
        {
            int bossHealth = boss.GetComponent<EntityFSM>().entitycurrentHealth;

            Debug.Log("Boss Health: " + bossHealth);

            if (bossHealth <= 0)
            {
                bossHealthBar.gameObject.SetActive(false);
                Destroy(gameObject);

                return;
            }

            bossHealthBar.UpdateHealth(bossHealth);
        }
    }


    private GameObject FindBoss()
    {
        GameObject boss;

        do
        {
            boss = GameObject.FindGameObjectsWithTag("Enemy").Where(enemy => enemy.name.Contains("Boss")).FirstOrDefault();
        } while (boss == null);

        return boss;
    }
}
