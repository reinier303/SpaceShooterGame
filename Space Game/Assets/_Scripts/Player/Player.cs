using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerAttack RPlayerAttack;
    public PlayerMovement RPlayerMovement;
    public PlayerEntity RPlayerEntity;

    private void Awake()
    {
        RPlayerAttack = GetComponent<PlayerAttack>();
        RPlayerMovement = GetComponent<PlayerMovement>();
        RPlayerEntity = GetComponent<PlayerEntity>();
    }
}
