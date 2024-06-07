using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CombatProperties : MonoBehaviour
{
    [Header("Damage Modifiers")]
    public float DamageDealtModifier = 100;
    public float DamageTakeModifier = 100;

    [Header("Base Attack Probabilities")]
    public float BaseSurpriseAttackChance = 5;
    public float BaseCounterAttackChance = 5;
    public float BaseCritAttackChance = 5;
    public float BaseDodgeChance = 5;

    [Header("Other Properties")]
    public int ProjectilePerAttack = 3;
    public int NumberOfManeuvers = 0;
    public int PercentageDamagePerManeuver = 20;


    [Header("Muzzle")]
    [SerializeField] private GameObject _muzzle;
    private Vector3 _muzzlePosition;
    private Vector3 _firingDirection;

    private List<GameObject> _allyShips = new List<GameObject>();
    private List<GameObject> _enemyShips = new List<GameObject>();
    
    private void Awake()
    {
        _muzzlePosition = _muzzle.transform.position;
        
        if (gameObject.tag == "Player" || gameObject.tag == "Ally")
        {
            _firingDirection = Vector3.right;
        }
        else
        {
            _firingDirection = Vector3.left;
        }

        SetAllyAndEnemyShips(gameObject.tag);

    }

    private void SetAllyAndEnemyShips(string shipTag)
    {
        if(shipTag == "Player" || shipTag == "Ally")
        {
            _enemyShips = GameObject.FindGameObjectsWithTag("Enemy").ToList();

            _allyShips = shipTag == "Player" ? GameObject.FindGameObjectsWithTag("Ally").ToList()
                : GameObject.FindGameObjectsWithTag("Ally").Where(allyShip => allyShip != gameObject).ToList();
            if(shipTag != "Player")
            {
                _allyShips.Add(GameObject.FindGameObjectWithTag("Player"));
            }
        }
        else if(shipTag == "Enemy")
        {
            _enemyShips = GameObject.FindGameObjectsWithTag("Ally").ToList();
            _enemyShips.Add(GameObject.FindGameObjectWithTag("Player"));
            
            _allyShips = GameObject.FindGameObjectsWithTag("Enemy").Where(allyShip => allyShip != gameObject).ToList();
        }
    }

    public List<GameObject> GetAllyShips() => _allyShips;
    public List<GameObject> GetEnemyShips() => _enemyShips;

    public Vector3 GetMuzzlePosition() => _muzzlePosition;
    public Vector3 GetFiringDirection() => _firingDirection;

}
