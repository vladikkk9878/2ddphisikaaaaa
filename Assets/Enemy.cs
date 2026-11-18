using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private LayerMask damageLayerMask;
    [SerializeField] private int damage;
    
    [SerializeField] private int maxHealth;
    [SerializeField] private LayerMask DangerLayerMask;
    [SerializeField] private int BulletDamage;

    private bool _ableToAttack;
    private int _currentHealth;



    private void Awake()
    {
        _ableToAttack = true;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        TryAttack(other.gameObject);
        TryGetDamage(other.gameObject);
    }

    private void TryAttack(GameObject other)
    {
        if (_ableToAttack)
        {
            if (LayerMaskUtil.Contains(damageLayerMask, other.layer))
            {
                if (other.gameObject.TryGetComponent(out playerHealth player))
                {
                    player.TakeDamage(damage);
                }
            }
        }
        
    }

    private void TryGetDamage(GameObject other)
    {
        if (LayerMaskUtil.Contains(DangerLayerMask, other.layer))
        {
            _currentHealth -= BulletDamage;
            if (_currentHealth <= 0)
            {
                _ableToAttack = false;
            }
        }
    }
    
    
    
    private static class LayerMaskUtil
    {
        public static bool Contains(LayerMask mask, int layer) =>
            mask == (mask | (1 << layer));
    }
}
