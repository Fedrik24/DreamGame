using Dreams.Combat;
using UnityEngine;
using UnityEngine.AI;


namespace Dreams.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private Fighter fighter; // DEBT need to fix this quicly, Cyclic Namespace...
        
        private NavMeshAgent playerAgent;
        private Animator characterAnimator;

        void Start()
        {
            playerAgent = GetComponent<NavMeshAgent>();
            characterAnimator = GetComponent<Animator>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        #region Public Method

        public void StartMoveAction(Vector3 destination)
        {
            fighter.CancelAttack(); 
            MoveTo(destination);
        }
        
        public void MoveTo(Vector3 destination)
        {
            playerAgent.destination = destination;
            playerAgent.isStopped = false;
        }

        public void Stop()
        {
            playerAgent.isStopped = true;
        }
        
        #endregion

        private void UpdateAnimator()
        {
            Vector3 velocity = playerAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float characterSpeed = localVelocity.z;
            characterAnimator.SetFloat("forwardSpeed", characterSpeed);
        }
    }
}