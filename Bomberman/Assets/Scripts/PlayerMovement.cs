using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Top-down Movement")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private PlayerControl inputActions;
    private Vector2 moveInput = Vector2.zero;

    private enum MovementState { idle = 0, walk = 1 }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        inputActions = new PlayerControl();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Movement.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput.normalized * moveSpeed;
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        MovementState state = moveInput != Vector2.zero ? MovementState.walk : MovementState.idle;
        anim.SetInteger("state", (int)state);

        // Optional: flip sprite kiri-kanan (x)
        if (moveInput.x > 0)
            sprite.flipX = false;
        else if (moveInput.x < 0)
            sprite.flipX = true;

        // OPTIONAL: arah atas/bawah untuk animasi arah
        if (anim != null)
        {
            anim.SetFloat("moveX", moveInput.x);
            anim.SetFloat("moveY", moveInput.y);
        }
    }

}
