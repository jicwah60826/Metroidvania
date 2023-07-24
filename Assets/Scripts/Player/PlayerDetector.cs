using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    private bool playerInRange;

    private void OnTriggerStay2D(Collider2D other)
    {
        //check what object tag the bullet collided with
        if (other.gameObject.tag == "Player")
        {
            //playerInRange = true;
        }
        else
        {
            //playerInRange = false;
        }
    }
}
