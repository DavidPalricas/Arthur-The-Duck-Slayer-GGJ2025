using UnityEngine;
using UnityEngine.UI;

public class LazerCoolDownLabel : MonoBehaviour
{
    [SerializeField]
    private Image lazerCoolDownLabel;

    private float lazerCoolDown;
    public void SetCoolDown(float lazerCoolDown)
    {
        this.lazerCoolDown = lazerCoolDown;
        lazerCoolDownLabel.fillAmount = Mathf.Clamp(this.lazerCoolDown / this.lazerCoolDown, 0, 1);
    }


    public void UpdateCoolDown(float currentLazerCoolDown)
    {
        lazerCoolDownLabel.fillAmount = Mathf.Clamp(currentLazerCoolDown / lazerCoolDown, 0, 1);
    }
}
