using System;
using UnityEngine;

namespace Dreams.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        [SerializeField] private Animator animator; // Bisa di jadiin 1 Script untuk semua animator

        private bool isDead = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        public bool IsDead()
        {
            return isDead;
        }
        
        private void Die()
        {
            if(isDead) return;
            animator.SetTrigger("die");
            isDead = true;
        }
    }
}