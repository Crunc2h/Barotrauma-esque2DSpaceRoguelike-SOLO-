using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : Action
{
    protected override float ActionEndDelay { get; } = 1f;
    protected override int ActionBudgetCost { get; } = 1;
    private GameObject _attackerShip;

    public override void TriggerAction()
    {
        base.TriggerAction();
        var enemyShips = _attackerShip.GetComponent<CombatProperties>().GetEnemyShips();

        foreach (var enemyShip in enemyShips)
        {
            var critRollResult = _attackStateManager.DetermineCriticalAttack(_attackerShip);
            var addedAttack = _actionQueueObject.AddComponent<Attack>();
            if (critRollResult)
            {
                addedAttack.SetAttackProperties(AttackType.CriticalAttack, _attackerShip, enemyShip);       
                _actionManager.EnqueueAction(addedAttack);
            }
            else
            {
                addedAttack.SetAttackProperties(AttackType.NormalAttack, _attackerShip, enemyShip);
                _actionManager.EnqueueAction(addedAttack);
            }
        }
        StartCoroutine(EndAction());
    }

    public void SetNormalAttackProperties(GameObject attackerShip) => _attackerShip = attackerShip;
}
