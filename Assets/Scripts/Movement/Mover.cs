using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        private NavMeshAgent navMesh;
        private Health health;

        [SerializeField] float maxSpeed = 6f;
        [SerializeField] float maxPathLength = 40f;

        private void Awake()
        {
            navMesh = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            navMesh.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction) // With canceling fight
        {            
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if(!hasPath || path.status != NavMeshPathStatus.PathComplete) return false;

            if(GetPathLength(path) > maxPathLength) return false;

            return true;
        }

        private float GetPathLength(NavMeshPath path)
        {
            float total = 0;
            if (path.corners.Length < 2) return total;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                total += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }

            return total;
        }

        public void MoveTo(Vector3 destination, float speedFraction) //without canceling fight
        {
            navMesh.destination = destination;
            navMesh.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMesh.isStopped = false;
        }

        public void Cancel()
        {
            navMesh.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity; //o ile się rusza na każdej z osi globalnie        
            Vector3 localVelocity = transform.InverseTransformDirection(velocity); //word space to local space       
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("ForwardSpeed", speed);
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3) state;
            navMesh.Warp(position.ToVector());            
        }
    }
}
