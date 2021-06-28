using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        Health target = null;
        [SerializeField] float speed = 2f;
        [SerializeField] bool isHoming = true;
        float damage = 0;

        GameObject player;

        private void Start() 
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update() 
        {
            if(target == null) return;
            if(isHoming && !target.IsDead()) transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);    
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null) return target.transform.position;
            return target.transform.position + Vector3.up * targetCapsule.height / 1.5f;
        }

        private void OnTriggerEnter(Collider other)
        {
            Health target = other.GetComponent<Health>();
            if (target == null || target.IsDead()) return;

            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
