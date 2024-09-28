


using System;
using System.Collections.Generic;
using UnityEngine;

public class CardData
{
    internal string Name{get;set;}
    internal string Faction{get; set;}
    internal string Type {get;set;}
    internal double ? Power{get;set;}
    internal bool[] Range{get;set;}
    internal Dictionary<Action<List<GameObject>>,(string,bool,Predicate<GameObject>)> EffectCard{get;set;}
    
    public CardData(string name, string faction, string type, double power, bool[] range){
        Name = name;
        Faction = faction;
        Type = type;
        Power = power;
        Range = range;
        EffectCard = new();
    }

    public CardData(string name,string type)
    {
        Name = name;
        Type = type;
        EffectCard = new();
    }

    
}
