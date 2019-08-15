using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float cutDelay = 0.4f;
    private float nextCut;
    private float playerHorizontalScale;
    private int playerDirection = 1;

    private Animator playerAnimator;
    private GameController gameControllerScript;
    private SpriteRenderer playerSpriteRenderer;
    private Rigidbody2D playerRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        playerHorizontalScale = transform.localScale.x;
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        gameControllerScript = GameObject.Find("_GC").GetComponent<GameController>();
        nextCut = cutDelay;
        if (!gameControllerScript.isStarted && !gameControllerScript.isOver) gameControllerScript.isStarted = true; 
    }

    // Update is called once per frame
    void Update()
    {
        nextCut -= Time.deltaTime;
        if (gameControllerScript.isStarted)
        {
            if (Input.GetButtonDown("Fire1") && nextCut <= 0)
            {
                nextCut = cutDelay;
                Cut();
            }    
        }
    }

    void Cut()
    {
        if (Input.mousePosition.x > Screen.width / 2)
        {
            playerDirection = 1;
        }
        else
        {
            playerDirection = -1;
        }

        transform.localScale = new Vector2((-playerDirection) * playerHorizontalScale, transform.localScale.y);
        transform.position = new Vector2(playerDirection * 1.3f, -3.4f);
        gameControllerScript.RemoveEnemyFromList(-playerDirection);

        gameControllerScript.CheckPlay();

        playerAnimator.ResetTrigger("Cut");
        playerAnimator.SetTrigger("Cut");
        
        Invoke("ResetAnimation", 0.6f);
    }

    void ResetAnimation()
    {
        playerAnimator.ResetTrigger("Idle");
        playerAnimator.SetTrigger("Idle");
    }

    public void Death()
    {
        playerSpriteRenderer.color = new Color(1.0f, 0.35f, 0.35f);

        playerRigidbody.isKinematic = false;
        
        playerRigidbody.AddTorque(100.0f);
        playerRigidbody.velocity = new Vector2(playerDirection * 5.0f, 3.0f);  
    }
}
