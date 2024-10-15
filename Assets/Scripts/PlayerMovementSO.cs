using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovementLevelx", menuName = "SO/PlayerMovement")]
public class PlayerMovementSO : ScriptableObject
{
    public float accleration = 1500f;
    public float maxSpeed = 10f;
    public float drag = 0.5f;
    public float dashMultiplier = 64f;
}
