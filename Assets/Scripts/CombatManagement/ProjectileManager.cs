using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileFiringForce = 40000f;
    [SerializeField] private float _projectileLifetime = 3f;
    public void CreateAndFireProjectile(GameObject attackerShip, GameObject defenderShip, int projectileDamage, bool willHit)
    {
        var muzzlePosition = attackerShip.GetComponent<CombatProperties>().GetMuzzlePosition();
        var fireDirection = attackerShip.GetComponent<CombatProperties>().GetFiringDirection();
        var spawnedProjectile = SpawnProjectile(muzzlePosition);
        SetProjectileProperties(spawnedProjectile, attackerShip, defenderShip, projectileDamage, willHit);
        FireProjectile(spawnedProjectile.GetComponent<Rigidbody2D>(), fireDirection, _projectileFiringForce);
    }

    private GameObject SpawnProjectile(Vector3 spawnPos) => Instantiate(_projectilePrefab, spawnPos, Quaternion.identity, null);
    private void FireProjectile(Rigidbody2D projectileRb, Vector3 fireDirection, float fireForce)
    {
        projectileRb.AddForce(fireDirection * fireForce);
    }

    private void SetProjectileProperties(GameObject spawnedProjectile, GameObject attackerShip, GameObject defenderShip, int projectileDamage, bool willHit)
    {
        var projectileProperties = spawnedProjectile.GetComponent<Projectile>();
        projectileProperties.Lifetime = _projectileLifetime;
        projectileProperties.AttackerShip = attackerShip;
        projectileProperties.DefenderShip = defenderShip;
        projectileProperties.Damage = projectileDamage;
        projectileProperties.WillDodge = willHit;
        if(attackerShip.tag == "Player" || attackerShip.tag == "Ally")
        {
            projectileProperties.ProjectileColor = Color.blue;
        }
        else if(attackerShip.tag == "Enemy")
        {
            projectileProperties.ProjectileColor = Color.red;
        }
    }
}
