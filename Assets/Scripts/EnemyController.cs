using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Animator enemyAnimator;
    private Rigidbody2D enemyRigidBody;

    private float enemyHorizontalScale;
    
    void Start()
    {
        enemyHorizontalScale = transform.localScale.x;
        enemyAnimator = GetComponent<Animator>();
        enemyRigidBody = GetComponent<Rigidbody2D>();
        if (this.tag != "Barrel")
        {
            SortEnemy();    
        }
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
        else
        {
            this.tag = "Barrel";
        }
    }

    public void TakeDamage(int direction)
    {
        enemyRigidBody.velocity = new Vector2((direction) * 10, 2);
        enemyRigidBody.isKinematic = false;
        enemyRigidBody.AddTorque((-direction) * 50.0f);
        if (this.tag == "Enemy")
        {
            enemyAnimator.ResetTrigger("Attack");
            enemyAnimator.SetTrigger("Death");
        }
        Invoke("DestroyEnemy", 2.0f);
    }

    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
}
