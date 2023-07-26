using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleGhost : MonoBehaviour

{

    private CameraController theCam;
    public Transform camPosition;
    public float camMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        theCam = FindObjectOfType(typeof(CameraController)) as CameraController;

        //disable the camera controller script on theCam so that we can move the camera as needed
        theCam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        theCam.transform.position = Vector3.MoveTowards(theCam.transform.position, camPosition.position, camMoveSpeed * Time.deltaTime);
    }
}
