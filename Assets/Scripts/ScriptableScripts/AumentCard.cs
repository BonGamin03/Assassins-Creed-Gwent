using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Aument Card", menuName = "Aument Card")]
public class AumentCard : ScriptableObject
{
    public  string NameCard;
    public UnityCard.EnumTypeCard TypeCard;  
    public UnityCard.EnumEfects EfectCard;
    public UnityCard.EnumTypeAttackCard RowAument;
    public UnityCard.EnumFactionCard FactionCard;
    
}
//Esta clase es para crear los Scriptable Objects de las cartas de aumento 