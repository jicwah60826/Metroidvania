using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class SaveData

{

    /////***** Player Controller *****/////

    public int level;
    public int ammoCount;
    public int bombCount;
    public float moveSpeed;
    public float jumpForce;
    public int maxJumps;
    public float hangTime;
    public float smallJumpMult;
    public float dashSpeed;
    public float dashTime;
    public float waitAfterDashing;
    public float dashHangAmt;
    public float waitToBall;
    public bool infiniteAmmo;

    public float playerSpawnX;
    public float playerSpawnY;



    /////***** Player Abilities *****/////

    public bool canDoubleJump;
    public bool canTripleJump;
    public bool canDash;
    public bool canBecomeBall;
    public bool canDropBombs;
    public float dashSpeedMult;
    public float dashTimeMult;


    /////***** Player Health *****/////

    //Current Health (int)
    public int currentHealth;
    //Max Health (int)
    public int maxHealth;

}
