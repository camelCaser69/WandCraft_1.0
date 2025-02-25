// ==================== WandManager.cs ====================
// Handles wand firing logic, mana management, and cooldowns
using UnityEngine;
using System.Collections;

public class WandManager : MonoBehaviour
{
    public WandBase wandData;
    public Transform firePoint;
    public GameObject target;
    private float currentMana;
    private bool isRecharging;

    private void Start()
    {
        currentMana = wandData.maxMana;
        StartCoroutine(FireWandSequence());
    }

    private IEnumerator FireWandSequence()
    {
        while (true)
        {
            if (currentMana >= wandData.CalculateTotalManaCost())
            {
                Debug.Log($"Wand {gameObject.name} fired.");
                foreach (SpellBase spell in wandData.spells)
                {
                    if (currentMana >= spell.manaCost)
                    {
                        spell.CastSpell(gameObject, target);
                        currentMana -= spell.manaCost;
                        Debug.Log($"Spell {spell.spellName} fired. Remaining mana: {currentMana}");
                        yield return new WaitForSeconds(spell.cooldown);
                    }
                    else
                    {
                        Debug.Log("No mana for next spell. Waiting for recharge.");
                        break;
                    }
                }
                Debug.Log("Wand sequence complete. Waiting for global cooldown.");
                yield return new WaitForSeconds(wandData.cooldown);
            }
            else
            {
                Debug.Log("Not enough mana to start sequence. Recharging...");
                yield return null;
            }
            RechargeMana();
            yield return null;
        }
    }

    private void RechargeMana()
    {
        if (currentMana < wandData.maxMana)
        {
            currentMana += wandData.manaRechargeRate * Time.deltaTime;
            if (currentMana > wandData.maxMana) currentMana = wandData.maxMana;
        }
    }
}