using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : Action
{
    protected override float ActionEndDelay { get; } = 1f;
    protected override int ActionBudgetCost { get; } = 1;
    protected override string ActionDescription => $"{_maneuvringShip} made maneuver!";
    private GameObject _maneuvringShip;

    public override void TriggerAction()
    {
        base.TriggerAction();
        


        StartCoroutine(EndAction());
    }

    public void AddActiveSkillToQueue(int skillId)
    {
        var addedSkill = _actionQueueObject.AddComponent<ActiveSkill>();
        _actionManager.EnqueueAction(addedSkill);
    }

  
}







//maneuver
//var currentShipCombatProperties = _maneuvringShip.GetComponent<CombatProperties>();

//var numberOfManeuvers = currentShipCombatProperties.NumberOfManeuvers;

//var currentDamageDealtModifier = currentShipCombatProperties.DamageDealtModifier;
//var percentageAdditionPerManeuver = currentShipCombatProperties.PercentageDamagePerManeuver;

//currentShipCombatProperties.DamageDealtModifier = currentDamageDealtModifier + percentageAdditionPerManeuver;