using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Character;

namespace LupiLab.Combat
{
    public class Grenade : MonoBehaviour
    {

        [SerializeField] private float _radius = 2f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float _damageValue = 50f;
        [SerializeField] private DamageType _damageType;
        [SerializeField] private float _fuseTime = 3f;

        [SerializeField] private GameObject _explosionPrefab;

        private float _timeOfDetonation = 0;

        // Start is called before the first frame update
        void Start()
        {
            _timeOfDetonation = Time.time + _fuseTime;
        }

        private void FixedUpdate()
        {
            if (Time.time >= _timeOfDetonation)
            {
                Explode();
            }
        }



        public void Explode()
        {
            SpawnExplosionPrefab();
            DealDamageToNearbyDamageables();
            Destroy(gameObject);
        }


        void SpawnExplosionPrefab()
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        }

        void DealDamageToNearbyDamageables()
        {
            foreach (DamageableAndCollider damageableAndCollider in GetDamageableCharactersAndCollidersInRadius(transform.position, _radius, layerMask))
            {
                damageableAndCollider.Damageable.DoDamage(new Damage(_damageValue, _damageType, attacker: null, damageableAndCollider.Collider, damageableAndCollider.Collider.bounds.center - transform.position));
                Debug.DrawLine(transform.position, damageableAndCollider.Collider.bounds.center, Color.red, 2f);
            }
        }

        public DamageableAndCollider[] GetDamageableCharactersAndCollidersInRadius(Vector3 position, float radius, LayerMask layerMask)
        {
            List<Damageable> damageables = new List<Damageable>();
            List<DamageableAndCollider> damageableAndColliders = new List<DamageableAndCollider>();
            Collider[] colliders = Physics.OverlapSphere(position, radius, layerMask);

            foreach (Collider collider in colliders)
            {
                var damageable = collider.GetComponent<Damageable>();

                if (damageable != null)
                {
                    if (!damageables.Contains(damageable))
                    {
                        damageables.Add(damageable);
                        damageableAndColliders.Add(new DamageableAndCollider(damageable, collider));
                    }
                }
            }

            return damageableAndColliders.ToArray();

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        public class DamageableAndCollider
        {
            public Damageable Damageable;
            public Collider Collider;

            public DamageableAndCollider(Damageable damageable, Collider collider)
            {
                Damageable = damageable;
                Collider = collider;
            }
        }
    }
}