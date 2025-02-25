// ==================== ProjectileBase.cs ====================
// Handles projectile movement, speed, mass, and gravity
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float speed;
    public float mass;
    public Vector3 direction;

    protected virtual void Update()
    {
        // To be implemented: Gravity and speed calculations
    }
}