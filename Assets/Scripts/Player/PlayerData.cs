using UnityEngine;

[CreateAssetMenu(fileName ="PlayerScriptableObject" , menuName = "ScriptableObjects/Player Data")]
public class PlayerData : ScriptableObject
{

    [Header("Movement")]
    public int moveSpeed;
    public int jumpForce;
    public float hangTime;
    public float smallJumpMult;
}