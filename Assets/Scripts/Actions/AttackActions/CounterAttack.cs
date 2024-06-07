using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttack : Action
{
    protected override float ActionEndDelay { get; } = 0f;
    protected override int ActionBudgetCost { get; } = 0;
    private GameObject _defenderShip;

    public override void TriggerAction()
    {
        base.TriggerAction();
        var attackerShips = _defenderShip.GetComponent<CombatProperties>().GetEnemyShips();

        var allCounterAttackMakingEnemies = _attackStateManager.DetermineCounterAttacks(_defenderShip, attackerShips);

        if (allCounterAttackMakingEnemies != null)
        {
            foreach (var counterAttackMakingEnemy in allCounterAttackMakingEnemies)
            {
                var addedAttack = _actionQueueObject.AddComponent<Attack>();
                addedAttack.SetAttackProperties(AttackType.CounterAttack, counterAttackMakingEnemy, _defenderShip);
                _actionManager.EnqueueAction(addedAttack);
            }
        }

        StartCoroutine(EndAction());
    }

    public void SetCounterAttackProperties(GameObject defenderShip) => _defenderShip = defenderShip;
}
