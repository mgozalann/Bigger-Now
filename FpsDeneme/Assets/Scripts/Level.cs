using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    [SerializeField]
    int enemyCount;
    
    [SerializeField]
    GameObject winMenu;

    
    public void CountEnemies()
    {
        enemyCount++;
    }

    public void EnemiesDead()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            winMenu.SetActive(true);
        }
    }
}
