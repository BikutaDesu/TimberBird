using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Animator enemyAnimator;

    private float enemyHorizontalScale;
    
    void Start()
    {
        enemyHorizontalScale = transform.localScale.x;
        enemyAnimator = GetComponent<Animator>();
        SortEnemy();
    }

    void Update()
    {
        
    }

    void SortEnemy()
    {
        if (Random.value > 0.5f)
        {
            if (Random.value > 0.5f)
            {
                transform.localScale = new Vector2(-enemyHorizontalScale, transform.localScale.y);
            }
            else
            {
                transform.localScale = new Vector2(enemyHorizontalScale, transform.localScale.y);
            }
            enemyAnimator.SetTrigger("Attack");
        }
    }
}
