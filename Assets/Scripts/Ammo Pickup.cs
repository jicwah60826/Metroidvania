using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    public int ammoCountAmmount;
    private int currentAmmo;

    private bool collected;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !collected)
        {

            // get current ammo that player has
            currentAmmo = PlayerController.instance.ammoCount;
            // add ammo pickup to current ammo
            currentAmmo += ammoCountAmmount;
            // update playerController ammo amount
            PlayerController.instance.ammoCount = currentAmmo;
            // Update UI
            UIController.instance.UpdateAmmo(currentAmmo);
            collected = true;
            Destroy(gameObject);
        }

    }
}
