using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // reference to the player controller in the scene
    private PlayerController player;

    public BoxCollider2D boundsBox;

    private float halfHeightOfCam, halfWidthOfCam;

    // Start is called before the first frame update
    void Start()
    {
        // find the player controller in the scene
        player = FindObjectOfType<PlayerController>();

        //get the width and height of camera
        halfHeightOfCam = Camera.main.orthographicSize; //get the height of the camera size EG: 10
        halfWidthOfCam = halfHeightOfCam * Camera.main.aspect; // calc the width using the aspect ratio of the camera
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // move the camera to the player controller location but only follow camera X and Y. Do not move camera Z

            // clamp the camera movement to HALF the size of the min and max X and half of min and max Y.
            transform.position = new Vector3(
            Mathf.Clamp(player.transform.position.x, boundsBox.bounds.min.x + halfWidthOfCam,boundsBox.bounds.max.x - halfWidthOfCam),
            Mathf.Clamp(player.transform.position.y, boundsBox.bounds.min.y + halfHeightOfCam,boundsBox.bounds.max.y - halfHeightOfCam),
            transform.position.z);
        }
    }
}
