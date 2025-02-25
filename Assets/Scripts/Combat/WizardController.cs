// ==================== WizardController.cs ====================
// Handles wizard logic: health, damage, and interacts with WandManager for spell casting.

using UnityEngine;

public class WizardController : MonoBehaviour
{
    [Header("Wizard Settings")]
    public string wizardName;
    public int maxHealth = 100;
    private int currentHealth;
    public float GetCurrentMana() => wandManager.GetCurrentMana();
    public float GetMaxMana() => wandManager.GetMaxMana();


    [Header("References")]
    public WandManager wandManager; // Using WandManager

    private void Awake()
    {
        currentHealth = maxHealth;
        if (wandManager == null)
        {
            wandManager = GetComponentInChildren<WandManager>();
            if (wandManager == null)
            {
                Debug.LogWarning($"{wizardName} has no WandManager assigned.");
            }
        }
    }

    private void Start()
    {
        wandManager?.InitializeWand();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{wizardName} took {amount} damage. Remaining HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{wizardName} has been defeated!");
        gameObject.SetActive(false);
    }

    public void StartBattle()
    {
        wandManager?.StartFiring();
    }

    public void StopBattle()
    {
        wandManager?.StopFiring();
    }
}