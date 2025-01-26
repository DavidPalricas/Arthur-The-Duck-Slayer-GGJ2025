
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            if (bossHealth <= 0)
            {
                bossHealthBar.gameObject.SetActive(false);
                SceneManager.LoadScene("EndGame");

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
