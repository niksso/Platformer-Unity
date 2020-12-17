using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(Rigidbody2D)))]
[RequireComponent((typeof(SpriteRenderer)))]
public class PatrolAI : MonoBehaviour
{
   [Tooltip("Дистанция, на которую отходит от стартовой точки в обе стороны.")]
   [SerializeField] private float distnace;
   [Tooltip("Скорость передвижения.")]
   [SerializeField] private float speed;

   private Rigidbody2D _rigidbody2D;
   private SpriteRenderer _spriteRenderer;
   
   private Vector3 _startPosition;
   private int _direction = -1;

   private void Start()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      _spriteRenderer = GetComponent<SpriteRenderer>();
      _startPosition = transform.position;
   }

   private void Update()
   {
      if (transform.position.x < _startPosition.x - distnace)
      {
         _direction = 1;
         _spriteRenderer.flipX = true;
      }

      if (transform.position.x > _startPosition.x + distnace)
      {
         _direction = -1;
         _spriteRenderer.flipX = false;
      }

      _rigidbody2D.velocity = new Vector2(_direction * speed,_rigidbody2D.velocity.y);
   }
}
