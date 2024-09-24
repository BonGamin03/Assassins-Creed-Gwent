using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public  class Effects : MonoBehaviour
{

    static GameObject GameManager = GameObject.FindGameObjectWithTag("Game Manager");
    static GameObject Board =  GameObject.FindGameObjectWithTag("board");

    public static void GetTipe(GameObject card)
    {
        if(card.TryGetComponent(out UnityCardScript component1))
        {
            PortalEffect(component1.EfectCard,card);
        }
        else if(card.TryGetComponent(out BossesEfect component2))
        {
            PortalEffect(component2.Effect,card);
        }else if(card.TryGetComponent(out WeatherCardScript component3))
        {
            PortalEffect(component3.EfectCard, card);
        }else if(card.TryGetComponent(out AumentCardScript component4))
        {
            PortalEffect(component4.EfectCard,card);
        }
    }
    public static void PortalEffect (UnityCard.EnumEfects enumEfects, GameObject gameObject)
    {
       
        switch (enumEfects)
        {
            case UnityCard.EnumEfects.NoEffect:
            return;
            
            case UnityCard.EnumEfects.EffectOfGwent_PlusPlus:
            EffectCompiler(gameObject);
            return;

            case  UnityCard.EnumEfects.AltairEffect:
            BossesEffect(gameObject);
            return;

            case UnityCard.EnumEfects.Al_Mualim_Effect:
            BossesEffect(gameObject);
            return;

            case UnityCard.EnumEfects.Edward_DeSableEffect:
            Edward_LaureanoEffect(gameObject);
            return;

            case UnityCard.EnumEfects.Arno_GermainEfect:
            PortalArno_GermainEffect(gameObject);
            return;

            case UnityCard.EnumEfects.Alexios_ShayCormacEffect:
            PortalAlexios_ShayCormacEffect(gameObject);
            return;

            case UnityCard.EnumEfects.ClimM:
            WeatherEffect(gameObject);
            return;

            case UnityCard.EnumEfects.ClimR:
            WeatherEffect(gameObject);
            return;

             case UnityCard.EnumEfects.ClimS:
            WeatherEffect(gameObject);
            return;

             case UnityCard.EnumEfects.CleanClimM:
            CleanEffect(gameObject);
            return;

             case UnityCard.EnumEfects.CleanClimR:
            CleanEffect(gameObject);
            return;

             case UnityCard.EnumEfects.CleanClimS:
            CleanEffect(gameObject);
            return;

             case UnityCard.EnumEfects.AumentM:
            AumentEffect(gameObject);
            return;
            
             case UnityCard.EnumEfects.AumentR:
            AumentEffect(gameObject);
            return;

             case UnityCard.EnumEfects.AumentS:
            AumentEffect(gameObject);
            return;

            default:
            return;
        }
    }


    public static void EffectCompiler(GameObject gameObject)
    {
       if(gameObject.TryGetComponent(out UnityCardScript unityCard))
       {
          GetGwentEffect(unityCard.GetComponent<UnityCardScript>().NameCard); 

       }else if(gameObject.TryGetComponent(out WeatherCardScript weatherCard)){

          GetGwentEffect(weatherCard.GetComponent<WeatherCardScript>().NameCard);

       }else if(gameObject.TryGetComponent(out AumentCardScript aumentCard)){

          GetGwentEffect(aumentCard.GetComponent<AumentCardScript>().NameCard);

       }else if(gameObject.TryGetComponent(out BossesEfect bossCard)){

          GetGwentEffect(bossCard.GetComponent<BossesEfect>().NameCard);
       }
    }

    private static void GetGwentEffect(string nameCard)
    {
       try
       {
          Dictionary<Action<List<GameObject>>,(string,bool, Predicate<GameObject>)> effectValue = CardsCreator.CardWithEffect[nameCard];


          foreach (var item in effectValue)
          {
             item.Key.Invoke(GetTargets(item.Value));
          }
       }
       catch (KeyNotFoundException)
       {
          return;
       }
    }
    
      //*****************************************************************************
      //*****************************************************************************
    private static List<GameObject> GetTargets((string,bool,Predicate<GameObject>) selector)
    {
        List<GameObject> targets = new();
        
        if(selector.Item1 is null) return targets;
        
        if(selector.Item1.Equals("hand")){

          //targets = CompilerManager.GetPlayer().HandOfPlayer();
          ExtGetTargets(targets,selector);
        
        }else if(selector.Item1.Equals("otherHand")){

          //targets = CompilerManager.GetOtherPlayer().HandOfPlayer();
          ExtGetTargets(targets,selector);
        }else if(selector.Item1.Equals("deck")){
          
          targets = CompilerManager.GetPlayer().DeckOfPlayer();
          ExtGetTargets(targets,selector);
        }else if(selector.Item1.Equals("otherDeck")){

          targets = CompilerManager.GetOtherPlayer().DeckOfPlayer();
          ExtGetTargets(targets,selector);
        }else if(selector.Item1.Equals("field")){

          targets = CompilerManager.GetPlayer().FieldOfPlayer().AllCards;
          ExtGetTargets(targets,selector);
        }else if(selector.Item1.Equals("otherField")){

          targets = CompilerManager.GetOtherPlayer().FieldOfPlayer().AllCards;
          ExtGetTargets(targets,selector);
        }else if (selector.Item1.Equals("board")){
          targets = CompilerManager.GetPlayer().FieldOfPlayer().AllCards.Concat(CompilerManager.GetOtherPlayer().FieldOfPlayer().AllCards).ToList();
          ExtGetTargets(targets, selector);
        }

      return targets;
    }

    public static void ExtGetTargets(List<GameObject> targets,(string,bool,Predicate<GameObject>) selector){
      
      List<GameObject> realTargets = targets.ToList();
      
      foreach (var target in realTargets)
      {
         if(selector.Item3 is not null && !selector.Item3.Invoke(target))
         targets.Remove(target);

      }
          
          if(selector.Item2 is true){

            if(targets.Count > 1) targets.RemoveRange(1,targets.Count-2);
          }
    }

    public static void PortalAlexios_ShayCormacEffect(GameObject gameObject)
    {
        if(gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins && !gameObject.GetComponent<UnityCardScript>().EfectActivated && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
          Alexios_ShayCormacEffect(Board.GetComponent<BoardScript>().TemplarsMAttack,Board.GetComponent<BoardScript>().TemplarsRAttack,Board.GetComponent<BoardScript>().TemplarSAttack, gameObject);
        if(gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar && !gameObject.GetComponent<UnityCardScript>().EfectActivated && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
          Alexios_ShayCormacEffect(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack, gameObject);
    }
    private static void Alexios_ShayCormacEffect(List<GameObject>Mattack,List<GameObject> Rattack,List<GameObject> Sattack, GameObject gameObject)
    {
        GameObject StrongestCard = BiggestCard(Mattack,Rattack,Sattack);

        for (int i = 0; i < Mattack.Count; i++)
        {
          if(!Mattack[i].CompareTag("Lure Card"))
          {
            if ( Mattack[i].GetComponent<UnityCardScript>().NameCard == StrongestCard.GetComponent<UnityCardScript>().NameCard ) // aqui tambien falta lo del senuelo 
            {

              if(Mattack[i].GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar)
              {
                GameManager.GetComponent<GameManajer>().TemplarPoints -= Mattack[i].GetComponent<UnityCardScript>().PointAttackCard;
                Board.GetComponent<BoardScript>().maskTemplarMattack[i] = false; // Human trails
              }else
              {
                GameManager.GetComponent<GameManajer>().AssassinPoints -= Mattack[i].GetComponent<UnityCardScript>().PointAttackCard;
                Board.GetComponent<BoardScript>().maskAssassinMattack[i] = false; //Human Trails
              }
      
              Destroy(Mattack[i]);
              Mattack.RemoveAt(i);
              gameObject.GetComponent<UnityCardScript>().EfectActivated  = true;
              return;
            }
          }
        }
        for (int i = 0; i < Rattack.Count; i++)
        {
           if(!Rattack[i].CompareTag("Lure Card"))
          {
            if (Rattack[i].GetComponent<UnityCardScript>().NameCard == StrongestCard.GetComponent<UnityCardScript>().NameCard) 
            {
              if(Rattack[i].GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar)
              {
                GameManager.GetComponent<GameManajer>().TemplarPoints -= Rattack[i].GetComponent<UnityCardScript>().PointAttackCard;
                Board.GetComponent<BoardScript>().maskTemplarRattack[i] = false; // Human trails
              }else
              {
                GameManager.GetComponent<GameManajer>().AssassinPoints -= Rattack[i].GetComponent<UnityCardScript>().PointAttackCard;
                Board.GetComponent<BoardScript>().maskAssassinRattack[i] = false; // Human trails
              }

              Destroy(Rattack[i]);
              Rattack.RemoveAt(i);
              gameObject.GetComponent<UnityCardScript>().EfectActivated = true;
              return;  
            }
          }
        }
        for (int i = 0; i < Sattack.Count; i++)
        {
          if(!Sattack[i].CompareTag("Lure Card"))
          {
            if (Sattack[i].GetComponent<UnityCardScript>().NameCard == StrongestCard.GetComponent<UnityCardScript>().NameCard ) 
            {
              if(Sattack[i].GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar)
              {
                GameManager.GetComponent<GameManajer>().TemplarPoints -= Sattack[i].GetComponent<UnityCardScript>().PointAttackCard;
                Board.GetComponent<BoardScript>().maskTemplarSattack[i] = false;
              }else
              {
                GameManager.GetComponent<GameManajer>().AssassinPoints -= Sattack[i].GetComponent<UnityCardScript>().PointAttackCard;
                Board.GetComponent<BoardScript>().maskAssassinRattack[i] = false;
              }
              Destroy(Sattack[i]);
              Sattack.RemoveAt(i);
              gameObject.GetComponent<UnityCardScript>().EfectActivated = true;
              return;
            }
          }
        }
    }

    public static GameObject BiggestCard(List<GameObject>Mattack,List<GameObject>Rattack,List<GameObject>Sattack)
      {
        GameObject StrongestCard = new(); 
        int maxPoints = int.MinValue;

        for (int i = 0; i < Mattack.Count; i++)
        {
          if(!Mattack[i].CompareTag("Lure Card") && Mattack[i].GetComponent<UnityCardScript>().PointAttackCard > maxPoints) //Agrege lo del senuelo porque intenta acceder a <UnityCardScript> 
          {
            StrongestCard = Mattack[i];
            maxPoints     = Mattack[i].GetComponent<UnityCardScript>().PointAttackCard;
          }
        }
        for (int i = 0; i < Rattack.Count; i++)
        {
          if(!Rattack[i].CompareTag("Lure Card") &&Rattack[i].GetComponent<UnityCardScript>().PointAttackCard > maxPoints)
          {
            StrongestCard = Rattack[i];
            maxPoints     = Rattack[i].GetComponent<UnityCardScript>().PointAttackCard;
          }
        }
        for (int i = 0; i < Sattack.Count; i++)
        {
          if(!Sattack[i].CompareTag("Lure Card") && Sattack[i].GetComponent<UnityCardScript>().PointAttackCard > maxPoints)
          {
            StrongestCard = Sattack[i];
            maxPoints     = Sattack[i].GetComponent<UnityCardScript>().PointAttackCard;
          }
        }
        return StrongestCard;
      }

      public static GameObject WeakestCard(List<GameObject>Mattack,List<GameObject>Rattack,List<GameObject>Sattack)
      {
        GameObject WeakestCard = new(); 
        int minPoints = int.MaxValue;

        for (int i = 0; i < Mattack.Count; i++)
        {
          if(!Mattack[i].CompareTag("Lure Card") && Mattack[i].GetComponent<UnityCardScript>().PointAttackCard < minPoints) //Agrege lo del senuelo porque intenta acceder a <UnityCardScript> 
          {
            WeakestCard = Mattack[i];
            minPoints     = Mattack[i].GetComponent<UnityCardScript>().PointAttackCard;
          }
        }
        for (int i = 0; i < Rattack.Count; i++)
        {
          if(!Rattack[i].CompareTag("Lure Card") &&Rattack[i].GetComponent<UnityCardScript>().PointAttackCard < minPoints)
          {
            WeakestCard = Rattack[i];
            minPoints     = Rattack[i].GetComponent<UnityCardScript>().PointAttackCard;
          }
        }
        for (int i = 0; i < Sattack.Count; i++)
        {
          if(!Sattack[i].CompareTag("Lure Card") && Sattack[i].GetComponent<UnityCardScript>().PointAttackCard < minPoints)
          {
            WeakestCard = Sattack[i];
            minPoints     = Sattack[i].GetComponent<UnityCardScript>().PointAttackCard;
          }
        }
        return WeakestCard;
      }

      private static void Edward_LaureanoEffect(GameObject gameObject)
      {
        
        if (!gameObject.GetComponent<UnityCardScript>().EfectActivated)
        {
          gameObject.GetComponent<UnityCardScript>().EfectActivated = true;
          
          int RandomIndexCardAssassin = UnityEngine.Random.Range(0,GameManajer.GameManger.Assassinsdeck.GetComponent<DeckScript>().deck.Count);
          int RandomIndexCardTemplar = UnityEngine.Random.Range(0,GameManajer.GameManger.Templarsdeck.GetComponent<DeckScript>().deck.Count);

          if(gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
          {

              GameObject drawCardAssassin = Instantiate(GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().deck[RandomIndexCardAssassin],GameManager.GetComponent<GameManajer>().PositionHandAssassin[GameManajer.PositionInNull(GameManager.GetComponent<GameManajer>().CardsHandAssassin)].transform.position,UnityEngine.Quaternion.identity);
              drawCardAssassin.transform.SetParent(GameManager.GetComponent<GameManajer>().HandPlayerAssassin.transform, false);
              drawCardAssassin.transform.position = GameManager.GetComponent<GameManajer>().PositionHandAssassin[GameManajer.PositionInNull(GameManager.GetComponent<GameManajer>().CardsHandAssassin)].transform.position;
              drawCardAssassin.transform.localScale = gameObject.transform.localScale;
              GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().RemoveAt(RandomIndexCardAssassin);
              GameManajer.AddCardToHand(GameManager.GetComponent<GameManajer>().CardsHandAssassin,drawCardAssassin);
          }
            
          if (gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
          {
                  GameObject drawCardTemplar = Instantiate(GameManager.GetComponent<GameManajer>().Templarsdeck.GetComponent<DeckScript>().deck[RandomIndexCardTemplar],GameManager.GetComponent<GameManajer>().PositionHandTemplar[GameManajer.PositionInNull(GameManager.GetComponent<GameManajer>().CardsHandTemplar)].transform.position,GameManager.GetComponent<GameManajer>().Templarsdeck.transform.rotation);
                  drawCardTemplar.transform.SetParent(GameManager.GetComponent<GameManajer>().HandPlayerTemplar.transform, false);
                  drawCardTemplar.transform.position = GameManager.GetComponent<GameManajer>().PositionHandTemplar[GameManajer.PositionInNull(GameManager.GetComponent<GameManajer>().CardsHandTemplar)].transform.position;
                  drawCardTemplar.transform.localScale = gameObject.transform.localScale; //cuidado con esto 
                  GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().RemoveAt(RandomIndexCardTemplar);
                  GameManajer.AddCardToHand(GameManager.GetComponent<GameManajer>().CardsHandTemplar,drawCardTemplar);
            }
        }
      }

      private static void PortalArno_GermainEffect(GameObject gameObject)
      {
        var _card = gameObject.GetComponent<UnityCardScript>();
        if(_card.FactionCard == UnityCard.EnumFactionCard.Assassins && !_card.EfectActivated && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
          Arno_GermainEffect(Board.GetComponent<BoardScript>().TemplarsMAttack,Board.GetComponent<BoardScript>().TemplarsRAttack,Board.GetComponent<BoardScript>().TemplarSAttack);
        
         if(_card.FactionCard == UnityCard.EnumFactionCard.Templar && !_card.EfectActivated && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
          Arno_GermainEffect(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack);
      }

      public static void Arno_GermainEffect(List<GameObject> Mattack, List<GameObject> Rattack, List<GameObject> Sattack)
      {

        if(Mattack.Count ==0 && Rattack.Count == 0 && Sattack.Count != 0)
        {
          DestroyGameObjects(Sattack);
        }
        else if(Mattack.Count ==0 && Rattack.Count != 0 && Sattack.Count == 0)
        {
          DestroyGameObjects(Rattack);
        }
        else if(Mattack.Count !=0 && Rattack.Count == 0 && Sattack.Count == 0)
        {
          DestroyGameObjects(Mattack);
        }
        else if(Mattack.Count ==0 && Rattack.Count != 0 && Sattack.Count != 0)
        {
          if(Rattack.Count > Sattack.Count)
          {
            DestroyGameObjects(Sattack);
          }else{
            DestroyGameObjects(Rattack);
          }
        }
        else if(Mattack.Count !=0 && Rattack.Count == 0 && Sattack.Count != 0)
        {
          if(Mattack.Count > Sattack.Count)
          {
            DestroyGameObjects(Sattack);
          }else{
            DestroyGameObjects(Mattack);
          }
        }
        else if(Mattack.Count !=0 && Rattack.Count != 0 && Sattack.Count == 0)
        {
          if(Mattack.Count > Rattack.Count)
          {
            DestroyGameObjects(Rattack);
          }else{
            DestroyGameObjects(Mattack);
          }
        }
        else
        {
          int n = Math.Min(Mattack.Count,Math.Min(Rattack.Count,Sattack.Count));
          if(n == Mattack.Count){
          DestroyGameObjects(Mattack);
          return;
          }
          if(n == Rattack.Count){
          DestroyGameObjects(Rattack);
          return;
          }
          if(n == Mattack.Count){
          DestroyGameObjects(Mattack);
          return;
          }
        }
      }

      //Efecto del promedio
      public static void EffectPromedy(GameObject gameObject)
      {
          gameObject.GetComponent<UnityCardScript>().AlreadySum = true;
          int promedy = 0;
          promedy = Promedy(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack,Board.GetComponent<BoardScript>().TemplarsMAttack,Board.GetComponent<BoardScript>().TemplarsRAttack,Board.GetComponent<BoardScript>().TemplarSAttack);
          GameManager.GetComponent<GameManajer>().AssassinPoints = promedy * CounterOfUnitys(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack);
          GameManager.GetComponent<GameManajer>().TemplarPoints = promedy * CounterOfUnitys(Board.GetComponent<BoardScript>().TemplarsMAttack,Board.GetComponent<BoardScript>().TemplarsRAttack,Board.GetComponent<BoardScript>().TemplarSAttack);
      }
      private static int Promedy(List<GameObject>MattackAssassins,List<GameObject>MattackTemplars,List<GameObject>RattackAssassins,List<GameObject>RattackTemplars,List<GameObject>SattackAssassins,List<GameObject>SattackTemplars)
      {
          int TotalSum = 0;
          int count = 0;

          for (int i = 0; i < MattackAssassins.Count; i++)
          {
            if(MattackAssassins[i] != null && !MattackAssassins[i].CompareTag("Lure Card"))
            {
              TotalSum += MattackAssassins[i].GetComponent<UnityCardScript>().PointAttackCard;
              count++;
            }
          }

           for (int i = 0; i < RattackAssassins.Count; i++)
          {
            if( RattackAssassins[i] != null && !RattackAssassins[i].CompareTag("Lure Card"))
            {
              TotalSum += RattackAssassins[i].GetComponent<UnityCardScript>().PointAttackCard;
              count++;
            }
          }

           for (int i = 0; i < SattackAssassins.Count; i++)
          {
            if(SattackAssassins[i] != null && !SattackAssassins[i].CompareTag("Lure Card"))
            {
              TotalSum +=SattackAssassins[i].GetComponent<UnityCardScript>().PointAttackCard;
              count++;
            }
          }
          for (int i = 0; i < MattackTemplars.Count; i++)
          {
            if(MattackTemplars[i] != null && !MattackTemplars[i].CompareTag("Lure Card"))
            {
              TotalSum += MattackTemplars[i].GetComponent<UnityCardScript>().PointAttackCard;
              count++;
            }
          }
          for (int i = 0; i < RattackTemplars.Count; i++)
          {
            if(RattackTemplars[i] != null && !RattackTemplars[i].CompareTag("Lure Card"))
            {
              TotalSum += RattackTemplars[i].GetComponent<UnityCardScript>().PointAttackCard;
              count++;
            }
          }
          for (int i = 0; i < SattackTemplars.Count; i++)
          {
            if(SattackTemplars[i] != null && !SattackTemplars[i].CompareTag("Lure Card"))
            {
              TotalSum += SattackTemplars[i].GetComponent<UnityCardScript>().PointAttackCard;
              count++;
            }
          }

          return TotalSum / count;
      }

      //Poner un aumento
      public static void PutAnAument(GameObject gameObject)
      {
        if(gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins)
        {
          for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Count; i++)
          {
            if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null && GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Aument Card"))
            {
              if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<AumentCardScript>().RowAument)
              {
                if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.M)
                {
                   GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].transform.position = Board.GetComponent<BoardScript>().AssassinsAumentM.transform.position;
                   Board.GetComponent<BoardScript>().MaskAssassinsAumentM = true;
                   Board.GetComponent<BoardScript>().AumentAssassins.Add(gameObject);
                   GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] = null;
              
                   for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsMAttack.Count; j++)
                   {
                      if(Board.GetComponent<BoardScript>().AssassinsMAttack[j] != null)
                      { 
                         if(!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsMAttack[j]))
                         {
                            GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                         }
                      }
                  }
                }

                if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.R)
                {
                   GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].transform.position = Board.GetComponent<BoardScript>().AssassinsAumentR.transform.position;
                   Board.GetComponent<BoardScript>().MaskAssassinsAumentR = true;
                   Board.GetComponent<BoardScript>().AumentAssassins.Add(gameObject);
                   GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] = null;
              
                   for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsRAttack.Count; j++)
                   {
                      if(Board.GetComponent<BoardScript>().AssassinsRAttack[j] != null)
                      { 
                         if(!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsRAttack[j]))
                         {
                            GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                         }
                      }
                  }
                }

                if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.S)
                {
                   GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].transform.position = Board.GetComponent<BoardScript>().AssassinsAumentS.transform.position;
                   Board.GetComponent<BoardScript>().MaskAssassinsAumentS = true;
                   Board.GetComponent<BoardScript>().AumentAssassins.Add(gameObject);
                   GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] = null;
              
                   for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsSAttack.Count; j++)
                   {
                      if(Board.GetComponent<BoardScript>().AssassinsSAttack[j] != null)
                      { 
                         if(!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsSAttack[j]))
                         {
                            GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                         }
                      }
                  }
                }
              }
            }
          }
        }

        if(gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar)
        {
          for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Count; i++)
          {
            if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] != null && GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].CompareTag("Aument Card"))
            {
              if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<AumentCardScript>().RowAument)
              {
                if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.M)
                {
                   GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].transform.position = Board.GetComponent<BoardScript>().TemplarsAumentM.transform.position;
                   Board.GetComponent<BoardScript>().MaskTemplarsAumentM = true;
                   Board.GetComponent<BoardScript>().AumentTemplars.Add(gameObject);
                   GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] = null;
              
                   for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarsMAttack.Count; j++)
                   {
                      if(Board.GetComponent<BoardScript>().TemplarsMAttack[j] != null)
                      { 
                         if(!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsMAttack[j]))
                         {
                            GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                         }
                      }
                  }
                }

                if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.R)
                {
                   GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].transform.position = Board.GetComponent<BoardScript>().TemplarsAumentR.transform.position;
                   Board.GetComponent<BoardScript>().MaskTemplarsAumentR = true;
                   Board.GetComponent<BoardScript>().AumentTemplars.Add(gameObject);
                   GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] = null;
              
                   for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarsRAttack.Count; j++)
                   {
                      if(Board.GetComponent<BoardScript>().TemplarsRAttack[j] != null)
                      { 
                         if(!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsRAttack[j]))
                         {
                            GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                         }
                      }
                  }
                }

                if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.S)
                {
                   GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].transform.position = Board.GetComponent<BoardScript>().TemplarsAumentS.transform.position;
                   Board.GetComponent<BoardScript>().MaskTemplarsAumentS = true;
                   Board.GetComponent<BoardScript>().AumentTemplars.Add(gameObject);
                   GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] = null;
              
                   for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarSAttack.Count; j++)
                   {
                      if(Board.GetComponent<BoardScript>().TemplarSAttack[j] != null)
                      { 
                         if(!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarSAttack[j]))
                         {
                            GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                         }
                      }
                    }
                  }
              }
            }
          }
        }
      }

      public static void BossesEffect(GameObject gameObject)
      {

            var _boss = gameObject.GetComponent<BossesEfect>();
            if(!_boss.IsPlayed && _boss.factionCard == UnityCard.EnumFactionCard.Assassins && GameManager.GetComponent<GameManajer>().AssassinPlay)
                {
                if(Board.GetComponent<BoardScript>().AssassinsMAttack.Count != 0 || Board.GetComponent<BoardScript>().AssassinsRAttack.Count != 0 || Board.GetComponent<BoardScript>().AssassinsSAttack.Count != 0)
                {
                    gameObject.GetComponent<BossesEfect>().IsPlayed = true; // revisar
                    GameObject Strongest = UnityCardScript.BiggestCard(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack);
                    GameManager.GetComponent<GameManajer>().AssassinPoints += Strongest.GetComponent<UnityCardScript>().PointAttackCard;
                    GameManager.GetComponent<GameManajer>().AssassinPlay = false;
                    GameManager.GetComponent<GameManajer>().TemplarsPlay = true;
                }
                }
                if(!_boss.IsPlayed && _boss.factionCard == UnityCard.EnumFactionCard.Templar && GameManager.GetComponent<GameManajer>().TemplarsPlay )
                {
                    gameObject.GetComponent<BossesEfect>().IsPlayed = true;
                    GameManager.GetComponent<GameManajer>().AssassinPoints -= 7;
                    GameManager.GetComponent<GameManajer>().AssassinPlay = true;
                    GameManager.GetComponent<GameManajer>().TemplarsPlay = false;
                }
      }
       private static int CounterOfUnitys (List<GameObject>Mattack,List<GameObject>Rattack,List<GameObject>Sattack)
      {
          int count = 0;

          for (int i = 0; i < Mattack.Count; i++)
          {
            if(Mattack[i] != null && !Mattack[i].CompareTag("Lure Card"))
            {
              count++;
            }
          }

           for (int i = 0; i < Rattack.Count; i++)
          {
            if(Rattack[i] != null && !Rattack[i].CompareTag("Lure Card"))
            {
              count++;
            }
          }

           for (int i = 0; i < Sattack.Count; i++)
          {
            if(Sattack[i] != null && !Sattack[i].CompareTag("Lure Card"))
            {
              count++;
            }
          }

          return count;
      }

      // public int SumRow(List<GameObject> row)
      
      //Metodo para eliminar una fila
      public static void DestroyGameObjects(List<GameObject> row)
      {
        for (int i = 0; i < row.Count; i++)
        {
          row[i].transform.position += new UnityEngine.Vector3(1000,1000,1000);
        }
        row.Clear();
      }

