using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject deadObject;
    [SerializeField] Material deadMaterial;
    [SerializeField] GameObject ground;
    [SerializeField] GameObject player;

    Level level;
    bool isDead = true;

    void Start()
    {
        level = FindObjectOfType<Level>();
        level.CountEnemies();
        animator = GetComponent<Animator>();
        animator.SetBool("isDead", false);
    }
    void Update()
    {
        MakeLowToKill();
    }
    public void GetFallDamage()
    {
        NavMeshAgent navMesh = GetComponent<NavMeshAgent>();
        navMesh.enabled = false;
        animator.enabled = false;
        Destroy(gameObject, 1f);
        level.EnemiesDead();
    }
    public void MakeLowToKill()
    {
        var temp = transform.localScale;
        var playerTempx = player.transform.localScale.x;
        if (temp.x <= playerTempx)
        {
            NavMeshAgent navMesh = GetComponent<NavMeshAgent>();
            if (navMesh != null)
            {
                navMesh.enabled = false;
            }
            temp.x = playerTempx + 1;
            EnemyDie();            
        }

        if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (temp.x >= 12f)
            {
                ground.AddComponent<Rigidbody>();
                temp.x = 3f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "SkeletonKnife")
        {
            EnemyDie();
        }
    }
    private void EnemyDie()
    {
        animator.SetBool("isDead", true);
        deadObject.GetComponent<SkinnedMeshRenderer>().material = deadMaterial;       
        if (isDead)
        {
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            boxCollider.size = new Vector3(boxCollider.size.x, boxCollider.size.y / 2.40f, boxCollider.size.y);
            isDead = false;
            level.EnemiesDead();
        }
        Destroy(gameObject, 2f);
    }
}
