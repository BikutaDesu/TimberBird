﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float cutDelay = 1;
    private float nextCut;

    private Animator playerAnimator;
    private float playerHorizontalScale;

    // Start is called before the first frame update
    void Start()
    {
        playerHorizontalScale = transform.localScale.x;
        playerAnimator = GetComponent<Animator>();
        nextCut = cutDelay;
    }

    // Update is called once per frame
    void Update()
    {
        nextCut -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && nextCut <= 0)
        {
            nextCut = cutDelay;
            Cut();
        }
    }

    void Cut()
    {
        if (Input.mousePosition.x > Screen.width / 2)
        {
            transform.position = new Vector2(1.3f, -3.5f);
            transform.localScale = new Vector2(-playerHorizontalScale, transform.localScale.y);
        }
        else
        {
            transform.position = new Vector2(-1.3f, -3.5f);
            transform.localScale = new Vector2(playerHorizontalScale, transform.localScale.y);
        }
        playerAnimator.ResetTrigger("Cut");
        playerAnimator.SetTrigger("Cut");
        Invoke("ResetAnimation", 0.6f);
    }

    void ResetAnimation()
    {
        playerAnimator.ResetTrigger("Idle");
        playerAnimator.SetTrigger("Idle");
            
    }

}
