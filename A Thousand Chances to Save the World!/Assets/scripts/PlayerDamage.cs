using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public int damageTaken;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D HitEnemy)
    {
        if (HitEnemy.isTrigger != true && (HitEnemy.CompareTag("MushBaddy") || HitEnemy.CompareTag("EarthBaddy")))
        {
            player.LoseHP(damageTaken);
        }
    }
}
