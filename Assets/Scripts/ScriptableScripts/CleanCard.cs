using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clean Card", menuName = "Clean Card")]
public class CleanCard : ScriptableObject
{
    public string NameCard;
    public UnityCard.EnumEfects EffectCard;
    public UnityCard.EnumFactionCard FactionCard;
    public UnityCard.EnumTypeClimAndClean TypeClean;

    public CleanCard(string name, UnityCard.EnumEfects effectCard, UnityCard.EnumFactionCard factionCard, UnityCard.EnumTypeClimAndClean typeClean)
    {
        NameCard = name;
        EffectCard = effectCard;
        FactionCard = factionCard;
        TypeClean = typeClean;
    }
}
//Esta clase es para crear los Scriptable Objects de las cartas de despeje 