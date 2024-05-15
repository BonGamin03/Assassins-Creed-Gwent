using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weather Card", menuName = "Weather Card")]
public class WeatherCard : ScriptableObject
{
        public string NameCard;
        public UnityCard.EnumTypeCard TypeCard;
        public UnityCard.EnumEfects EfectCard;
        public UnityCard.EnumFactionCard FactionCard;
        public UnityCard.EnumTypeClimAndClean TypeClim;
}
