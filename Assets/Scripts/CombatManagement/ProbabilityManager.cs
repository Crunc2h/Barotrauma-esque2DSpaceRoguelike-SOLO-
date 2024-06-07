using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProbabilityManager : MonoBehaviour
{
    [Header("Stat Effects On Counter Attack")]
    [SerializeField] private float _reconCounterAttackEffect = 0.2f;
    [SerializeField] private float _maneuverCounterAttackEffect = 0.4f;

    [Header("Stat Effects On Surprise Attack")]
    [SerializeField] private float _stealthSurpriseAttackEffect = 0.4f;

    [Header("StatEffects On Dodge")]
    [SerializeField] private float _maneuverDodgeEffect = 0.3f;

    public float GetSurpriseAttackProbability(ShipStats attackerShipStats, ShipStats defenderShipStats, CombatProperties attackerShipCombatProperties)
    {
        var attackerStealthVsDefenderRecon = (float) attackerShipStats.BaseStealth / defenderShipStats.BaseReconnaissance;

        var flatStealthBoost = attackerShipStats.BaseStealth / _stealthSurpriseAttackEffect;

        var resultProbability = attackerStealthVsDefenderRecon * (attackerShipCombatProperties.BaseSurpriseAttackChance + flatStealthBoost) / 100f;      

        return ClampProbability(resultProbability);
    }
    public float GetCriticalAttackProbability(CombatProperties attackerShipCombatProperties) => ClampProbability(attackerShipCombatProperties.BaseCritAttackChance / 100f);
    public float GetCounterAttackProbability(ShipStats defenderShipStats, ShipStats attackerShipStats, CombatProperties attackerShipCombatProperties)
    {
        var attackerVsDefenderRecon = (float) attackerShipStats.BaseReconnaissance / defenderShipStats.BaseReconnaissance;
        var attackerVsDefenderManeuvre = (float) attackerShipStats.BaseManeuverability / defenderShipStats.BaseManeuverability;

        var flatReconBoost = attackerShipStats.BaseReconnaissance * _reconCounterAttackEffect;
        var flatManeuverBoost = attackerShipStats.BaseManeuverability * _maneuverCounterAttackEffect;       
        var baseCAttackChance = attackerShipCombatProperties.BaseCounterAttackChance;

        var resultProbability = attackerVsDefenderRecon * attackerVsDefenderManeuvre * (baseCAttackChance + flatManeuverBoost + flatReconBoost) / 100f;
        
        return ClampProbability(resultProbability);

    }
    public float GetDodgeProbability(ShipStats defenderShipStats, CombatProperties defenderShipCombatProperties)
    {
        var defenderShipManeuver = defenderShipStats.BaseManeuverability;
        var baseDodgeChance = defenderShipCombatProperties.BaseDodgeChance;

        var dodgeChance = (defenderShipManeuver * _maneuverDodgeEffect + baseDodgeChance) / 100f;
        return ClampProbability(dodgeChance);
    }
    public bool Roll(float percentageProbability)
    {
        var diceRoll = Random.Range(0f, 1f);
        
        if(diceRoll <= percentageProbability)
        {
            return true;
        }
        return false;
    }
    private float ClampProbability(float unclampedProbability) => Mathf.Clamp(unclampedProbability, 0f, 1f);


}
