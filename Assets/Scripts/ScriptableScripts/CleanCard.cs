using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clean Card", menuName = "Clean Card")]
public class CleanCard : ScriptableObject
{
    public string NameCard;
    public UnityCard.EnumEfects EfectCard;
    public UnityCard.EnumFactionCard FactionCard;
    public UnityCard.EnumTypeClimAndClean TypeClean;
}
//Esta clase es para crear los Scriptable Objects de las cartas de despeje 