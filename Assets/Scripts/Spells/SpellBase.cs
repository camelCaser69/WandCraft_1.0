// ==================== SpellBase.cs ====================
// Abstract base class for all spells
using UnityEngine;

public abstract class SpellBase : ScriptableObject
{
    public string spellName;
    public float manaCost;
    public float cooldown;

    public abstract void CastSpell(GameObject caster, GameObject target);
}