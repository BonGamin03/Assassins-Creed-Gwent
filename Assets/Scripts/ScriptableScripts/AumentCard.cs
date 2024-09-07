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

    public AumentCard(string name, UnityCard.EnumTypeCard typeCard, UnityCard.EnumEfects efectCard,  UnityCard.EnumTypeAttackCard rowAument,  UnityCard.EnumFactionCard factionCard)
    {
        NameCard = name;
        TypeCard = typeCard;
        EfectCard = efectCard;
        RowAument = rowAument;
        FactionCard = factionCard;
    }
    
}
//Esta clase es para crear los Scriptable Objects de las cartas de aumento 