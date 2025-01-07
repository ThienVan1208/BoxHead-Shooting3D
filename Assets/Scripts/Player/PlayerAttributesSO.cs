using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu(menuName = "Player/PlayerAttributesSO", fileName = "PlayerAttributesSO")]
public class PlayerAttributesSO : ScriptableObject
{
    public Vector2 moveDirection;
    public float jumpForce;
    public float moveSpeed;
    public float jumpDistance;
    public float groundCheckDelay;
    public float groundCheckDistance;
    public LayerMask layerCheckJump;

    public float maxHP, curHP;
    public bool isDie;
    public PlayerTransformSO playerTransformSO;
}
