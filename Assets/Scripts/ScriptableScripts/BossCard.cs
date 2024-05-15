using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Boss Card", menuName = "Boss Card")]
public class BossCard : ScriptableObject
{
   public string NameCard;
   public UnityCard.EnumTypeCard TypeCard;
   public UnityCard.EnumFactionCard FactionCard;

}
