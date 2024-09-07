using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unity Card", menuName = "Unity Card")]
public class UnityCard : ScriptableObject
{
    public string NameCard;
    public EnumTypeAttackCard TypeAttackCard;
    public EnumTypeCard TypeCard;
    public EnumTypeUnity TypeUnity;
    public int PointAttackCard;
    public EnumEfects EfectCard;
    public EnumFactionCard FactionCard;
    

    public UnityCard (string name, EnumTypeAttackCard typeAttackCard, EnumTypeCard typeCard, EnumTypeUnity typeUnity, int pointAttackCard, EnumEfects effectCard, EnumFactionCard factionCard )
    {
        NameCard = name;
        TypeAttackCard = typeAttackCard;
        TypeCard = EnumTypeCard.UnityCard;
        PointAttackCard = pointAttackCard;
        EfectCard = effectCard;
        FactionCard = factionCard;
    }
     
public enum EnumEfects
{
    AltairEffect,
    Al_Mualim_Effect,
    Alexios_ShayCormacEffect,
    Edward_DeSableEffect,
    Arno_GermainEfect,
    ClimM,
    ClimR,
    ClimS,
    AumentM,
    AumentR,
    AumentS,
    CleanClimM,
    CleanClimR,
    CleanClimS,
    NoEffect,
}

public enum EnumTypeAttackCard
{
    M,R,S,T,
}

public enum EnumTypeUnity 
{
    Gold,
    Silver,
}
public enum EnumFactionCard 
{
    Assassins,
    Templar,
    Neutral,
}

public enum EnumTypeClimAndClean
{
    M,R,S,
}

public enum EnumTypeCard
{
    UnityCard,WeatherCard,CleanCard,AumentCard,Lure,Boss,
}

}
