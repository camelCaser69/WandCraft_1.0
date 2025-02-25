// ==================== SparkBoltSpell.cs ====================
// Basic projectile spell similar to "Spark Bolt" from Noita, now aiming toward the target.

using UnityEngine;

[CreateAssetMenu(fileName = "SparkBoltSpell", menuName = "Spells/SparkBolt")]
public class SparkBoltSpell : SpellBase
{
    public GameObject projectilePrefab;
    public float baseProjectileSpeed = 10f;
    public int damage = 10;
    // How far from the firePoint the projectile spawns (to avoid immediate collisions)
    public float spawnOffsetDistance = 0.5f;

    public override void CastSpell(GameObject caster, GameObject target, float speedMultiplier)
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("Projectile prefab is not assigned.");
            return;
        }

        // Get the firePoint from the caster's WandManager
        Transform firePoint = caster.GetComponentInChildren<WandManager>().firePoint;
        float spreadAngle = caster.GetComponentInChildren<WandManager>().wandData.spread;

        // Compute the direction from the firePoint to the target
        Vector3 directionToTarget = (target.transform.position - firePoint.position).normalized;

        // Compute the base angle (in degrees) from the horizontal axis
        float baseAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        // Apply a random spread offset (in degrees)
        float angleOffset = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
        float finalAngle = baseAngle + angleOffset;
        Quaternion rotation = Quaternion.Euler(0, 0, finalAngle);

        // Instantiate the projectile with an offset so it doesn't spawn inside the caster.
        GameObject projectile = InstantiateProjectileWithOffset(projectilePrefab, firePoint, directionToTarget, spawnOffsetDistance, rotation);

        // Debug log for spawn details.
        Debug.Log($"Projectile spawned at {projectile.transform.position} aimed at {target.transform.position}." +
                  $" BaseAngle: {baseAngle:F2}°, AngleOffset: {angleOffset:F2}°, FinalAngle: {finalAngle:F2}°.");

        // Initialize the projectile (pass the caster so it doesn't hit itself).
        ProjectileController controller = projectile.GetComponent<ProjectileController>();
        controller.Initialize(caster, target, baseProjectileSpeed * speedMultiplier, damage);
    }
}
