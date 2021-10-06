using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCollider : MonoBehaviour
{
    Level level;
    private void Start()
    {
        level = FindObjectOfType<Level>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        level.EnemiesDead();
    }
}
