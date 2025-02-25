// ==================== ProjectileController.cs ====================
// Controls projectile behavior (movement, collision, and damage).

using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private GameObject target;
    private float speed;
    private int damage;

    private void Start() => Destroy(gameObject, 5f); // Destroy after 5 seconds
    
    public void Initialize(GameObject target, float speed, int damage)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;
    }

    private void Update()
    {
        if (target == null) return;

        // Move towards the target
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        if (target.TryGetComponent(out WizardController wizard))
        {
            wizard.TakeDamage(damage);
            Debug.Log($"Projectile hit {wizard.wizardName} for {damage} damage.");
        }
        Destroy(gameObject);
    }
    
    
}