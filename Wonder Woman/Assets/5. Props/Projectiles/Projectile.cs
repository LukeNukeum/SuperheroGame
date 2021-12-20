using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Character;

namespace LupiLab.Combat
{
    public class Projectile : MonoBehaviour
    {

        public Character.Character Attacker { get; set; }
        [SerializeField] protected float _speed = 5f;

        [SerializeField] protected LayerMask _layerMask;
        [SerializeField] protected float _damageValue = 10f;
        [SerializeField] protected DamageType _damageType;

        // Update is called once per frame
        void FixedUpdate()
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(origin: transform.position, direction: transform.forward, hitInfo: out hitInfo, _speed * Time.fixedDeltaTime, layerMask: _layerMask))
            {
                Damageable hitDamageable = hitInfo.collider.GetComponentInParent<Damageable>();
                hitDamageable?.DoDamage(new Damage(_damageValue,_damageType,Attacker,hitInfo.collider,transform.forward));
                Destroy(gameObject);
            }

            transform.position += transform.forward*_speed*Time.fixedDeltaTime;
        }
    }
}