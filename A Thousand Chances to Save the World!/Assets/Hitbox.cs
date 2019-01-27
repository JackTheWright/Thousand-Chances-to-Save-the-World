using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    BoxCollider2D hitbox;
    int colliderflag = 0;

    public int dmg = 1;


    // Start is called before the first frame update
    void Start()
    {
        hitbox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void OnTriggerEnter2D(Collider2D HitEnemy)
    {
        if (HitEnemy.isTrigger != true && (HitEnemy.CompareTag("MushBaddy") || HitEnemy.CompareTag("EarthBaddy")))
        {
            HitEnemy.SendMessageUpwards("Damage", dmg);
        }
    }
}
