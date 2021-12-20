using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Targeting;
using LupiLab.Character;
using LupiLab.Combat;

public class AIShooting : MonoBehaviour
{


    [SerializeField] Transform _firingPoint;
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] Searcher.SearcherSettings _searcherSettings;
    [SerializeField] float _firingDelay = 2f;

    Character _character;
    Searcher _searcher;
    float _shootingCooldownTime;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _searcher = new Searcher(_searcherSettings);
    }

    private void Start()
    {
        SetCooldownTime();
    }

    private void FixedUpdate()
    {
        if (_searcher.GetTarget())
        {
            FaceTowardsTarget(_searcher.Target.Position);
            TryShooting(_searcher.Target.Position);
        }
    }

    void FaceTowardsTarget(Vector3 targetPosition)
    {
        Vector3 difference = targetPosition - transform.position;
        difference.y = 0;
        if (difference.sqrMagnitude > 0.01f)
        {
            transform.forward = difference;
        }
    }

    void TryShooting(Vector3 targetPosition)
    {
        if (Time.time >= _shootingCooldownTime)
        {
            Shoot(targetPosition);
            SetCooldownTime();
        }
    }

    void Shoot(Vector3 targetPosition)
    {
        GameObject instantiatedObject = Instantiate(_projectilePrefab, _firingPoint.position, Quaternion.LookRotation(targetPosition - _firingPoint.position));
        Projectile projectile = instantiatedObject.GetComponent<Projectile>();
        if (projectile) projectile.Attacker = _character;
    }

    void SetCooldownTime()
    {
        _shootingCooldownTime = Time.time + _firingDelay;
    }



}