using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Collider2D attackCollider; 
    // Start is called before the first frame update
    void Start()
    {
        attackCollider = GetComponent<BoxCollider2D>();
    }


    public void HitBox() {
        attackCollider.enabled = true;
    }
    public void ShitBox() {
        attackCollider.enabled = false;
    }
}
