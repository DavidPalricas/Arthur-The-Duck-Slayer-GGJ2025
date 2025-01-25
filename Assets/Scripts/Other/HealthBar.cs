using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float maxHealth;
    public Image healthBar;
    // Start is called before the first frame update
   
    public void SetHealthBar(int playerMaxHealth)
    {
        maxHealth = playerMaxHealth;
        healthBar.fillAmount= Mathf.Clamp(maxHealth / maxHealth, 0, 1);
    }

    public void PlayerTookDamaged(int playerCurrentHealth)
    {
        healthBar.fillAmount = Mathf.Clamp(playerCurrentHealth / maxHealth, 0, 1);
    }
}
