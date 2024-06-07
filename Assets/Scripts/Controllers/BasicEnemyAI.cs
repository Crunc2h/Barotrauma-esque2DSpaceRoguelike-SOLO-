using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour
{
    private ActionManager _actionManager;
    private ProbabilityManager _probabilityManager;
    private GameObject _actionQueueObject;
    private int _actionBudget;
    private void Start()
    {
        _actionManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<ActionManager>();
        _actionQueueObject = _actionManager.gameObject.transform.GetChild(0).gameObject;
        _probabilityManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<ProbabilityManager>();
    }
    private void Update()
    {
        //var currentlyActingShip = _actionManager.GetCurrentlyActingShip();
        //_actionBudget = GetComponent<ActionBudget>()._actionBudget;

        //if (currentlyActingShip == gameObject && !_actionManager.ProcessingAction && _actionBudget > 0)
        //{
        //    var rollResult = _probabilityManager.Roll(0.6f);
        //    if(rollResult)
        //    {
        //        var addedNormalAttack = _actionQueueObject.AddComponent<NormalAttack>();
        //        addedNormalAttack.SetNormalAttackProperties(gameObject);               
        //        _actionManager.EnqueueAction(addedNormalAttack);
        //    }
        //    else
        //    {
        //        var addedManeuvre = _actionQueueObject.AddComponent<Maneuvre>();
        //        addedManeuvre.SetManeuvreProperties(gameObject);
        //        _actionManager.EnqueueAction(addedManeuvre);
        //    }
        //}
    }
}
