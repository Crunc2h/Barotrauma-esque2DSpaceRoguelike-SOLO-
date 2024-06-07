using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBudget : MonoBehaviour
{
    public int _actionBudget = 0;
    private ActionManager _actionManager;

    private void Start()
    {
        _actionManager = GameObject.FindGameObjectWithTag("CombatManager").GetComponent<ActionManager>();
    }

    private void Update()
    {
        if (_actionManager.GetCurrentlyActingShip() == gameObject && _actionBudget == 0
            && !_actionManager.ProcessingAction)
        {
            _actionManager.EndTurn();
        }
    }



}
