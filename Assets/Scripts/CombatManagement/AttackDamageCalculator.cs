using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDamageCalculator : MonoBehaviour
{
    [Header("Damage Multipliers Based On Attack Type")]
    [SerializeField] private float _critDamageMultiplier = 2f;
    [SerializeField] private float _counterDamageMultiplier = 1.2f;

    [Header("Damage Range Multipliers")]
    [SerializeField] private float _damageRangeMultiplier = 4;

    [Header("Stat Based Damage Alteration Multipliers")]
    [SerializeField] private float ManeuverDmgReductionModifier = 2f;

    public int[] GetAttackDamagePerProjectile(GameObject sourceShip, GameObject targetShip, AttackType attackType)
    {
        var sourceShipStats = sourceShip.GetComponent<ShipStats>();
        var sourceShipCombatProperties = sourceShip.GetComponent<CombatProperties>();
        var targetShipStats = targetShip.GetComponent<ShipStats>();
        var targetShipCombatProperties = targetShip.GetComponent<CombatProperties>();

        var meanDmgPerProjectile = GetArtilleryBaseAttackDamage(sourceShipStats, targetShipStats, sourceShipCombatProperties, targetShipCombatProperties);

        meanDmgPerProjectile = AdjustMeanDamageWithAttackType(meanDmgPerProjectile, sourceShipStats, attackType);
        
        var randomDamageRange = meanDmgPerProjectile / _damageRangeMultiplier;    

        var projectileDamages = new int[sourceShipCombatProperties.ProjectilePerAttack]; 
        
        for(int i = 0; i < sourceShipCombatProperties.ProjectilePerAttack; i++)
        {
            var finalProjectileDamage = GenerateNormalDamageDistributionFromBase(meanDmgPerProjectile, randomDamageRange);
            projectileDamages[i] = finalProjectileDamage;
        }

        return projectileDamages;

    }
  
    private int GenerateNormalDamageDistributionFromBase(int meanDamagePerProjectile, float damageRange)
    {
        int randomlyDistributedDamage;
        
        do
        {
            float u1 = 1.0f - Random.value;
            float u2 = 1.0f - Random.value;

            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2);
            
            randomlyDistributedDamage = (int)(meanDamagePerProjectile + damageRange * randStdNormal);
        
        } while (randomlyDistributedDamage < meanDamagePerProjectile - damageRange || randomlyDistributedDamage > meanDamagePerProjectile + damageRange);

        return randomlyDistributedDamage;
    }
       
    private int AdjustMeanDamageWithAttackType(int meanDmgPerProjectile, ShipStats sourceShip, AttackType attackType)
    {
        if (attackType == AttackType.CriticalAttack)
        {
            return GetArtilleryBaseCritDamage(meanDmgPerProjectile);
        }
        else if (attackType == AttackType.CounterAttack)
        {
            return GetCounterAttackBaseDamage(meanDmgPerProjectile);
        }
        else if (attackType == AttackType.SurpriseAttack)
        {
            return GetShockBaseDamage(sourceShip.GetComponent<ShipStats>(), GetArtilleryBaseCritDamage(meanDmgPerProjectile));
        }
        else
        {
            return meanDmgPerProjectile;
        }
    }
    
    private int GetArtilleryBaseAttackDamage(ShipStats sourceShipStats, ShipStats targetShipStats, 
        CombatProperties sourceShipCombatProperties, CombatProperties targetShipCombatProperties)
    {
        var sourceArtilleryPower = sourceShipStats.BaseArtilleryPower;
        var sourceDamageDealtModifier = sourceShipCombatProperties.DamageDealtModifier;
        var sourceProjectilePerAttack = sourceShipCombatProperties.ProjectilePerAttack;

        var targetManeuverability = targetShipStats.BaseManeuverability;
        var targetDamageTakeModifier = targetShipCombatProperties.DamageTakeModifier;
              
        var pureDamage = sourceArtilleryPower * (sourceDamageDealtModifier / 100) / sourceProjectilePerAttack;

        var targetManeuverReductionModifier = (100f - (targetManeuverability / ManeuverDmgReductionModifier)) / 100f;
        
        var targetManeuverReduction = pureDamage * targetManeuverReductionModifier;     
        var targetDamageTakeModifierReduction = targetManeuverReduction * (targetDamageTakeModifier / 100);
        
        return (int)targetDamageTakeModifierReduction;
    }
    private int GetArtilleryBaseCritDamage(int artilleryBaseAttackDamage) => (int)(artilleryBaseAttackDamage * _critDamageMultiplier);
    private int GetCounterAttackBaseDamage(int artilleryBaseAttackDamage) => (int)(artilleryBaseAttackDamage * _counterDamageMultiplier);
    private int GetShockBaseDamage(ShipStats sourceShipStats, int artilleryBaseCritDamage)
    {
        var sourceManeuverability = sourceShipStats.BaseManeuverability;
        var sourceShockPower = sourceShipStats.BaseShockPower;
        
        var sourceManeuverMultiplier = sourceManeuverability / 100f;
       
        int sourceShockPowerAddition = (int)(sourceShockPower * sourceManeuverMultiplier);
        
        return sourceShockPowerAddition + artilleryBaseCritDamage;
    }
    



}
