using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class UnityCardScript : MonoBehaviour
{
    public string NameCard;
    public GameObject Board;
    public UnityEngine.Vector3 positionOfCard; //Se utiliza para el cambio del senuelo
    public GameObject ZoomAssasin;
    public GameObject ZoomTemplar; //Game Object del Zoom
    public bool IsPlayed; // Se utiliza para que a la vez que se juege la carta no puedaa volver a ser jugada 
    public bool EfectActivated; //Se utiliza para saber si el efecto de la carta ha sido activado
    public bool AlreadySum; 
    public bool AfectedByWeather; 
    public GameObject GameManager;
    //Deberia probar el removeat en vez del remove a ver si desaparecen del todo los errores del serializable 
    
    public UnityCard.EnumTypeCard TypeCard;
    public UnityCard.EnumTypeAttackCard TypeAttackCard;
    public UnityCard.EnumTypeUnity TypeUnity;
    public int PointAttackCard;
    public UnityCard.EnumEfects EfectCard;
    public UnityCard.EnumFactionCard FactionCard;
    public Sprite CardsFront;
    // Los dos siguientes campos son para simular el giro de la cartas 
    public Sprite TemplarsCardsBack; 
    public Sprite AssassinsCardsBack;
    

    [SerializeField] UnityCard unityCard;

    void Start()
    {
      NameCard        = unityCard.NameCard;
      TypeCard        = unityCard.TypeCard;
      TypeAttackCard  = unityCard.TypeAttackCard;
      TypeUnity       = unityCard.TypeUnity;
      PointAttackCard = unityCard.PointAttackCard;
      EfectCard       = unityCard.EfectCard;
      FactionCard     = unityCard.FactionCard;

      Board       = GameObject.FindGameObjectWithTag("board");
      ZoomAssasin = GameObject.FindGameObjectWithTag("Zoom Assassin");
      ZoomTemplar = GameObject.FindGameObjectWithTag("Zoom Templar");
      GameManager = GameObject.FindGameObjectWithTag("Game Manager");
    }

    void Update()
    {
      // Las siguientes lineas simulan la rotacion de la carta entre turnos
        if(GameManager.GetComponent<GameManajer>().AssassinPlay)
        {
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Length; i++)
            {
              if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null)
              {
                if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Unity Card"))
                {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<UnityCardScript>().CardsFront;
                }
              }
            }
            for (int j = 0; j < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Length; j++)
            {
              if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[j] != null)
              {
                if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[j].CompareTag("Unity Card"))
                {
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[j].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandTemplar[j].GetComponent<UnityCardScript>().TemplarsCardsBack;
                }
              }
            }
        }

        if(GameManager.GetComponent<GameManajer>().TemplarsPlay)
        {
          for (int j = 0; j < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Length; j++)
            {
               if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[j] != null)
              {
                if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[j].CompareTag("Unity Card"))
                {
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[j].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandTemplar[j].GetComponent<UnityCardScript>().CardsFront;
                }
              }
            }
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Length; i++)
            {
               if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null)
              {
                if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Unity Card"))
                {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<UnityCardScript>().AssassinsCardsBack;
                }
              }
            }
        }
    }
    private void OnMouseDown() 
  {
    //El bloque de codigo siguiente se utiliza para que si se producen dos pases consecutivos por parte de ambos jugadores se finalice la ronda 
    if(GameManager.GetComponent<GameManajer>().CounterPassRound > 0)
    {
      GameManager.GetComponent<GameManajer>().CounterPassRound -=1;
    }
    //EL siguiente bloque es para efectuar el cambio del senuelo
    if(GameManager.GetComponent<GameManajer>().LureIsReady && IsPlayed)
    {
      if(gameObject.GetComponent<UnityCardScript>().FactionCard == GameManager.GetComponent<GameManajer>().LureForChange.GetComponent<LureCardScript>().FactionCard)
      {
        GameManager.GetComponent<GameManajer>().LureForChange.GetComponent<LureCardScript>().IsPlayed = true;  
        positionOfCard = gameObject.transform.position;
        gameObject.transform.position = GameManager.GetComponent<GameManajer>().LureForChange.transform.position;
         
        GameManager.GetComponent<GameManajer>().LureForChange.transform.position = positionOfCard;
        GameManager.GetComponent<GameManajer>().LureIsReady = false;
        gameObject.GetComponent<UnityCardScript>().IsPlayed = false;
        
         

        if(gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins)
        {
          GameManajer.AddCardToHand(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject); 
          GameManager.GetComponent<GameManajer>().AssassinPoints -= gameObject.GetComponent<UnityCardScript>().PointAttackCard;
          if(Board.GetComponent<BoardScript>().AssassinsMAttack.Contains(gameObject))
          {
            Board.GetComponent<BoardScript>().AssassinsMAttack.Remove(gameObject);
            Board.GetComponent<BoardScript>().AssassinsMAttack.Add(GameManager.GetComponent<GameManajer>().LureForChange);
          }
          if(Board.GetComponent<BoardScript>().AssassinsRAttack.Contains(gameObject))
          {
            Board.GetComponent<BoardScript>().AssassinsRAttack.Remove(gameObject);
            Board.GetComponent<BoardScript>().AssassinsRAttack.Add(GameManager.GetComponent<GameManajer>().LureForChange);
          }
          if(Board.GetComponent<BoardScript>().AssassinsSAttack.Contains(gameObject))
          {
            Board.GetComponent<BoardScript>().AssassinsSAttack.Remove(gameObject);
            Board.GetComponent<BoardScript>().AssassinsSAttack.Add(GameManager.GetComponent<GameManajer>().LureForChange);
          }

          GameManager.GetComponent<GameManajer>().AssassinPlay = false;
          GameManager.GetComponent<GameManajer>().TemplarsPlay = true;
        }

         if(gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar)
        {
          GameManajer.AddCardToHand(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject);//Si se rompe puede ser porque no este en la misma posiscion de la lista internal que de el array visual 
          GameManager.GetComponent<GameManajer>().TemplarPoints -= gameObject.GetComponent<UnityCardScript>().PointAttackCard;
          if(Board.GetComponent<BoardScript>().TemplarsMAttack.Contains(gameObject))
          {
            Board.GetComponent<BoardScript>().TemplarsMAttack.Remove(gameObject);
            Board.GetComponent<BoardScript>().TemplarsMAttack.Add(GameManager.GetComponent<GameManajer>().LureForChange);
          }
          if(Board.GetComponent<BoardScript>().TemplarsRAttack.Contains(gameObject))
          {
            Board.GetComponent<BoardScript>().TemplarsRAttack.Remove(gameObject);
            Board.GetComponent<BoardScript>().TemplarsRAttack.Add(GameManager.GetComponent<GameManajer>().LureForChange);
          }
          if(Board.GetComponent<BoardScript>().TemplarSAttack.Contains(gameObject))
          {
            Board.GetComponent<BoardScript>().TemplarSAttack.Remove(gameObject);
            Board.GetComponent<BoardScript>().TemplarSAttack.Add(GameManager.GetComponent<GameManajer>().LureForChange);
          }
          GameManager.GetComponent<GameManajer>().AssassinPlay = true;
          GameManager.GetComponent<GameManajer>().TemplarsPlay = false;
        }
        GameManager.GetComponent<GameManajer>().LureForChange = null;
      }
     
      
    }// El bloque de codigo siguiente es el encargado de invocar la cartas en el campo despues de realizado el cambio de las dos carta 
   if (gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins && GameManager.GetComponent<GameManajer>().AssassinPlay && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin && !IsPlayed)
   {
    GameManager.GetComponent<GameManajer>().maskPositionHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = false;
    IsPlayed = true;
    if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.M)
    {
      for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinMattackPos.Length; i++)
      {
        if(!Board.GetComponent<BoardScript>().maskAssassinMattack[i])
        {
          gameObject.transform.position = Board.GetComponent<BoardScript>().AssassinMattackPos[i].transform.position;
          Board.GetComponent<BoardScript>().maskAssassinMattack [i] = true;
          
          Board.GetComponent<BoardScript>().AssassinsMAttack.Add(gameObject);
          GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = null;
          break; 
        }
      }
    }

    if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.R)
    {
      for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinRattackPos.Length; i++)
      {
        if(!Board.GetComponent<BoardScript>().maskAssassinRattack[i])
        {
          gameObject.transform.position = Board.GetComponent<BoardScript>().AssassinRattackPos[i].transform.position;
          Board.GetComponent<BoardScript>().maskAssassinRattack [i] = true; 
          Board.GetComponent<BoardScript>().AssassinsRAttack.Add(gameObject);
          GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = null;
          break; 
        }
      }
    } 

    if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.S)
    {
      for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinSattackPos.Length; i++)
      {
        if(!Board.GetComponent<BoardScript>().maskAssassinSattack[i])
        {
          gameObject.transform.position = Board.GetComponent<BoardScript>().AssassinSattackPos[i].transform.position;
          Board.GetComponent<BoardScript>().maskAssassinSattack [i] = true;
          Board.GetComponent<BoardScript>().AssassinsSAttack.Add(gameObject);
          GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = null;
          break; 
        }
      }
    }

      GameManager.GetComponent<GameManajer>().AssassinPlay = false;
      GameManager.GetComponent<GameManajer>().TemplarsPlay = true;
  }

  if(gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar && GameManager.GetComponent<GameManajer>().TemplarsPlay && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar && !IsPlayed)
  {
      GameManager.GetComponent<GameManajer>().maskPositionHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = false;
      IsPlayed = true;
     if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.M)
    {
      for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarMattackpos.Length; i++)
      {
        if(!Board.GetComponent<BoardScript>().maskTemplarMattack[i])
        {
          gameObject.transform.position = Board.GetComponent<BoardScript>().TemplarMattackpos[i].transform.position;
          Board.GetComponent<BoardScript>().maskTemplarMattack [i] = true; 
          Board.GetComponent<BoardScript>().TemplarsMAttack.Add(gameObject);
          GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = null;
          break; 
        }
      }
    }

    if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.R)
    {
      for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarRattackpos.Length; i++)
      {
        if(!Board.GetComponent<BoardScript>().maskTemplarRattack[i])
        {
          gameObject.transform.position = Board.GetComponent<BoardScript>().TemplarRattackpos[i].transform.position;
          Board.GetComponent<BoardScript>().maskTemplarRattack [i] = true;
          Board.GetComponent<BoardScript>().TemplarsRAttack.Add(gameObject);
          GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = null; 
          break; 
        }
      }
    }

    if(gameObject.GetComponent<UnityCardScript>().TypeAttackCard == UnityCard.EnumTypeAttackCard.S)
    {
      for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarSattackpos.Length; i++)
      {
        if(!Board.GetComponent<BoardScript>().maskTemplarSattack[i])
        {
          gameObject.transform.position = Board.GetComponent<BoardScript>().TemplarSattackpos[i].transform.position;
          Board.GetComponent<BoardScript>().maskTemplarSattack [i] = true;
          Board.GetComponent<BoardScript>().TemplarSAttack.Add(gameObject);
          GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = null;
          break; 
        }
      }
    }

      GameManager.GetComponent<GameManajer>().AssassinPlay = true;
      GameManager.GetComponent<GameManajer>().TemplarsPlay = false;
  }
  //Efectos de las especiales 

    switch (gameObject.GetComponent<UnityCardScript>().EfectCard)
    {
      
      case UnityCard.EnumEfects.Alexios_ShayCormacEffect:
        PortalAlexios_ShayCormacEffect(gameObject.GetComponent<UnityCardScript>().FactionCard);
        gameObject.GetComponent<UnityCardScript>().EfectActivated = true;
      break;

      case UnityCard.EnumEfects.Arno_GermainEfect:
        PutAnAument();
        gameObject.GetComponent<UnityCardScript>().EfectActivated = true;
        break;

      case UnityCard.EnumEfects.Edward_DeSableEffect:
        Edward_LaureanoEffect(gameObject.GetComponent<UnityCardScript>().FactionCard);
         gameObject.GetComponent<UnityCardScript>().EfectActivated = true;
      break;

    }

    //ESTO ES PARA CAMBIAR LAS CARTAS//************** 
    if(!GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin && GameManager.GetComponent<GameManajer>().AssassinPlay && gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins)
    {
        int index = UnityEngine.Random.Range(0,GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().deck.Count);
        GameManager.GetComponent<GameManajer>().MaxChangeAssassin++;
        GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().Add(gameObject);
        GameObject drawCardAssassin = Instantiate(GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().deck[index],gameObject.transform.position,UnityEngine.Quaternion.identity);
        drawCardAssassin.transform.SetParent(GameManager.GetComponent<GameManajer>().HandPlayerAssassin.transform, false);
        drawCardAssassin.transform.localScale = gameObject.transform.localScale;
        drawCardAssassin.transform.position = gameObject.transform.position;
        GameManager.GetComponent<GameManajer>().CardsHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = drawCardAssassin;
        gameObject.transform.position += new UnityEngine.Vector3(1000,1000,1000);
        GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().RemoveAt(index);
        if(GameManager.GetComponent<GameManajer>().MaxChangeAssassin == 2) GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin = true;
    }
     if(!GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar && GameManager.GetComponent<GameManajer>().TemplarsPlay && gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar)
        {
            int index = UnityEngine.Random.Range(0,GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().deck.Count);
            GameManager.GetComponent<GameManajer>().MaxChangeTemplar++;
            GameManager.GetComponent<GameManajer>().Templarsdeck.GetComponent<DeckScript>().Add(gameObject);
            GameObject drawCardTemplar = Instantiate(GameManager.GetComponent<GameManajer>().Templarsdeck.GetComponent<DeckScript>().deck[index],gameObject.transform.position,GameManager.GetComponent<GameManajer>().Templarsdeck.transform.rotation);
            drawCardTemplar.transform.SetParent(GameManager.GetComponent<GameManajer>().HandPlayerTemplar.transform, false);
            drawCardTemplar.transform.localScale = gameObject.transform.localScale;
            drawCardTemplar.transform.position = gameObject.transform.position;
            GameManager.GetComponent<GameManajer>().CardsHandTemplar[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject)] = drawCardTemplar;
            gameObject.transform.position += new UnityEngine.Vector3(1000,1000,1000);
            GameManager.GetComponent<GameManajer>().Templarsdeck.GetComponent<DeckScript>().RemoveAt(index);
            if(GameManager.GetComponent<GameManajer>().MaxChangeTemplar == 2) GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar = true;
        }

}
      //Este metodo es para el zoom de las cartas 
       void OnMouseEnter() 
      {
          if(GameManager.GetComponent<GameManajer>().AssassinPlay)
          ZoomAssasin.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite; 
          else
          ZoomTemplar.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
      }

      //Elimina la carta con mayor poder del campo rival 
      public void PortalAlexios_ShayCormacEffect(UnityCard.EnumFactionCard factionCard)
      {
        if(factionCard == UnityCard.EnumFactionCard.Assassins && !EfectActivated && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
          Alexios_ShayCormacEffect(Board.GetComponent<BoardScript>().TemplarsMAttack,Board.GetComponent<BoardScript>().TemplarsRAttack,Board.GetComponent<BoardScript>().TemplarSAttack);
        if(factionCard == UnityCard.EnumFactionCard.Templar && !EfectActivated && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
          Alexios_ShayCormacEffect(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack);
      }
      private void Alexios_ShayCormacEffect(List<GameObject>Mattack,List<GameObject> Rattack,List<GameObject> Sattack)
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
              EfectActivated = true;
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
              EfectActivated = true;
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
              EfectActivated = true;
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
      //Elimina la carta con menor poder del campo rival
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
      //Efecto de robar una carta 
      private void Edward_LaureanoEffect(UnityCard.EnumFactionCard enumFactionCard)
      {
        if (!EfectActivated)
        {
          EfectActivated = true;
          int RandomIndexCard = UnityEngine.Random.Range(0,GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().deck.Count-1);
          if(enumFactionCard == UnityCard.EnumFactionCard.Assassins && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
          {

              GameObject drawCardAssassin = Instantiate(GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().deck[RandomIndexCard],GameManager.GetComponent<GameManajer>().PositionHandAssassin[GameManajer.PositionInNull(GameManager.GetComponent<GameManajer>().CardsHandAssassin)].transform.position,UnityEngine.Quaternion.identity);
              drawCardAssassin.transform.SetParent(GameManager.GetComponent<GameManajer>().HandPlayerAssassin.transform, false);
              drawCardAssassin.transform.position = GameManager.GetComponent<GameManajer>().PositionHandAssassin[GameManajer.PositionInNull(GameManager.GetComponent<GameManajer>().CardsHandAssassin)].transform.position;
              drawCardAssassin.transform.localScale = gameObject.transform.localScale;
              GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().RemoveAt(RandomIndexCard);
              GameManajer.AddCardToHand(GameManager.GetComponent<GameManajer>().CardsHandAssassin,drawCardAssassin);
          }
            
          if (enumFactionCard == UnityCard.EnumFactionCard.Templar && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
          {
                  GameObject drawCardTemplar = Instantiate(GameManager.GetComponent<GameManajer>().Templarsdeck.GetComponent<DeckScript>().deck[RandomIndexCard],GameManager.GetComponent<GameManajer>().PositionHandTemplar[GameManajer.PositionInNull(GameManager.GetComponent<GameManajer>().CardsHandTemplar)].transform.position,GameManager.GetComponent<GameManajer>().Templarsdeck.transform.rotation);
                  drawCardTemplar.transform.SetParent(GameManager.GetComponent<GameManajer>().HandPlayerTemplar.transform, false);
                  drawCardTemplar.transform.position = GameManager.GetComponent<GameManajer>().PositionHandTemplar[GameManajer.PositionInNull(GameManager.GetComponent<GameManajer>().CardsHandTemplar)].transform.position;
                  drawCardTemplar.transform.localScale = gameObject.transform.localScale; //cuidado con esto 
                  GameManager.GetComponent<GameManajer>().Assassinsdeck.GetComponent<DeckScript>().RemoveAt(RandomIndexCard);
                  GameManajer.AddCardToHand(GameManager.GetComponent<GameManajer>().CardsHandTemplar,drawCardTemplar);
            }
        }
      }
      // Elimina la fila con menos unidades del campo rival
      private void PortalArno_GermainEffect(UnityCard.EnumFactionCard factionCard)
      {
        if(factionCard == UnityCard.EnumFactionCard.Assassins && !EfectActivated && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
          Arno_GermainEffect(Board.GetComponent<BoardScript>().TemplarsMAttack,Board.GetComponent<BoardScript>().TemplarsRAttack,Board.GetComponent<BoardScript>().TemplarSAttack);
        
         if(factionCard == UnityCard.EnumFactionCard.Templar && !EfectActivated && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
          Arno_GermainEffect(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack);
      }

      public void Arno_GermainEffect(List<GameObject> Mattack, List<GameObject> Rattack, List<GameObject> Sattack)
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
      //Efecto para agregar un clima
      public void AddWeather(UnityCard.EnumFactionCard factionCard)
      {
        if(factionCard == UnityCard.EnumFactionCard.Assassins)
        {
          for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Length; i++)
          {
              if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null && GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Weather Card "))
              {
                 GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<WeatherCardScript>().IsPlayed = true;
                 GameManager.GetComponent<GameManajer>().maskPositionHandAssassin[i] = false;
                 if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.M)
                 {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].transform.position = Board.GetComponent<BoardScript>().AssassinsWeatherM.transform.position;
                    Board.GetComponent<BoardScript>().MaskAssassinsWeatherM = true;
                    Board.GetComponent<BoardScript>().WeatherAssassins.Add(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i]);
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] = null;
                    for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsMAttack.Count; j++)
                    {
                            if(Board.GetComponent<BoardScript>(). AssassinsMAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsMAttack[j].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsMAttack[j])) 
                            {
                                Board.GetComponent<BoardScript>().AssassinsMAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                                GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                            }

                    }
                    for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarsMAttack.Count; j++)
                    {
                        if(Board.GetComponent<BoardScript>(). TemplarsMAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarsMAttack[j].CompareTag("Lure Card") &&!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsMAttack[j]))
                        {
                            Board.GetComponent<BoardScript>().TemplarsMAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                            GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                        }
                    }
                  }

                 else if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.R)
                 {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].transform.position = Board.GetComponent<BoardScript>().AssassinsWeatherR.transform.position;
                    Board.GetComponent<BoardScript>().MaskAssassinsWeatherR = true;
                    Board.GetComponent<BoardScript>().WeatherAssassins.Add(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i]);
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] = null;
                    for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsRAttack.Count; j++)
                    {
                        if(Board.GetComponent<BoardScript>(). AssassinsRAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsRAttack[j].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsRAttack[j])) 
                        {
                            Board.GetComponent<BoardScript>().AssassinsRAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                            GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                        }

                    }
                    for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarsRAttack.Count; j++)
                    {
                        if(Board.GetComponent<BoardScript>(). TemplarsRAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarsRAttack[j].CompareTag("Lure Card") &&!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsRAttack[j]))
                        {
                            Board.GetComponent<BoardScript>().TemplarsRAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                            GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                        }
                    }
                  }

                   else if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.S)
                  {
                      GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].transform.position = Board.GetComponent<BoardScript>().AssassinsWeatherS.transform.position;
                      Board.GetComponent<BoardScript>().MaskAssassinsWeatherS = true;
                      Board.GetComponent<BoardScript>().WeatherAssassins.Add(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i]);
                      GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] = null;
                      for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsSAttack.Count; j++)
                      {
                          if(Board.GetComponent<BoardScript>(). AssassinsSAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsSAttack[j].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsSAttack[j])) 
                          {
                              Board.GetComponent<BoardScript>().AssassinsSAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                              GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                          }

                      }
                      for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarSAttack.Count; j++)
                      {
                          if(Board.GetComponent<BoardScript>(). TemplarSAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarSAttack[j].CompareTag("Lure Card") &&!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarSAttack[j]))
                          {
                              Board.GetComponent<BoardScript>().TemplarSAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                              GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                          }
                      }
                  }
              break;
            }
          }

          if(factionCard == UnityCard.EnumFactionCard.Templar)
          {
          for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Length; i++)
          {
             if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] != null && GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].CompareTag("Weather Card "))
              {
                 GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<WeatherCardScript>().IsPlayed = true;
                 GameManager.GetComponent<GameManajer>().maskPositionHandTemplar[i] = false;
                 if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.M)
                 {
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].transform.position = Board.GetComponent<BoardScript>().TemplarsWeatherM.transform.position;
                    Board.GetComponent<BoardScript>().MaskTemplarsWeatherM = true;
                    Board.GetComponent<BoardScript>().WeatherTemplars.Add(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i]);
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] = null;
                    for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsMAttack.Count; j++)
                    {
                            if(Board.GetComponent<BoardScript>(). AssassinsMAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsMAttack[j].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsMAttack[j])) 
                            {
                                Board.GetComponent<BoardScript>().AssassinsMAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                                GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                            }

                    }
                    for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarsMAttack.Count; j++)
                    {
                        if(Board.GetComponent<BoardScript>(). TemplarsMAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarsMAttack[j].CompareTag("Lure Card") &&!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsMAttack[j]))
                        {
                            Board.GetComponent<BoardScript>().TemplarsMAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                            GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                        }
                    }
                  }

                 else if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.R)
                 {
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].transform.position = Board.GetComponent<BoardScript>().TemplarsWeatherR.transform.position;
                    Board.GetComponent<BoardScript>().MaskTemplarsWeatherR = true;
                    Board.GetComponent<BoardScript>().WeatherTemplars.Add(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i]);
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] = null;
                    for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsRAttack.Count; j++)
                    {
                        if(Board.GetComponent<BoardScript>(). AssassinsRAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsRAttack[j].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsRAttack[j])) 
                        {
                            Board.GetComponent<BoardScript>().AssassinsRAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                            GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                        }

                    }
                    for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarsRAttack.Count; j++)
                    {
                        if(Board.GetComponent<BoardScript>(). TemplarsRAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarsRAttack[j].CompareTag("Lure Card") &&!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarsRAttack[j]))
                        {
                            Board.GetComponent<BoardScript>().TemplarsRAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                            GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                        }
                    }
                  }

                  else if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<WeatherCardScript>().TypeClim == UnityCard.EnumTypeClimAndClean.S)
                  {
                      GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].transform.position = Board.GetComponent<BoardScript>().TemplarsWeatherS.transform.position;
                      Board.GetComponent<BoardScript>().MaskTemplarsWeatherS = true;
                      Board.GetComponent<BoardScript>().WeatherTemplars.Add(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i]);
                      GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] = null;
                      for (int j = 0; j < Board.GetComponent<BoardScript>().AssassinsSAttack.Count; j++)
                      {
                          if(Board.GetComponent<BoardScript>(). AssassinsSAttack[j] != null && !Board.GetComponent<BoardScript>().AssassinsSAttack[j].CompareTag("Lure Card") && !AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().AssassinsSAttack[j])) 
                          {
                              Board.GetComponent<BoardScript>().AssassinsSAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                              GameManager.GetComponent<GameManajer>().AssassinPoints -= 2;
                          }

                      }
                      for (int j = 0; j < Board.GetComponent<BoardScript>().TemplarSAttack.Count; j++)
                      {
                          if(Board.GetComponent<BoardScript>(). TemplarSAttack[j] != null && !Board.GetComponent<BoardScript>().TemplarSAttack[j].CompareTag("Lure Card") &&!AumentCardScript.IsGoldUnity(Board.GetComponent<BoardScript>().TemplarSAttack[j]))
                          {
                              Board.GetComponent<BoardScript>().TemplarSAttack[j].GetComponent<UnityCardScript>().AfectedByWeather = true;
                              GameManager.GetComponent<GameManajer>().TemplarPoints -= 2;
                          }
                      }
                  }
              break;
              }
            }
          }
        }
      }
      //Efecto del promedio
      public void EffectPromedy()
      {
          gameObject.GetComponent<UnityCardScript>().AlreadySum = true;
          int promedy = 0;
          promedy = Promedy(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack,Board.GetComponent<BoardScript>().TemplarsMAttack,Board.GetComponent<BoardScript>().TemplarsRAttack,Board.GetComponent<BoardScript>().TemplarSAttack);
          GameManager.GetComponent<GameManajer>().AssassinPoints = promedy * CounterOfUnitys(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack);
          GameManager.GetComponent<GameManajer>().TemplarPoints = promedy * CounterOfUnitys(Board.GetComponent<BoardScript>().TemplarsMAttack,Board.GetComponent<BoardScript>().TemplarsRAttack,Board.GetComponent<BoardScript>().TemplarSAttack);
      }
      private int Promedy(List<GameObject>MattackAssassins,List<GameObject>MattackTemplars,List<GameObject>RattackAssassins,List<GameObject>RattackTemplars,List<GameObject>SattackAssassins,List<GameObject>SattackTemplars)
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
      public void PutAnAument()
      {
        if(gameObject.GetComponent<UnityCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins)
        {
          for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Length; i++)
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
          for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Length; i++)
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
       private int CounterOfUnitys (List<GameObject>Mattack,List<GameObject>Rattack,List<GameObject>Sattack)
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

}
