using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackStateManager : MonoBehaviour
{
    private ProbabilityManager _probabilityManager;

    private void Start()
    {
        _probabilityManager = GetComponent<ProbabilityManager>();
    }

    public List<(GameObject attackerShip, GameObject defenderShip)> DetermineSurpriseAttacks()
    {
        List<(GameObject attackerShip, GameObject defenderShip)> surpriseAttacks = new List<(GameObject, GameObject)>();

        var side1 = GameObject.FindGameObjectsWithTag("Ally").ToList();
        side1.Add(GameObject.FindGameObjectWithTag("Player"));
        var side2 = GameObject.FindGameObjectsWithTag("Enemy").ToList();

        surpriseAttacks = CheckSideForSurpriseAttack(surpriseAttacks, side1, side2);
        surpriseAttacks = CheckSideForSurpriseAttack(surpriseAttacks, side2, side1);
        
        if(surpriseAttacks.Count == 0)
        {
            return null;
        }
        return surpriseAttacks;
    }
    public List<GameObject> DetermineCounterAttacks(GameObject defenderShip, List<GameObject> attackerShips)
    {
        List<GameObject> counterAttacks = new List<GameObject>();
        
        foreach (var attackerShip in attackerShips)
        {
            var attackerShipStats = attackerShip.GetComponent<ShipStats>();
            var attackerShipCombatProperties = attackerShip.GetComponent<CombatProperties>();
            var defenderShipStats = defenderShip.GetComponent<ShipStats>();
                      
            var counterAttackProbability = _probabilityManager.GetCounterAttackProbability(defenderShipStats, attackerShipStats, attackerShipCombatProperties);
                      
            var rollResult = _probabilityManager.Roll(counterAttackProbability);
            
            if (rollResult)
            {
                counterAttacks.Add(attackerShip);
            }
        }
        
        if (counterAttacks.Count == 0)
        {
            return null;
        }
        return counterAttacks;
    }
    public bool DetermineCriticalAttack(GameObject attackerShip)
    {
        var attackerShipCombatProperties = attackerShip.GetComponent<CombatProperties>();
        var critProbability = _probabilityManager.GetCriticalAttackProbability(attackerShipCombatProperties);

        return _probabilityManager.Roll(critProbability);
    }
    public bool DetermineDodge(GameObject defenderShip)
    {
        var defenderShipStats = defenderShip.GetComponent<ShipStats>();
        var defenderShipCombatProperties = defenderShip.GetComponent<CombatProperties>();
        var dodgeProbability = _probabilityManager.GetDodgeProbability(defenderShipStats, defenderShipCombatProperties);

        return _probabilityManager.Roll(dodgeProbability);
    }

    private List<(GameObject attackerShip, GameObject defenderShip)> CheckSideForSurpriseAttack(List<(GameObject attackerShip, GameObject defenderShip)> resultList,
        List<GameObject> side1, List<GameObject> side2)
    {
        foreach (var allyShip in side1)
        {
            foreach (var enemyShip in side2)
            {
                var allyShipStats = allyShip.GetComponent<ShipStats>();
                var allyShipCombatProperties = allyShip.GetComponent<CombatProperties>();
                var enemyShipStats = enemyShip.GetComponent<ShipStats>();
                
                var surpriseAttackProbability = _probabilityManager.GetSurpriseAttackProbability(allyShipStats, enemyShipStats, allyShipCombatProperties);
                
                var rollResult =_probabilityManager.Roll(surpriseAttackProbability);
                
                if (rollResult)
                {
                    resultList.Add((allyShip, enemyShip));
                }
            }
        }
        return resultList;
    }
}
