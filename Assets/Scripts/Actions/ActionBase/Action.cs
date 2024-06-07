using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{
    public bool ReadyToProcess = false;
    protected GameObject _actionQueueObject;
    protected ActionManager _actionManager;
    protected AttackStateManager _attackStateManager;
    protected AttackDamageCalculator _attackDamageCalculator;
    protected ProjectileManager _projectileManager;

    protected virtual int ActionBudgetCost { get; }
    protected virtual float ActionEndDelay { get; }
    protected virtual string ActionDescription { get; } = string.Empty;

    public virtual void TriggerAction()
    {
        _actionManager.ProcessingAction = true;
        _actionManager.GetCurrentlyActingShip().GetComponent<ActionBudget>()._actionBudget -= ActionBudgetCost;
        DescribeAction();
    }

    protected IEnumerator EndAction()
    {
        CheckForCounterAttacks();
        yield return new WaitForSeconds(ActionEndDelay);
        _actionManager.DequequeAction();
        _actionManager.ProcessingAction = false;
        Destroy(this);
    }

    public void SetActionProperties()
    {
        var combatManager = gameObject.transform.parent;
        _actionManager = combatManager.GetComponent<ActionManager>();
        _attackDamageCalculator = combatManager.GetComponent<AttackDamageCalculator>();
        _attackStateManager = combatManager.GetComponent<AttackStateManager>();
        _projectileManager = combatManager.GetComponent<ProjectileManager>();
        _actionQueueObject = gameObject;
        ReadyToProcess = true;
    }

    protected void CheckForCounterAttacks()
    {
        if(ActionBudgetCost > 0)
        {
            var addedCounterAttack = _actionQueueObject.AddComponent<CounterAttack>();
            addedCounterAttack.SetCounterAttackProperties(_actionManager.GetCurrentlyActingShip());
            _actionManager.EnqueueAction(addedCounterAttack);
        }
    }
    protected virtual void DescribeAction()
    {
        if(ActionDescription != string.Empty)
        {
            Debug.Log(ActionDescription);
        }
    }


}






