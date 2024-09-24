using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public  class CardsCreator : MonoBehaviour
{
   internal static Dictionary<string,Dictionary<Action<List<GameObject>>,(string,bool,Predicate<GameObject>)>> CardWithEffect = new();
   public static Player Player1 {get; private set;}
   public static Player Player2 {get; private set;}



    public static Queue<GameObject> CardCreator(string textForCompiling)
    {
        Queue<GameObject> cards = new();
        bool [] ping = new bool[]{false,true,false};
        List<CardData> cardsData = ProgrNode.CompiledCards(textForCompiling);
        
        

        foreach (var item in cardsData)
        {
            GameObject prefab= Resources.Load<GameObject>("Prefabs/Compiler Card");
            GameObject card = Instantiate(prefab);
            card.transform.position = new Vector3(1000,1000,1000);
             
            if(item.Type == "Oro" || item.Type == "Plata")
            {

                UnityCard unity_Card = new(item.Name, GetRangeUnity(item.Range), GetTypeUnity(item.Type),(int)item.Power, UnityCard.EnumEfects.EffectOfGwent_PlusPlus, GetFaction(item.Faction));

                CardWithEffect.Add(item.Name,item.EffectCard);
                card.AddComponent<UnityCardScript>();
                card.GetComponent<UnityCardScript>().unityCard = unity_Card;
                Charging_UI(card);
                
                if(card.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins){
                    GameManajer.GameManger.Assassinsdeck.GetComponent<DeckScript>().deck.Add(card);

                }else{
                    GameManajer.GameManger.Assassinsdeck.GetComponent<DeckScript>().deck.Add(card);
                }

            }else if(item.Type == "Clima" ){
                
                CardWithEffect.Add(item.Name,item.EffectCard);
                WeatherCard weather_card = new(item.Name, UnityCard.EnumTypeCard.WeatherCard, UnityCard.EnumEfects.EffectOfGwent_PlusPlus, GetRangeWheather(item.Range));
                card.AddComponent<WeatherCardScript>();
                card.GetComponent<WeatherCardScript>().weatherCard = weather_card;
                Charging_UI(card);
                cards.Enqueue(card);

            }else if(item.Type == "Aumento"){
                
                CardWithEffect.Add(item.Name,item.EffectCard);
                AumentCard aument_Card = new(item.Name, UnityCard.EnumTypeCard.AumentCard, UnityCard.EnumEfects.EffectOfGwent_PlusPlus, GetRangeUnity(item.Range), UnityCard.EnumFactionCard.Neutral);
                card.AddComponent<AumentCardScript>();
                card.GetComponent<AumentCardScript>().aumentCard = aument_Card;
                Charging_UI(card);
                cards.Enqueue(card);

            }else if(item.Type == "Lider"){
                
                CardWithEffect.Add(item.Name,item.EffectCard);
                BossCard boss_card = new(item.Name, UnityCard.EnumTypeCard.Boss, UnityCard.EnumEfects.EffectOfGwent_PlusPlus,GetFaction(item.Faction));
                card.AddComponent<BossesEfect>();
                card.GetComponent<BossesEfect>().bossCard = boss_card;
                Charging_UI(card);

                if(boss_card.FactionCard ==  UnityCard.EnumFactionCard.Assassins){
                    GameObject altair = GameObject.FindGameObjectWithTag("Altair");

                    var Altair =  altair.GetComponent<BossesEfect>();
                    altair.GetComponent<SpriteRenderer>().sprite = card.GetComponent<SpriteRenderer>().sprite;
                    Altair.Effect = UnityCard.EnumEfects.EffectOfGwent_PlusPlus;
                    Altair.NameCard = boss_card.NameCard;
                    
                }else{

                    GameObject alMualim = GameObject.FindGameObjectWithTag("Al Mualim");
                    var AlMualim =  alMualim.GetComponent<BossesEfect>();
                    alMualim.GetComponent<SpriteRenderer>().sprite = card.GetComponent<SpriteRenderer>().sprite;
                    AlMualim.Effect = UnityCard.EnumEfects.EffectOfGwent_PlusPlus;
                    AlMualim.NameCard = boss_card.NameCard;

                }

            }

        }

        return cards ;
    }

    
    private static void Charging_UI(GameObject card)
    {
        card.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/reverse");
    }

    private static Sprite GetImage(Sprite sprite)
    {
        throw new NotImplementedException();
    }

    private static UnityCard.EnumTypeUnity GetTypeUnity(string type)
    {
        if(type == "Oro")
        return UnityCard.EnumTypeUnity.Gold;
        else
        return UnityCard.EnumTypeUnity.Silver;
    }

    private static UnityCard.EnumFactionCard GetFaction(string faction)
    {
        if(faction == "Assassins")
        return UnityCard.EnumFactionCard.Assassins;
        else
        return UnityCard.EnumFactionCard.Templar;
    }

    private static UnityCard.EnumTypeAttackCard GetRangeUnity (bool[] range )
    {
        if(range[0])
        {
            return UnityCard.EnumTypeAttackCard.M;
        }else if(range[1]){
            return UnityCard.EnumTypeAttackCard.R;
        }else
        {
            return UnityCard.EnumTypeAttackCard.S;
        }
    }

    private static UnityCard.EnumTypeClimAndClean GetRangeWheather (bool[] range)
    {
        if(range[0])
        {
            return UnityCard.EnumTypeClimAndClean.M;
        }else if(range[1]){
            return UnityCard.EnumTypeClimAndClean.R;
        }else
        {
            return UnityCard.EnumTypeClimAndClean.S;
        }
    }



    // gameObject.AddComponent<UnityCardScript>();
    // gameObject.GetComponent<UnityCardScript>().
}
