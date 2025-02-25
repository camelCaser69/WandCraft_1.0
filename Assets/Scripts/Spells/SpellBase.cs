// ==================== SpellBase.cs ====================
// Abstract base class for all spells with a helper to instantiate projectiles aimed at the target.

using UnityEngine;

public abstract class SpellBase : ScriptableObject
{
    public string spellName;
    public float manaCost;
    public float cooldown;

    // All spells must implement their own CastSpell method.
    public abstract void CastSpell(GameObject caster, GameObject target, float speedMultiplier);

    // Helper method to instantiate a projectile with an offset along the direction to the target.
    protected GameObject InstantiateProjectileWithOffset(GameObject projectilePrefab, Transform firePoint, Vector3 directionToTarget, float offsetDistance, Quaternion rotation)
    {
        Vector3 spawnPosition = firePoint.position + directionToTarget * offsetDistance;
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, rotation);
        return projectile;
    }
}