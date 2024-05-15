using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class CleanCardScript : MonoBehaviour
{

    public string NameCard;
    public UnityCard.EnumEfects EfectCard;
    public UnityCard.EnumFactionCard FactionCard;
    public UnityCard.EnumTypeClimAndClean TypeClean;
    
    public bool IsPlayed;
    public GameObject GameManager;
    public GameObject Board;  
    public Sprite CardsFront;
    public Sprite AssassinsCardsBack;
    public Sprite TemplarsCardsBack;
    public GameObject ZoomAssassin;
    public GameObject ZoomTemplar;
    [SerializeField] CleanCard cleanCard;
    
    //Verificar la pincha de los climas que dan error cuando se juega un despeje 
    void Start() 
    {
      NameCard        = cleanCard.NameCard;
      TypeClean       = cleanCard.TypeClean;
      EfectCard       = cleanCard.EfectCard;
      FactionCard     = cleanCard.FactionCard;

      Board        = GameObject.FindGameObjectWithTag("board");
      ZoomAssassin = GameObject.FindGameObjectWithTag("Zoom Assassin");
      ZoomTemplar = GameObject.FindGameObjectWithTag("Zoom Templar");
      GameManager  = GameObject.FindGameObjectWithTag("Game Manager");
    }

    void Update()
    {
      if(GameManager.GetComponent<GameManajer>().AssassinPlay)
      {
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Length;i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null && GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Clean Card") )
                {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<CleanCardScript>().CardsFront;
                }
            }
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Length; i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] != null && GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].CompareTag("Clean Card"))
                {
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<CleanCardScript>().TemplarsCardsBack;
                }
            }
      }

      if(GameManager.GetComponent<GameManajer>().TemplarsPlay)
      {
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Length;i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] != null && GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].CompareTag("Clean Card") )
                {
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<CleanCardScript>().CardsFront;
                }
            }
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Length; i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null && GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Clean Card"))
                {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<CleanCardScript>().AssassinsCardsBack;
                }
            }
      }
    }

    private void OnMouseDown()
    {
        if(GameManager.GetComponent<GameManajer>().CounterPassRound > 0)
        {
            GameManager.GetComponent<GameManajer>().CounterPassRound -=1;
        }
       
        if(GameManajer.IsInHand(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject) && GameManager.GetComponent<GameManajer>().AssassinPlay && !IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
        {
            IsPlayed = true;
            GameManager.GetComponent<GameManajer>().maskPositionHandAssassin[GameManajer.PositionOfGameObject(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject)] = false;
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
        }
            if(GameManajer.IsInHand(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject) &&  GameManager.GetComponent<GameManajer>().TemplarsPlay && !IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
             {
                IsPlayed = true;
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
             if(!GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin && GameManager.GetComponent<GameManajer>().AssassinPlay && GameManajer.IsInHand(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject))
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
            if(!GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar && GameManager.GetComponent<GameManajer>().TemplarsPlay && GameManajer.IsInHand(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject))
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
   
   void OnMouseEnter() 
      {
          if(GameManager.GetComponent<GameManajer>().AssassinPlay)
          ZoomAssassin.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite; 
          else
          ZoomTemplar.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
      }
}

