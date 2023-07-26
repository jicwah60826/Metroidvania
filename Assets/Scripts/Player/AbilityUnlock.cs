using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityUnlock : MonoBehaviour
{
    public float textOnScreenTime;

    public string onscreenText;

    //public TMP_Text unlockText;

    public bool unlockDoubleJump, unlockTripleJump, unlockDash, unlockBecomeBall, unlockDropBomb, dashTimeMult, dashSpeedMult;

    public float dashTimeIncreaseAmt = 1.25f;
    public float dashSpeedIncreaseAmt = 1.25f;

    private bool collected = false;

    public GameObject pickupEffect;

    public Collider2D theCollider;

    public SpriteRenderer theSR;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !collected)
        {
            Debug.Log("Player detected in ability pickup");
            PlayerAbilityTracker player = other.GetComponentInParent<PlayerAbilityTracker>();
            PlayerController playerController = player.GetComponentInParent<PlayerController>();
            AudioManager.instance.PlaySFX(11); // pickup gem sound

            collected = true;

            // deactivate the sprite
            theSR.enabled = false;

            // disable the collider
            theCollider.enabled = false;

            if (unlockDoubleJump)
            {
                player.canDoubleJump = true;
                playerController.maxJumps = 2;
                Debug.Log("Double jump unlocked");
            }

            if (unlockTripleJump)
            {
                player.canTripleJump = true;
                playerController.maxJumps = 3;
                Debug.Log("Double jump unlocked");
            }

            if (unlockDash)
            {
                player.canDash = true;
            }

            if (unlockBecomeBall)
            {
                player.canBecomeBall = true;
            }

            if (unlockDropBomb)
            {
                player.canDropBomb = true;
            }

            Instantiate(pickupEffect, transform.position, transform.rotation);

            //Invoke co routine for on screen text display 
            StartCoroutine(OnScreenTextController());

            //Destroy(gameObject, .1f);
        }
    }

    public IEnumerator OnScreenTextController()
    {
        //Destroy(gameObject);

        // Set On Screen Message UI text
        UIController.instance.onScreenLowerThirdText.text = onscreenText;

        // Enable the on screen message UI element
        UIController.instance.onScreenLowerThirdText.gameObject.SetActive(true);

        yield return new WaitForSeconds(textOnScreenTime);
        // Clear the text from the on screen text (ready for future use)
        UIController.instance.onScreenLowerThirdText.text = "";

        // Destroy this game object a short time after the text has been disabled
        Destroy(gameObject);
    }
}
