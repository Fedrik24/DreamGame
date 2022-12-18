using System.Collections.Generic;
using Dreams.Core;
using Dreams.Core.Health;
using Dreams.Core.Scheduler;
using Dreams.Saving;
using UnityEngine;
using UnityEngine.AI;


namespace Dreams.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        private NavMeshAgent playerAgent;
        private Animator characterAnimator;
        private Health health;

        void Start()
        {
            playerAgent = GetComponent<NavMeshAgent>();
            characterAnimator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            playerAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        #region Public Method

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
        
        public void MoveTo(Vector3 destination)
        {
            playerAgent.destination = destination;
            playerAgent.isStopped = false;
        }

        public void Cancel()
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


        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }
        
    }
}