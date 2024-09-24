using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weather Card", menuName = "Weather Card")]
public class WeatherCard : ScriptableObject
{
        public string NameCard;
        public UnityCard.EnumTypeCard TypeCard;
        public UnityCard.EnumEfects EffectCard;
        public UnityCard.EnumTypeClimAndClean TypeClim;
        public double IdParent;
        public WeatherCard(string name, UnityCard.EnumTypeCard typeCard, UnityCard.EnumEfects effectCard, UnityCard.EnumTypeClimAndClean typeClim)
        {
                NameCard = name;
                TypeCard = typeCard;
                EffectCard = effectCard;
                TypeClim = typeClim;
        }

    
}
