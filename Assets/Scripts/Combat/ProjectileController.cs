// ==================== ProjectileController.cs ====================
// Controls projectile behavior (movement, collision, and damage) based on its rotation.

using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private int damage;
    private GameObject caster;

    private void Start() => Destroy(gameObject, 5f); // Auto-destroy after 5 seconds

    public void Initialize(GameObject caster, GameObject target, float speed, int damage)
    {
        this.caster = caster;
        this.speed = speed;
        this.damage = damage;
        // Use transform.right as the movement direction if your sprite is drawn facing right.
        direction = transform.right;
        Debug.Log($"Projectile initialized. Rotation: {transform.rotation.eulerAngles}, Direction: {direction}, Speed: {speed}");
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Prevent the projectile from hitting the caster.
        if (collision.gameObject == caster) return;

        if (collision.TryGetComponent(out WizardController wizard))
        {
            wizard.TakeDamage(damage);
            Debug.Log($"Projectile hit {wizard.wizardName} for {damage} damage.");
            Destroy(gameObject);
        }
    }
}