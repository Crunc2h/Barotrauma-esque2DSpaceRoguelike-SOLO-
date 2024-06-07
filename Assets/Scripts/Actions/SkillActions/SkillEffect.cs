using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    [Header("Skill Identifiers")]
    [SerializeField] private string _name;
    [SerializeField] private int _id;
    
    [Header("Skill Duration")]
    [SerializeField] private int _numOfTurns;
    [SerializeField] private int _numOfUses;
    
    public virtual EffectType EffectType { get; set; }
    public virtual void ApplyEffect() { }
    public virtual void RemoveEffect() { }

    public string GetSkillName() => _name;
    public int GetSkillId() => _id;
    public int GetNumOfTurns() => _numOfTurns;
    public int GetNumOfUses() => _numOfUses;
}
