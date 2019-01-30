using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBaddy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    public Rigidbody2D myRigidBody;
    public Transform target;
    public float range;
    public int hpEarth = 2;
    public int earthDmg = 1;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) < range)
        {

            if (transform.position.x - target.position.x > 0)
            {
                print("self position is > player");
                //spriterenderer.flipX = true;
                transform.localScale = new Vector3(-1, 1, 1);

            }
            else if (transform.position.x - target.position.x < 0)
            {
                print("self position is < player");
                //spriterenderer.flipX = false;
                transform.localScale = new Vector3(1, 1, 1);

            }
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, transform.position.y), moveSpeed * Time.deltaTime + .01f);




            Debug.Log("I see you bitch.\n");
        }
        else if (IsFacingRight() && (Vector3.Distance(target.position, transform.position) >= range))
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else if (!IsFacingRight() && (Vector3.Distance(target.position, transform.position) >= range))
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }

        if (hpEarth == 0) {
            Destroy(gameObject);
        }
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    public void Damage(int damage)
    {
        hpEarth -= damage;
        print("damaged mushboy");
    }


}   

