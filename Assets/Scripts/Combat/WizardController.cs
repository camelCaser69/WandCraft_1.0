// ==================== WizardController.cs ====================
// Controls wizard behavior: wand usage, damage handling
using UnityEngine;

public class WizardController : MonoBehaviour
{
    public WandManager wandManager;
    public float maxHP;
    public float currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(float amount)
    {
        currentHP -= amount;
        Debug.Log($"Wizard {gameObject.name} took {amount} damage. Current HP: {currentHP}");
    }
}