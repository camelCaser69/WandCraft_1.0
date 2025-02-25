// ==================== SpellBase.cs ====================
// Abstract base class for all spells with a helper to instantiate projectiles with a spawn offset.
using UnityEngine;

public abstract class SpellBase : ScriptableObject
{
    public string spellName;
    public float manaCost;
    public float cooldown;

    // All spells must implement their own CastSpell method.
    public abstract void CastSpell(GameObject caster, GameObject target, float speedMultiplier);

    // Helper method to instantiate a projectile with a forward offset.
    // This moves the projectile along its local right (assuming that’s its forward direction)
    // by the given offsetDistance.
    protected GameObject InstantiateProjectileWithOffset(GameObject projectilePrefab, Transform firePoint, Quaternion rotation, float offsetDistance)
    {
        Vector3 offset = rotation * (Vector3.right * offsetDistance);
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position + offset, rotation);
        return projectile;
    }
}