
using UnityEngine;
public class BubbleHeatlhTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){

            EntityFSM playerFSM = collision.gameObject.GetComponent<EntityFSM>();

            Player playerProprieties = (Player) playerFSM.entityProprieties;

            int playerCurrentHealth = playerFSM.entitycurrentHealth;


            if (playerCurrentHealth < playerProprieties.maxHealth)
            {
                AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
                audioManager.PlaySFX(audioManager.heal);
                playerFSM.entitycurrentHealth = playerProprieties.maxHealth;

                playerProprieties.healthBar.UpdateHealth(playerFSM.entitycurrentHealth);

                Destroy(gameObject);
            }      
        } 
    }


}
