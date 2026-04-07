using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    public Action Moved;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Moved();
    }

    public void MovePlayer(float moveX, float moveY)
    {
        // Input en 4 direcciones (tipo Stardew)
        movement.x = moveX;
        movement.y = moveY;

        // Normalizar para evitar movimiento más rápido en diagonal
        movement = movement.normalized;
    }

}
