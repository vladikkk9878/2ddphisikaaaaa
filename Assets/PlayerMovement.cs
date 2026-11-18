using UnityEngine;
using System;
using TMPro.EditorUtilities;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayerMask;
    [Header("Shoot")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private float horizontalInput;
    private Rigidbody2D _rb;
    private float _jumpCounter;
    private bool _isGrounded;
    private const int maxJumpCounter = 2;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        if (Input.GetMouseButtonDown(0))
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityX = horizontalInput * speed;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (LayerMaskUtil.Contains(groundLayerMask, other.gameObject.layer))
            _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (LayerMaskUtil.Contains(groundLayerMask, other.gameObject.layer))
            _isGrounded = true;
    }

    public static class LayerMaskUtil
    {
        public static bool Contains(LayerMask mask, int layer) =>
            mask == (mask | (1 << layer));
    }
}

