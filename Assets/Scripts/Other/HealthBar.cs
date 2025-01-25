using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float maxHealth;
    public Image healthBar;
    // Start is called before the first frame update
   
    public void SetHealthBar(int entityMaxHealth)
    {
        maxHealth = entityMaxHealth;
        healthBar.fillAmount= Mathf.Clamp(maxHealth / maxHealth, 0, 1);
    }

    public void UpdateHealth(int entityCurrentHealth)
    {
        healthBar.fillAmount = Mathf.Clamp(entityCurrentHealth / maxHealth, 0, 1);
    }
}
