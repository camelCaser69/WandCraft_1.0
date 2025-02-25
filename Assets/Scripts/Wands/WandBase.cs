// ==================== WandBase.cs ====================
// Base wand data class containing core stats
using UnityEngine;

[System.Serializable]
public class WandBase : ScriptableObject
{
    public float maxMana;
    public float manaRechargeRate;
    public float rechargeTime;
    public float spread;
    public float cooldown;
    public float projectileSpeedMultiplier;

    // To be expanded with additional wand properties
}