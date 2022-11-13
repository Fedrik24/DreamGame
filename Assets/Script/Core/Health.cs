using System;
using Dreams.Core.Scheduler;
using UnityEngine;

namespace Dreams.Core.Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        [SerializeField] private Animator animator;

        private ActionScheduler actionScheduler;

        private bool isDead = false;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
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
            actionScheduler.CancelCurrentAction();
        }
    }
}