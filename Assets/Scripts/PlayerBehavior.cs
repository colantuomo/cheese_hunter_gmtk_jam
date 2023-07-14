using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private float _jumpForce = 3f;
    [SerializeField]
    private float _moveSpeed = 3f;
    private float _tempSpeed = 0f;
    private float _direction;
    void Start()
    {
        _direction = 1;
        ResetTempSpeed();
        _rb = GetComponent<Rigidbody2D>();
        EventsManager.Singleton.OnUseFastForwardBlock += OnUseFastForwardBlock;
        EventsManager.Singleton.OnResetBlockEffect += OnResetBlockEffect;
        EventsManager.Singleton.OnUseJumpBlock += OnUseJumpBlock;
        EventsManager.Singleton.OnUseBackwardBlock += OnUseBackwardBlock;
    }

    private void OnUseBackwardBlock()
    {
        _direction = -1;
    }

    private void OnUseJumpBlock()
    {
        _rb.AddForce(Vector2.up * _jumpForce);
        //_rb.DOMoveY(transform.position.y + 4f, 1f);
        //var jumpHeight = new Vector3(transform.position.x + 4f, transform.position.y, transform.position.z);
        //_rb.DOJump(jumpHeight, 5f, 1, 1f);
    }

    private void OnResetBlockEffect()
    {
        ResetTempSpeed();
    }

    private void OnUseFastForwardBlock(float speed)
    {
        _direction = 1;
        _rb.DOMoveX(transform.position.x + 3f, 2f).SetEase(Ease.OutExpo);
    }

    private void FixedUpdate()
    {
        float moveX = (_tempSpeed * 10f) * Time.fixedDeltaTime;
        Vector2 velocity = new(moveX * _direction, _rb.velocity.y);
        _rb.velocity = velocity;
    }

    private void ResetTempSpeed()
    {
        _tempSpeed = _moveSpeed;
    }

    public void SetTempSpeed(float speed)
    {
        _tempSpeed = speed;
    }
}
