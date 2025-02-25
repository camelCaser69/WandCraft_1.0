// ==================== WandBase.cs ====================
// Base wand data class containing core stats and spell management
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewWand", menuName = "Wand/WandBase")] // <--- Added this line
[System.Serializable]
public class WandBase : ScriptableObject
{
    public float maxMana;
    public float manaRechargeRate;
    public float rechargeTime;
    public float spread;
    public float cooldown;
    public float projectileSpeedMultiplier;

    public List<SpellBase> spells = new List<SpellBase>();

    public float CalculateTotalManaCost()
    {
        float totalCost = 0f;
        foreach (SpellBase spell in spells)
        {
            totalCost += spell.manaCost;
        }
        return totalCost;
    }
}