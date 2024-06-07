using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    private ActionManager _actionManager;
    private GameObject _actionQueueObject;
    private int _actionBudget;
    private void Start()
    {
        _actionManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<ActionManager>();
        _actionQueueObject = _actionManager.gameObject.transform.GetChild(0).gameObject;
    }
    void Update()
    {
        _actionBudget = GetComponent<ActionBudget>()._actionBudget;

        if(Input.GetKeyDown(KeyCode.R) && _actionBudget > 0 && _actionManager.GetCurrentlyActingShip() == gameObject 
            && !_actionManager.ProcessingAction)
        {
            var addedNormalAttack = _actionQueueObject.AddComponent<NormalAttack>();
            addedNormalAttack.SetNormalAttackProperties(gameObject);
            _actionManager.EnqueueAction(addedNormalAttack);
        }

        //if(Input.GetKeyDown(KeyCode.T) && _actionBudget > 0 && _actionManager.GetCurrentlyActingShip() == gameObject
        //    && !_actionManager.ProcessingAction)
        //{
        //    var addedManeuvre = _actionQueueObject.AddComponent<Maneuvre>();
        //    addedManeuvre.SetManeuvreProperties(gameObject);
        //    _actionManager.EnqueueAction(addedManeuvre);
        //}

    }
}
