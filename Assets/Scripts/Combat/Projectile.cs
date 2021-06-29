using RPG.Core;
using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        Health target = null;
        [SerializeField] float speed = 2f;
        [SerializeField] float maxLifeTime = 10;
        [SerializeField] bool isHoming = true;
        [SerializeField] GameObject hitEffect = null;
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

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if(targetCapsule == null) return target.transform.position;
            return target.transform.position + Vector3.up * targetCapsule.height / 1.5f;
        }

        private void OnTriggerEnter(Collider other)
        {            
            if(other.GetComponent<Health>() != target) return;
            if(target.IsDead()) return;

            target.TakeDamage(damage);

            if(hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
