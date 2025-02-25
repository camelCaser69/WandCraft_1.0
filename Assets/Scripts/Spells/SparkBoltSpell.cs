// ==================== SparkBoltSpell.cs ====================
// Basic projectile spell similar to "Spark Bolt" from Noita,
// now using a spawn offset via the helper in SpellBase.
using UnityEngine;

[CreateAssetMenu(fileName = "SparkBoltSpell", menuName = "Spells/SparkBolt")]
public class SparkBoltSpell : SpellBase
{
    public GameObject projectilePrefab;
    public float baseProjectileSpeed = 10f;
    public int damage = 10;
    // New property to control how far from the firePoint the projectile spawns.
    public float spawnOffsetDistance = 1.5f;

    public override void CastSpell(GameObject caster, GameObject target, float speedMultiplier)
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("Projectile prefab is not assigned.");
            return;
        }

        Transform firePoint = caster.GetComponentInChildren<WandManager>().firePoint;
        float spreadAngle = caster.GetComponentInChildren<WandManager>().wandData.spread;

        // Apply random spread within the cone defined by spreadAngle.
        float angleOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
        Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angleOffset);

        // Instantiate the projectile with the offset using the helper method.
        GameObject projectile = InstantiateProjectileWithOffset(projectilePrefab, firePoint, rotation, spawnOffsetDistance);

        // Debug log for spawn details.
        Debug.Log($"Projectile spawned at {firePoint.position + (rotation * (Vector3.right * spawnOffsetDistance))} with firePoint rotation {firePoint.rotation.eulerAngles}, " +
                  $"angle offset {angleOffset}°, resulting in projectile rotation {rotation.eulerAngles}.");

        // Get the ProjectileController component and initialize it.
        ProjectileController controller = projectile.GetComponent<ProjectileController>();
        controller.Initialize(caster, target, baseProjectileSpeed * speedMultiplier, damage);
    }
}
