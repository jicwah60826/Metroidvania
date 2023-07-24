using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamParallax : MonoBehaviour
{

    public Transform target;
    public Transform backGround, midGround, foreGround;

    public float FarBGMoveAmt = 1f, MidBGMoveAmt = .5f, ForBGMoveAmt = .25f;

    private float lastXPos;

    // Start is called before the first frame update
    void Start()
    {
        lastXPos = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);

        float amountToMoveX = transform.position.x - lastXPos;

        if (backGround != null)
        {
            backGround.position += new Vector3(amountToMoveX * FarBGMoveAmt, 0f, 0f);
        }

        if (midGround != null)
        {

            midGround.position += new Vector3(amountToMoveX * MidBGMoveAmt, 0f, 0f);

        }

        if (foreGround != null)
        {
            foreGround.position += new Vector3(amountToMoveX * -ForBGMoveAmt, 0f, 0f);
        }

        lastXPos = transform.position.x;
    }
}
