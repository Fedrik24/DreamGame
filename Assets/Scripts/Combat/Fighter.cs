using Dreams.Core;
using Dreams.Core.Health;
using Dreams.Core.Scheduler;
using Dreams.Movement;
using UnityEngine;

namespace Dreams.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private Mover move;
        [SerializeField] private float weaponRange = 2f; // Can use ScriptableObject in the future...
        [SerializeField] private float timeThreshold = 1f;
        [SerializeField] private float weaponDamage = 10f;
        [SerializeField] private GameObject weaponPrefabs = null;
        [SerializeField] private Transform handTransform;
        [SerializeField] private AnimatorOverrideController weaponOverrides = null;

        private Animator animation;
        private Health target;

        private float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            animation = GetComponent<Animator>();
            SpawnWeapon();
        }

        private void SpawnWeapon()
        {
            Instantiate(weaponPrefabs, handTransform);
            Animator animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = weaponOverrides;
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                move.MoveTo(target.transform.position);
            }
            else
            {
                move.Cancel();
                AttackBehaviour();
            }
        }
        
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeThreshold)
            {
                animation.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }
        
        // Animation Event
        private void Shoot()
        {
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }
        
        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }
        
        public void Cancel()
        {
            animation.SetTrigger("stopAttack");
            target = null;
        }
    }
}