using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class SkillStatEffect : SkillEffect
{
    public override EffectType EffectType { get; set; } = EffectType.StatEffect;

    [Header("FLAT STAT BOOSTS")]

    [Header("Permanent")]

    public float _pfHullHealth;
    public float _pfHullRegen;
    public float _pfArmor;
    public float _pfArmorRegen;
                   
    public float _pfBaseShockPower;
    public float _pfBaseArtilleryPower;
    public float _pfBaseManeuverability;
    public float _pfBaseStealth;
    public float _pfBaseReconnaissance;
                   
    public float _pfDamageDealtModifier;
    public float _pfDamageTakeModifier;
                   
    public float _pfBaseSurpriseAttackChance;
    public float _pfBaseCounterAttackChance;
    public float _pfBaseCritAttackChance;
    public float _pfBaseDodgeChance;

    [Header("Temporary")]

    public float _tfHullHealth;
    public float _tfHullRegen;
    public float _tfArmor;
    public float _tfArmorRegen;
                   
    public float _tfBaseShockPower;
    public float _tfBaseArtilleryPower;
    public float _tfBaseManeuverability;
    public float _tfBaseStealth;
    public float _tfBaseReconnaissance;
                  
    public float _tfDamageDealtModifier;
    public float _tfDamageTakeModifier;
                  
    public float _tfBaseSurpriseAttackChance;
    public float _tfBaseCounterAttackChance;
    public float _tfBaseCritAttackChance;
    public float _tfBaseDodgeChance;

    [Header("PERCENTAGE STAT EFFECT BOOSTS")]
    
    [Header("Permanent")]
    
    public float _ppHullHealth;
    public float _ppHullRegen;
    public float _ppArmor;
    public float _ppArmorRegen;
                   
    public float _ppBaseShockPower;
    public float _ppBaseArtilleryPower;
    public float _ppBaseManeuverability;
    public float _ppBaseStealth;
    public float _ppBaseReconnaissance;
                   
    public float _ppDamageDealtModifier;
    public float _ppDamageTakeModifier;
                   
    public float _ppBaseSurpriseAttackChance;
    public float _ppBaseCounterAttackChance;
    public float _ppBaseCritAttackChance;
    public float _ppBaseDodgeChance;
 
    [Header("Temporary")]
    
    public float _tpHullHealth;
    public float _tpHullRegen;
    public float _tpArmor;
    public float _tpArmorRegen;
                   
    public float _tpBaseShockPower;
    public float _tpBaseArtilleryPower;
    public float _tpBaseManeuverability;
    public float _tpBaseStealth;
    public float _tpBaseReconnaissance;
                   
    public float _tpDamageDealtModifier;
    public float _tpDamageTakeModifier;
                   
    public float _tpBaseSurpriseAttackChance;
    public float _tpBaseCounterAttackChance;
    public float _tpBaseCritAttackChance;
    public float _tpBaseDodgeChance;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ApplyEffect();
        }
    }

    public override void ApplyEffect()
    {
        var targetShipStats = gameObject.GetComponent<ShipStats>();
        var targetCombatProperties = gameObject.GetComponent<CombatProperties>();
        
        var shipStatsFieldInfos = targetShipStats.GetType().GetFields();
        var combatPropertiesFieldInfos = targetCombatProperties.GetType().GetFields();
        
        List<FieldInfo> allFieldsForShipStats = new List<FieldInfo>();      
        foreach(var shipStatField in shipStatsFieldInfos)
        {
            allFieldsForShipStats.Add(shipStatField);
        }
        foreach (var combatPropertyField in combatPropertiesFieldInfos)
        {
            allFieldsForShipStats.Add(combatPropertyField);
        }
        
        var skillStatEffectFields = GetType().GetFields();

        foreach (var stat in allFieldsForShipStats)
        {
            List<FieldInfo> relevantFields = new List<FieldInfo>();
            relevantFields = skillStatEffectFields.Where(statEffectField => statEffectField.Name.Remove(0, 3) == stat.Name).ToList();
            float percentageModifier = default;
            float flatBoost = default;
            
            if (relevantFields.Count() > 0)
            {
                foreach(var relevantField in  relevantFields)
                {
                    var fieldValue = (float)relevantField.GetValue(this);
                    if (relevantField.Name[2] != 'f')
                    {
                        percentageModifier += fieldValue;
                    }
                    else
                    {
                        flatBoost += fieldValue;
                    }
                }
                
                object targetScript = shipStatsFieldInfos.Contains(stat) ? targetShipStats : targetCombatProperties;
        
                var statValueAsObj = stat.GetValue(targetScript);
                float currentStatValue;

                if (statValueAsObj as float? is null)
                {
                    currentStatValue = (int)statValueAsObj;
                    var targetStatChange = (int)(currentStatValue + flatBoost + ((currentStatValue + flatBoost) * percentageModifier));
                    
                    //Will be removed, QoL for me momentarily
                    if(stat.Name == "HullHealth" || stat.Name == "Armor")
                    {
                        targetStatChange = (int)(currentStatValue + flatBoost + 
                            (percentageModifier * (stat.Name == "HullHealth" ? targetShipStats.MaxHullHealth : targetShipStats.MaxArmor)));
                    }              
                    stat.SetValue(targetScript, targetStatChange);

                }
                else
                {
                    currentStatValue = (float)statValueAsObj;
                    var targetStatChange = currentStatValue + + flatBoost + ((currentStatValue + flatBoost) * percentageModifier);
                    stat.SetValue(targetScript, targetStatChange);
                }
            }         
        }

    }
    public override void RemoveEffect()
    {
        
    }

}