#region WeatherEffects
    
    public static void WeatherEffect(GameObject gameObject)
    {
       
        if(GameManajer.IsInHand(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject) && GameManager.GetComponent<GameManajer>().AssassinPlay && !gameObject.GetComponent<WeatherCardScript>().IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
        {
            gameObject.GetComponent<WeatherCardScript>().IsPlayed = true;
            GameManager.GetComponent<GameManajer>().maskPositionHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = false;
            if(gameObject.GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.M)
            {
                gameObject.transform.position = Board.GetComponent<BoardScript>().AssassinsWeatherM.transform.position;
                Board.GetComponent<BoardScript>().MaskAssassinsWeatherM = true;
                Board.GetComponent<BoardScript>().WeatherAssassins.Add(gameObject);
                GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = null;
                for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsMAttack.Count; i++)
                {
                        if(Board.GetComponent<BoardScript>(). AssassinsMAttack[i] != null && !Board.GetComponent<BoardScript>().AssassinsMAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsMAttack[i])) //Probar que el null no sea aqui
                        {
                            Board.GetComponent<BoardScript>().AssassinsMAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                            GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                        }

                }
                for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarsMAttack.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>(). TemplarsMAttack[i] != null && !Board.GetComponent<BoardScript>().TemplarsMAttack[i].CompareTag("Lure Card") &&!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsMAttack[i]))
                    {
                        Board.GetComponent<BoardScript>().TemplarsMAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                    }
                }
            }

            if(gameObject.GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.R)
            {
                gameObject.transform.position = Board.GetComponent<BoardScript>().AssassinsWeatherR.transform.position;
                Board.GetComponent<BoardScript>().MaskAssassinsWeatherR = true;
                
                Board.GetComponent<BoardScript>().WeatherAssassins.Add(gameObject);
                GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = null;

                for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsRAttack.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>(). AssassinsRAttack[i] != null && !Board.GetComponent<BoardScript>(). AssassinsRAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsRAttack[i]))
                    {
                        Board.GetComponent<BoardScript>().AssassinsRAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                    }

                }

                for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarsRAttack.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>(). TemplarsRAttack[i] != null && !Board.GetComponent<BoardScript>().TemplarsRAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsRAttack[i]))
                    {
                        Board.GetComponent<BoardScript>().TemplarsRAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                    }
                }
            }

            if(gameObject.GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.S)
            {
        
                gameObject.transform.position = Board.GetComponent<BoardScript>().AssassinsWeatherS.transform.position;
                Board.GetComponent<BoardScript>().MaskAssassinsWeatherS = true;
                Board.GetComponent<BoardScript>().WeatherAssassins.Add(gameObject);
                GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = null;

                for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsSAttack.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>(). AssassinsSAttack[i] != null && !Board.GetComponent<BoardScript>(). AssassinsSAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsSAttack[i]))
                    {
                        Board.GetComponent<BoardScript>().AssassinsSAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                    }

                }
                
                for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarSAttack.Count; i++)
                {
                     if(Board.GetComponent<BoardScript>(). TemplarSAttack[i] != null && !Board.GetComponent<BoardScript>().TemplarSAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarSAttack[i]))
                    {
                        Board.GetComponent<BoardScript>().TemplarSAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                    }
                }
            }
            
            GameManager.GetComponent<GameManajer>().AssassinPlay = false;
            GameManager.GetComponent<GameManajer>().TemplarsPlay = true;
        }
        
        if(GameManajer.IsInHand(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject) &&  GameManager.GetComponent<GameManajer>().TemplarsPlay && !gameObject.GetComponent<WeatherCardScript>().IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
        {
            gameObject.GetComponent<WeatherCardScript>().IsPlayed = true;
            GameManager.GetComponent<GameManajer>().maskPositionHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = false;
            if(gameObject.GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.M)
            {
                gameObject.transform.position = Board.GetComponent<BoardScript>().TemplarsWeatherM.transform.position;
                Board.GetComponent<BoardScript>().MaskTemplarsWeatherM = true;
                Board.GetComponent<BoardScript>().WeatherTemplars.Add(gameObject); 
                GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = null;
                
                for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsMAttack.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>(). AssassinsMAttack[i] != null && !Board.GetComponent<BoardScript>().AssassinsMAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsMAttack[i]))
                    {
                        Board.GetComponent<BoardScript>().AssassinsMAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                    }
                }
                
                for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarsMAttack.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>(). TemplarsMAttack[i] != null && !Board.GetComponent<BoardScript>().TemplarsMAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsMAttack[i]))
                    {
                        Board.GetComponent<BoardScript>().TemplarsMAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                    }
                }
            }

            if(gameObject.GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.R)
            {
                gameObject.transform.position = Board.GetComponent<BoardScript>().TemplarsWeatherR.transform.position;
                Board.GetComponent<BoardScript>().MaskTemplarsWeatherR = true;
                Board.GetComponent<BoardScript>().WeatherTemplars.Add(gameObject);
                GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = null;

                for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsRAttack.Count; i++)
                {   

                    if(Board.GetComponent<BoardScript>(). AssassinsRAttack[i] != null && !Board.GetComponent<BoardScript>().AssassinsRAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsRAttack[i]))
                    {
                        Board.GetComponent<BoardScript>().AssassinsRAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                    }

                }

                for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarsRAttack.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>(). TemplarsRAttack[i] != null && !Board.GetComponent<BoardScript>().TemplarsRAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsRAttack[i]))
                    {   
                        Board.GetComponent<BoardScript>().TemplarsRAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                    }
                }    
            }
            
             if(gameObject.GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.S)
            {
               
                gameObject.transform.position = Board.GetComponent<BoardScript>().TemplarsWeatherS.transform.position;
                Board.GetComponent<BoardScript>().MaskTemplarsWeatherS = true;
                Board.GetComponent<BoardScript>().WeatherTemplars.Add(gameObject);
                GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = null;

                for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsSAttack.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>(). AssassinsSAttack[i] != null && !Board.GetComponent<BoardScript>().AssassinsSAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsSAttack[i]))
                    {
                        Board.GetComponent<BoardScript>().AssassinsSAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                    }
                }
                for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarSAttack.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>(). TemplarSAttack[i] != null && !Board.GetComponent<BoardScript>().TemplarSAttack[i].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarSAttack[i]))
                    {
                        Board.GetComponent<BoardScript>().TemplarSAttack[i].GetComponent<UnityCardScript>().AfectedByWeather = true;
                        GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                    }
                }

            }
                GameManager.GetComponent<GameManajer>().AssassinPlay = true;
                GameManager.GetComponent<GameManajer>().TemplarsPlay = false;
        }
    }
