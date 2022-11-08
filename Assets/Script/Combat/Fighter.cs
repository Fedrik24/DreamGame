using Dreams.Movement;
using UnityEngine;

namespace Dreams.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private Mover move;
        [SerializeField] private float weaponRange = 2f; // Can use ScriptableObject in the future...
        
        private Transform target;
        private void Update()
        {
            if (target == null) return;
            
            if (!GetIsInRange())
            {
                move.MoveTo(target.position);
            }
            else
            {
                move.Stop();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
        }

        public void CancelAttack()
        {
            target = null;
        }
    }
}