using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour {
    Rigidbody2D myrigidbody;
    Animator myanimator;
    Collider2D mycollider;
    CapsuleCollider2D mybodycollider;
    BoxCollider2D myfeetcollider;

    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float runspeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);

    bool isAlive = true;
    int HP;

	// Use this for initialization
	void Start () {
        myrigidbody = GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();
        mybodycollider = GetComponent<CapsuleCollider2D>();
        myfeetcollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isAlive) { return; }
        Run();
        FlipSprite();
        Jump();
    	}

    private void Run () {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(controlThrow * runspeed, myrigidbody.velocity.y);
        myrigidbody.velocity = velocity;

        if (Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon){
            myanimator.SetBool("Running", true);
        }
        else {
            myanimator.SetBool("Running", false);
        }
    }

    private void Jump() {
        if (!myfeetcollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        
        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            Vector2 jumpUp = new Vector2(0f, jumpSpeed);
            myrigidbody.velocity += jumpUp;
        }
    }

    private void FlipSprite() {

        bool playerHorizonVelocity = Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHorizonVelocity) {
            transform.localScale = new Vector2((float)(Mathf.Sign(myrigidbody.velocity.x) * 2.657), (float)2.355);
        }
    }

    private void loseHP()
    {
        if (mybodycollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))) {
            HP -= 1;
            if (HP == 0)
            {
                myanimator.SetTrigger("Die");
                GetComponent<Rigidbody2D>().velocity = deathKick;
            }
        }
    }

}
