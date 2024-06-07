using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Action
{
    protected override float ActionEndDelay { get; } = 0.7f;
    protected override int ActionBudgetCost { get;  } = 0;

    protected override string ActionDescription 
        => $"{_attackerShip.name} made {_attackType} to {_defenderShip.name}";
    private float _timeInBetweenProjectiles = 0.2f;
    private AttackType _attackType;
    private GameObject _attackerShip;
    private GameObject _defenderShip;

    public override void TriggerAction()
    {
        base.TriggerAction();
        StartCoroutine(HandleAttack());

    }
    private IEnumerator HandleAttack()
    {
        int[] projectileDamages = _attackDamageCalculator.GetAttackDamagePerProjectile(_attackerShip, _defenderShip, _attackType);      
        
        for (int i = 0; i < projectileDamages.Length; i++)
        {
            _projectileManager.CreateAndFireProjectile(_attackerShip, _defenderShip,
                projectileDamages[i], _attackStateManager.DetermineDodge(_defenderShip));
            yield return new WaitForSeconds(_timeInBetweenProjectiles);
        }
        StartCoroutine(EndAction());
    }

    public void SetAttackProperties(AttackType attackType, GameObject attackerShip, GameObject defenderShip)
    {
        _attackType = attackType;
        _attackerShip = attackerShip;
        _defenderShip = defenderShip;
    }

}