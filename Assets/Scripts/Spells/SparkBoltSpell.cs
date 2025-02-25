// ==================== SparkBoltSpell.cs ====================
// Basic projectile spell similar to "Spark Bolt" from Noita.

using UnityEngine;

[CreateAssetMenu(fileName = "SparkBoltSpell", menuName = "Spells/SparkBolt")]
public class SparkBoltSpell : SpellBase
{
    public GameObject projectilePrefab;
    public float baseProjectileSpeed = 10f;
    public int damage = 10;

    public override void CastSpell(GameObject caster, GameObject target, float speedMultiplier)
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("Projectile prefab is not assigned.");
            return;
        }

        // Spawn projectile at caster's firePoint
        Transform firePoint = caster.GetComponentInChildren<WandManager>().firePoint;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        ProjectileController controller = projectile.GetComponent<ProjectileController>();
        controller.Initialize(target, baseProjectileSpeed * speedMultiplier, damage);
    }
}