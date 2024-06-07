using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurpriseAttack : Action
{
    protected override float ActionEndDelay { get; } = 0f;
    protected override int ActionBudgetCost { get; } = 0;
    public override void TriggerAction()
    {
        base.TriggerAction();
        var allSurpriseAttacks = _attackStateManager.DetermineSurpriseAttacks();

        if (allSurpriseAttacks != null)
        {
            foreach (var surpriseAttack in allSurpriseAttacks)
            {
                var addedAttack = _actionQueueObject.AddComponent<Attack>();
                addedAttack.SetAttackProperties(AttackType.SurpriseAttack, surpriseAttack.attackerShip, surpriseAttack.defenderShip);
                _actionManager.EnqueueAction(addedAttack);
            }
        }

        StartCoroutine(EndAction());
    }
}
