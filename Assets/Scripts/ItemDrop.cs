using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{


    private Rigidbody2D theRB;
    public float dropForce = 5;

    // Start is called before the first frame update
    void Start()
    {
     
        // find it's own rigid body
        theRB = GetComponent<Rigidbody2D>();
        //bounce up in the air as soon as instantiated
        theRB.AddForce(Vector2.up * dropForce, ForceMode2D.Impulse);

    }

}
