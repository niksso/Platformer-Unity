using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Передвижение.")]
    [Tooltip("Скорость передвижения персонажа.")]
    [SerializeField] private float speed;
    [Header("Прыжок.")]
    [Tooltip("Сила прыжка персонажа.")]
    [SerializeField] private float jumpForce;
    [Tooltip("Слой, по которому проверяется стоит ли персонаж на земле.")]
    [SerializeField] private LayerMask groundMask;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;
    
    private float _direction;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(_direction * speed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = movement;
    }

    private void Update()
    {
        if (_direction != 0f)
        {
            _animator.SetBool("Movement", true);
        }
        else
        {
            _animator.SetBool("Movement", false);
        }

        if (_direction < 0f)
        {
            _spriteRenderer.flipX = true;
        }
        if (_direction > 0f)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void OnMovement(InputValue value)
    {
        _direction = value.Get<Vector2>().x;
    }

    private void OnJump()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 0f,Vector2.down, 0.1f, groundMask);
       
        if (raycastHit2D.collider != null)
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
        }
    }
}
