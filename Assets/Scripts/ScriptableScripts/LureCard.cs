using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lure Cards", menuName = "Lure Card")]
public class LureCard : ScriptableObject
{
    public string NameCard;
    public UnityCard.EnumTypeCard TypeCard;
    public UnityCard.EnumEfects EfectCard;
    public UnityCard.EnumFactionCard FactionCard;
    public int PointAttackCard; 
}
