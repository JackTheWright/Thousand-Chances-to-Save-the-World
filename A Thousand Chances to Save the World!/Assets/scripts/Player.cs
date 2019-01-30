using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour {
    Rigidbody2D myrigidbody;
    Animator myanimator;
    CapsuleCollider2D mybodycollider;
    BoxCollider2D myfeetcollider;
    public BoxCollider2D Hitboy;
    BoxCollider2D enembox;

    int attackflag=0;
    float lastattacktime = 0;
    bool animated = false;
    float xpos = 0;

    public float attackwait = 2f;

    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float runspeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(0.5f, 0.5f);

    bool isAlive = true;
    int HP;


    // Use this for initialization
    void Start () {

        myrigidbody = GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();
        mybodycollider = GetComponent<CapsuleCollider2D>();
        myfeetcollider = GetComponent<BoxCollider2D>();
        Hitboy = transform.Find("Hitbox").GetComponent<BoxCollider2D>();
        Hitboy.enabled = false;
        //Hitboy = transform.Find(Hitboy);
    }

    private void Awake()
    {
        Hitboy.enabled = false;
    }

    // Update is called once per frame
    void Update () {
        if (!isAlive) { return; }



        myanimator.ResetTrigger("attack1");

        if (myfeetcollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && myanimator.GetBool("attack1") == false)
        {
            myanimator.SetBool("jump", false);
            myanimator.SetBool("Fall", false);
            if (Input.GetKey(KeyCode.Mouse0) && (Time.time - lastattacktime) > 0.5)
            {
                xpos = myrigidbody.position.x;
                print("getting stuck in here?");
                HitIt();
                lastattacktime = Time.time;
                animated = true;

            }
            Run();
            FlipSprite();
            Jump();
        }
        else if (!myfeetcollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && (myrigidbody.velocity.y < -1))
        {
            print("does reach?");
            myanimator.SetBool("Fall", true);
            Run();
            FlipSprite();
        }
        else
        {
            Run();
            FlipSprite();
        }
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 velocity = new Vector2(controlThrow * runspeed, myrigidbody.velocity.y);
        myrigidbody.velocity = velocity;

        if (Mathf.Abs(myrigidbody.velocity.x) > Mathf.Epsilon && (myfeetcollider.IsTouchingLayers(LayerMask.GetMask("Ground"))))
        {
            myanimator.SetBool("Running", true);
        }
        else if (myrigidbody.velocity.y < -0.5 && (!myfeetcollider.IsTouchingLayers(LayerMask.GetMask("Ground"))))
        {
            myanimator.SetBool("Fall", true);
        }

        else
        {
            myanimator.SetBool("Running", false);
        }
        if (Mathf.Abs(xpos - myrigidbody.position.x) > 3) {
            attackflag = 0;

        }

    }

    public void HitIt() {

        myanimator.SetTrigger("attack1");
        print("reached the attack 1");
  
    }

    private void Jump() {
        if (!myfeetcollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        
        if (CrossPlatformInputManager.GetButtonDown("Jump")) {
            attackflag = 0;
            myanimator.SetBool("jump", true);
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

    public void LoseHP(int damageTaken)
    {
        HP -= damageTaken;
        print("Got hit by the enemy");
        GetComponent<Rigidbody2D>().velocity = deathKick;
        if (HP == 0)
        {
            myanimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
        }
    }

    public void OnTriggerEnter2D(Collider2D Enemy)
    {
        if (Enemy.isTrigger != true && (Enemy.CompareTag("MushBaddy") || Enemy.CompareTag("EarthBaddy")))
        {
            HP -= 1;
            print("Got hit by the enemy");
            GetComponent<Rigidbody2D>().velocity = deathKick;

        }
    }

    public void HitBoxStart() {
        Hitboy.enabled = true;
        print("it on");
        Debug.Log("Collider.enabled = " + Hitboy.enabled);
       // HitEnemy();

    }

    public void HitBoxEnd() {
        Hitboy.enabled = false;
        print("it off");
        Debug.Log("Collider.enabled = " + Hitboy.enabled);

    }

    //public void HitEnemy() {
    //    GameObject[] mushcollider = GameObject.FindGameObjectsWithTag("MushBaddy");
    //    foreach (GameObject enemmushcollider in mushcollider) {
    //        enembox = enemmushcollider.GetComponent<BoxCollider2D>();
    //        if (Hitboy.IsTouching(enembox)) {
    //            print("wankyboy");
    //        }
    //    }
    //}


}
