using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosionVFX;
    float radius = 50f;
    float force = 300f;

    private void OnCollisionEnter(Collision ground)
    {
        if (ground.gameObject.name == "Ground")
        {
            Explode();
            gameObject.SetActive(false);
        }
    }
    void Explode()
    {
        Instantiate(explosionVFX, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider nearbyObject in colliders)
        {
            Enemy enemy = nearbyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.GetFallDamage();
            }
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }
    }
}
