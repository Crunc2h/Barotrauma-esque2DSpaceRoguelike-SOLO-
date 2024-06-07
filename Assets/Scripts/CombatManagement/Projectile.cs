using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Lifetime;
    public GameObject AttackerShip { get; set; }
    public GameObject DefenderShip { get; set; }   
    public int Damage { get; set; }
    public bool WillDodge { get; set; }
    public Color ProjectileColor { private get; set; }
    public bool TeleportedToTarget { get; set; } = false;
    
    [SerializeField] private AudioSource _laserSfx;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = ProjectileColor;
    }

    private void Update()
    {
        Lifetime -= Time.deltaTime;
        if(Lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != AttackerShip.tag && collision.tag != "Projectile" && collision.tag != "Border")
        {
            var targetStats = DefenderShip.GetComponent<ShipStats>();

            if (!WillDodge)
            {
                ApplyDamage();
                Destroy(gameObject);
            }
        }

    }

    private void ApplyDamage()
    {
        var DefenderStats = DefenderShip.GetComponent<ShipStats>();

        var defenderArmor = DefenderStats.Armor;
        var defenderHullHealth = DefenderStats.HullHealth;

        if(defenderArmor > 0)
        {
            var damageMinusArmor = defenderArmor - Damage;
            if(damageMinusArmor < 0)
            { 
                defenderArmor = 0;
                defenderHullHealth = defenderHullHealth + damageMinusArmor;
            }
            else
            {
                defenderArmor = defenderArmor - Damage;
            }
        }
        else
        {
            defenderHullHealth = defenderHullHealth - Damage;
        }
        DefenderStats.Armor = defenderArmor;
        DefenderStats.HullHealth = defenderHullHealth;

        //TEST
        if(defenderHullHealth <= 0)
        {
            Destroy(DefenderShip);
        }
    }

}
