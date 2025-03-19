using System;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Animator healthAnimator;

    private void OnEnable()
    {
        Health.OnLifesChanged += UpdateHealthAnimation;
    }

    private void OnDisable()
    {
        Health.OnLifesChanged -= UpdateHealthAnimation;
    }

    private void UpdateHealthAnimation(int currentLifes)
    {
        if (healthAnimator == null) return;

        // Set the appropriate trigger based on life count
        string triggerName = "HP" + currentLifes;
        healthAnimator.SetTrigger(triggerName);
    }
}