#endregion

#region CleanEffect

    public static void CleanEffect(GameObject gameObject)
    {
        for (int i = 0; i < Board.GetComponent<BoardScript>().WeatherTemplars.Count; i++)
            {
                if(Board.GetComponent<BoardScript>().WeatherTemplars[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.M && gameObject.GetComponent<CleanCardScript>().TypeClean == UnityCard.EnumTypeClimAndClean.M)
                {
                    Board.GetComponent<BoardScript>().WeatherTemplars[i].transform.position += new UnityEngine.Vector3(1000,1000,1000);
                    Board.GetComponent<BoardScript>().WeatherTemplars.RemoveAt(i);
                    Board.GetComponent<BoardScript>().MaskTemplarsWeatherM = false;
                    
                    for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsMAttack.Count; j++)
                    {
                        if(Board.GetComponent<BoardScript>().AssassinsMAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsMAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().AssassinsMAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                        {
                            GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                        }
                    }

                    for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarsMAttack.Count; j++)
                    {
                       if(Board.GetComponent<BoardScript>().TemplarsMAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarsMAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().TemplarsMAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                        {
                            GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                        }
                    }
                    break;

                }
                if(Board.GetComponent<BoardScript>().WeatherTemplars[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.R && gameObject.GetComponent<CleanCardScript>().TypeClean == UnityCard.EnumTypeClimAndClean.R)
                {
                    Board.GetComponent<BoardScript>().WeatherTemplars[i].transform.position += new UnityEngine.Vector3(1000,1000,1000);
                    Board.GetComponent<BoardScript>().WeatherTemplars.RemoveAt(i);
                    Board.GetComponent<BoardScript>().MaskTemplarsWeatherR = false;

                     for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsRAttack.Count; j++)
                    {
                        if(Board.GetComponent<BoardScript>().AssassinsRAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsRAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().AssassinsRAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                        {
                            GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                        }
                    }

                    for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarsRAttack.Count; j++)
                    {
                       if(Board.GetComponent<BoardScript>().TemplarsRAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarsRAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().TemplarsRAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                        {
                            GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                        }
                    }

                    break;
                }
                if(Board.GetComponent<BoardScript>().WeatherTemplars[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.S && gameObject.GetComponent<CleanCardScript>().TypeClean == UnityCard.EnumTypeClimAndClean.S)
                {
                    Board.GetComponent<BoardScript>().WeatherTemplars[i].transform.position += new UnityEngine.Vector3(1000,1000,1000);
                    Board.GetComponent<BoardScript>().WeatherTemplars.RemoveAt(i);
                    Board.GetComponent<BoardScript>().MaskTemplarsWeatherS = false;

                    for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsMAttack.Count; j++)
                    {
                        if(Board.GetComponent<BoardScript>().AssassinsSAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsSAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().AssassinsSAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                        {
                            GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                        }
                    }

                    for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarSAttack.Count; j++)
                    {
                       if(Board.GetComponent<BoardScript>().TemplarSAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarSAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().TemplarSAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                        {
                            GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                        }
                    }
                    break;
                }
            }
            GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = null;
            Destroy(gameObject);
            GameManager.GetComponent<GameManajer>().AssassinPlay = false;
            GameManager.GetComponent<GameManajer>().TemplarsPlay = true;
        
            if(GameManajer.IsInHand(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject) &&  GameManager.GetComponent<GameManajer>().TemplarsPlay && !gameObject.GetComponent<CleanCardScript>().IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
             {
                gameObject.GetComponent<CleanCardScript>().IsPlayed = true;
                GameManager.GetComponent<GameManajer>().maskPositionHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = false;
                if(Board.GetComponent<BoardScript>().WeatherAssassins.Count > 0)
                {
                for (int i = 0; i < Board.GetComponent<BoardScript>().WeatherAssassins.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>().WeatherAssassins[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.M && gameObject.GetComponent<CleanCardScript>().TypeClean == UnityCard.EnumTypeClimAndClean.M)
                    {
                        Board.GetComponent<BoardScript>().WeatherAssassins[i].transform.position += new UnityEngine.Vector3(1000,1000,1000);
                        Board.GetComponent<BoardScript>().WeatherAssassins.RemoveAt(i);
                        Board.GetComponent<BoardScript>().MaskAssassinsWeatherM = false;

                         for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsMAttack.Count; j++)
                        {
                            if(Board.GetComponent<BoardScript>().AssassinsMAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsMAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().AssassinsMAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                            {
                                GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                            }
                        }

                        for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarsMAttack.Count; j++)
                        {
                            if(Board.GetComponent<BoardScript>().TemplarsMAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarsMAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().TemplarsMAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                            {
                                GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                            }
                        }
                        break;

                    }
                    if(Board.GetComponent<BoardScript>().WeatherAssassins[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.R && gameObject.GetComponent<CleanCardScript>().TypeClean == UnityCard.EnumTypeClimAndClean.R)
                    {
                         Board.GetComponent<BoardScript>().WeatherTemplars[i].transform.position += new UnityEngine.Vector3(1000,1000,1000);
                        Board.GetComponent<BoardScript>().WeatherAssassins.RemoveAt(i);
                        Board.GetComponent<BoardScript>().MaskAssassinsWeatherM = false;

                         for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsRAttack.Count; j++)
                        {
                            if(Board.GetComponent<BoardScript>().AssassinsRAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsRAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().AssassinsRAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                            {
                                GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                            }
                        }

                        for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarsRAttack.Count; j++)
                        {
                        if(Board.GetComponent<BoardScript>().TemplarsRAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarsRAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().TemplarsRAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                            {
                                GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                            }
                        }
                            break;
                    }
                    if(Board.GetComponent<BoardScript>().WeatherAssassins[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.S && gameObject.GetComponent<CleanCardScript>().TypeClean == UnityCard.EnumTypeClimAndClean.S)
                    {
                        Board.GetComponent<BoardScript>().WeatherTemplars[i].transform.position += new UnityEngine.Vector3(1000,1000,1000);
                        Board.GetComponent<BoardScript>().WeatherAssassins.RemoveAt(i);
                        Board.GetComponent<BoardScript>().MaskAssassinsWeatherS = false;

                        for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsSAttack.Count; j++)
                        {
                            if(Board.GetComponent<BoardScript>().AssassinsSAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsSAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().AssassinsSAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                            {
                                GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                            }
                        }

                        for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarSAttack.Count; j++)
                        {
                        if(Board.GetComponent<BoardScript>().TemplarSAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarSAttack[j].CompareTag("Lure Card") && Board.GetComponent<BoardScript>().TemplarSAttack[j].GetComponent<UnityCardScript>().AfectedByWeather)
                            {
                                GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                            }
                        }
                            break;
                    }
                }
            }
              
                GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = null;
                Destroy(gameObject);
                GameManager.GetComponent<GameManajer>().AssassinPlay = true;
                GameManager.GetComponent<GameManajer>().TemplarsPlay = false;
             
        }
    }       
#endregion

#region AumentEffect

    public static void AumentEffect(GameObject gameObject)
    {
        if(gameObject.GetComponent<AumentCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins && GameManager.GetComponent<GameManajer>().AssassinPlay && !gameObject.GetComponent<AumentCardScript>().IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
      {
          gameObject.GetComponent<AumentCardScript>().IsPlayed = true;
          GameManager.GetComponent<GameManajer>().maskPositionHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = false;
          if(gameObject.GetComponent<AumentCardScript>().RowAument == UnityCard.EnumTypeAttackCard.M )
          {
              gameObject.transform.position = Board.GetComponent<BoardScript>().AssassinsAumentM.transform.position;
              Board.GetComponent<BoardScript>().MaskAssassinsAumentM = true;
              Board.GetComponent<BoardScript>().AumentAssassins.Add(gameObject);
              GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = null;
              
              for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsMAttack.Count; i++)
              {
                  if(Board.GetComponent<BoardScript>().AssassinsMAttack[i] != null)
                  { 
                    if(!IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsMAttack[i]))
                    {
                        GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                    }
                  }
              }
          }

          if(gameObject.GetComponent<AumentCardScript>().RowAument == UnityCard.EnumTypeAttackCard.R)
          {
              gameObject.transform.position = Board.GetComponent<BoardScript>().AssassinsAumentR.transform.position;
              Board.GetComponent<BoardScript>().MaskAssassinsAumentR = true;
              Board.GetComponent<BoardScript>().AumentAssassins.Add(gameObject);
              GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = null;
                     
              for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsRAttack.Count; i++)
              {
                  if(Board.GetComponent<BoardScript>().AssassinsRAttack[i] != null)
                  {
                      if(!IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsRAttack[i]))
                      {
                          GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                      }
                  }
              }
          } 
                
          if(gameObject.GetComponent<AumentCardScript>().RowAument == UnityCard.EnumTypeAttackCard.S)
          {
              gameObject.transform.position = Board.GetComponent<BoardScript>().AssassinsAumentS.transform.position;
              Board.GetComponent<BoardScript>().MaskAssassinsAumentS = true;
              Board.GetComponent<BoardScript>().AumentAssassins.Add(gameObject);
              GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = null;
              
              for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsSAttack.Count; i++)
              {
                  if(Board.GetComponent<BoardScript>().AssassinsSAttack[i] != null)
                  {
                    if(!IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsSAttack[i]))
                    {
                        GameManager.GetComponent<GameManajer>().AssassinPoints += 2;
                    }
                  }
              }   
          }

          GameManager.GetComponent<GameManajer>().TemplarsPlay = true;
          GameManager.GetComponent<GameManajer>().AssassinPlay = false;
      }

      if(gameObject.GetComponent<AumentCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar && GameManager.GetComponent<GameManajer>().TemplarsPlay && !gameObject.GetComponent<AumentCardScript>().IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
      {
          gameObject.GetComponent<AumentCardScript>().IsPlayed = true;
          GameManager.GetComponent<GameManajer>().maskPositionHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = false;
          if(gameObject.GetComponent<AumentCardScript>().RowAument == UnityCard.EnumTypeAttackCard.M)
          {
              gameObject.transform.position = Board.GetComponent<BoardScript>().TemplarsAumentM.transform.position;
              Board.GetComponent<BoardScript>().MaskTemplarsAumentM = true;
              Board.GetComponent<BoardScript>().AumentTemplars.Add(gameObject);
              GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = null;
                
              for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarsMAttack.Count; i++)
              {
                  if(Board.GetComponent<BoardScript>(). TemplarsMAttack[i] != null)
                  {
                      if(!IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsMAttack[i]))
                      {
                        GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                      }
                  }
              }
          }
          
          if(gameObject.GetComponent<AumentCardScript>().RowAument == UnityCard.EnumTypeAttackCard.R)
          {
              gameObject.transform.position = Board.GetComponent<BoardScript>().TemplarsAumentR.transform.position;
              Board.GetComponent<BoardScript>().MaskTemplarsAumentR = true;
              Board.GetComponent<BoardScript>().AumentTemplars.Add(gameObject);
              GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = null;
              
              for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarsRAttack.Count; i++)
              {
                  if(Board.GetComponent<BoardScript>(). TemplarsRAttack[i] != null)
                  {
                      if(!IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsRAttack[i]))
                      {
                        GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                      }
                  }
              }
           }
            
            if(gameObject.GetComponent<AumentCardScript>().RowAument == UnityCard.EnumTypeAttackCard.S)
            {
                gameObject.transform.position = Board.GetComponent<BoardScript>().TemplarsAumentS.transform.position;
                Board.GetComponent<BoardScript>().MaskTemplarsAumentS = true;
                Board.GetComponent<BoardScript>().AumentTemplars.Add(gameObject);
                GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = null;

                
                for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarSAttack.Count; i++)
                {
                    if(Board.GetComponent<BoardScript>(). TemplarSAttack[i] != null)
                    {
                        if(!IsGoldUnity(Board.GetComponent<BoardScript>().TemplarSAttack[i]))
                        {
                            GameManager.GetComponent<GameManajer>().TemplarPoints += 2;
                        }
                    }
                }
            }
            GameManager.GetComponent<GameManajer>().TemplarsPlay = false;
          GameManager.GetComponent<GameManajer>().AssassinPlay = true;
        }
    }


    public static bool IsGoldUnity(GameObject gameObject)
    {
      if(gameObject.GetComponent<UnityCardScript>().TypeUnity == UnityCard.EnumTypeUnity.Gold)
      return true;

      return false;
    }
#endregion
}



