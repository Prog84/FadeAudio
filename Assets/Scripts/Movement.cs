using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbodyPlayer;
    [SerializeField] private Vector3 _direction;
    public Vector3 Direction => _direction;

    private void Update()
    {
        Move();
        _animator.SetFloat("Speed", Mathf.Abs(_direction.x));
    }

    private void Move()
    {
        _direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
            _direction = Vector3.left;
        if (Input.GetKey(KeyCode.D))
            _direction = Vector3.right;
        _direction *= _speed;
        _rigidbodyPlayer.velocity = _direction;
        if (_direction.x > 0)
            _spriteRenderer.flipX = false;
        if (_direction.x < 0)
            _spriteRenderer.flipX = true;
    }
}
