using System;
using Dreams.Core.Scheduler;
using Dreams.Saving;
using UnityEngine;

namespace Dreams.Core.Health
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float healthPoints;
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

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float) state;
            if (healthPoints == 0)
            {
                Die();
            }
        }
    }
}