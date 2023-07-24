using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMover : MonoBehaviour
{

    public float moveSpeed = 2f;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection.x = Random.Range(-moveSpeed, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
