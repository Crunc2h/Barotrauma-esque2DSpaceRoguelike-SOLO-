using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    [Header("Health Stats")]
    public int MaxHullHealth = 1000;
    public int HullHealth = 1000;
    public int HullRegen = 5;
                             
    [Header("Armor Stats")]  
    public int MaxArmor = 1000;
    public int Armor = 1000;
    public int ArmorRegen = 10;
                             
    [Header("Combat Stats")]  
    public int BaseShockPower = 175;
    public int BaseArtilleryPower = 150;
    public int BaseManeuverability = 30;
    public int BaseStealth = 15;
    public int BaseReconnaissance = 15;  

}
