using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{

    public GameObject theBoss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            theBoss.SetActive(true);
        }

        gameObject.SetActive(false);

    }

}
