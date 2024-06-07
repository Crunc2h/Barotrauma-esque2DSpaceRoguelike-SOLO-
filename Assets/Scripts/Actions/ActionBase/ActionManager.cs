using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private Queue<Action> _allActionsQueue = new Queue<Action>();
    private List<GameObject> _allShips = new List<GameObject>();
    private GameObject _actionQueueObject;
    private GameObject _currentlyActingShip;
    private int _currentActingShipIndex = -1;

    public bool ProcessingAction = false;

    public void EnqueueAction(Action action)
    {
        action.SetActionProperties();
        _allActionsQueue.Enqueue(action);
    }
    public void DequequeAction() => _allActionsQueue.Dequeue();

    private void Awake()
    {
        _actionQueueObject = gameObject.transform.GetChild(0).gameObject;
        SetAllShips();      
    }
    private void Start()
    {
        var addedSurpriseAttack = _actionQueueObject.AddComponent<SurpriseAttack>();
        EnqueueAction(addedSurpriseAttack);
        EndTurn();
        _currentlyActingShip = _allShips[_currentActingShipIndex];
    }
    private void Update()
    {     
        
        if(_allActionsQueue.Count > 0 && !ProcessingAction && _allActionsQueue.First().ReadyToProcess)
        {
            _allActionsQueue.First().TriggerAction();
        }       
    }

    private void SetAllShips()
    {
        _allShips.Add(GameObject.FindGameObjectWithTag("Player"));
        var allyShips = GameObject.FindGameObjectsWithTag("Ally");
        var enemyShips = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var allyShip in allyShips)
        {
            _allShips.Add(allyShip);
        }
        foreach(var enemyShip in enemyShips)
        {
            _allShips.Add(enemyShip);
        }
    }
    public GameObject GetCurrentlyActingShip() => _currentlyActingShip;

    public void EndTurn()
    {
        if (_currentActingShipIndex == _allShips.Count - 1)
        {
            _currentActingShipIndex = 0;
        }
        else
        {
            _currentActingShipIndex++;
        }
        _allShips[_currentActingShipIndex].GetComponent<ActionBudget>()._actionBudget = 1;
        _currentlyActingShip = _allShips[_currentActingShipIndex];
    }

}
