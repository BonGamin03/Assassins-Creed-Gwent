using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherCardScript : MonoBehaviour
{
    public string NameCard;
    public UnityCard.EnumTypeCard TypeCard;
    public UnityCard.EnumEfects EfectCard;
    public UnityCard.EnumFactionCard FactionCard;
    public UnityCard.EnumTypeClimAndClean TypeClim;
    public bool IsPlayed;
    public GameObject GameManager;
    public GameObject Board;  
    public Sprite CardsFront;
    public Sprite AssassinsCardsBack;
    public Sprite TemplarsCardsBack;
    public GameObject ZoomAssassin;
    public GameObject ZoomTemplar;
    [SerializeField] WeatherCard weatherCard;
    // Start is called before the first frame update
   void Start()
    {
      NameCard        = weatherCard.NameCard;
      TypeCard        = weatherCard.TypeCard;
      TypeClim        = weatherCard.TypeClim;
      EfectCard       = weatherCard.EffectCard;
      FactionCard     = weatherCard.FactionCard;

      Board   = GameObject.FindGameObjectWithTag("board");
      ZoomAssassin = GameObject.FindGameObjectWithTag("Zoom Assassin");
      ZoomTemplar  = GameObject.FindGameObjectWithTag("Zoom Templar");
      GameManager = GameObject.FindGameObjectWithTag("Game Manager");
    }
    void Update()
    { // Las siguientes lineas simulan la rotacion de la carta entre turnos
        if(GameManager.GetComponent<GameManajer>().AssassinPlay)
        {
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Length;i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null && GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Weather Card ") )
                {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<WeatherCardScript>().CardsFront;
                }
            }
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Length; i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] != null && GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].CompareTag("Weather Card "))
                {
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<WeatherCardScript>().TemplarsCardsBack;
                }
            }
        }

      if(GameManager.GetComponent<GameManajer>().TemplarsPlay)
      {
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Length;i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] != null && GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].CompareTag("Weather Card ") )
                {
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<WeatherCardScript>().CardsFront;
                }
            }
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Length; i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null && GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Weather Card "))
                {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<WeatherCardScript>().AssassinsCardsBack;
                }
            }
       }
    }

    private void OnMouseDown() {
        //El bloque de codigo siguiente se utiliza para que si se producen dos pases consecutivos por parte de ambos jugadores se finalice la ronda 
        if(GameManager.GetComponent<GameManajer>().CounterPassRound > 0)
        {
            GameManager.GetComponent<GameManajer>().CounterPassRound -=1;
        }
        //En este caso para invocar una carta se utiliza para saber en que mano se encuentra la carta el metodo is in Hand esto es debido a que las cartas de clima a pesar de que se encuentran en los mazos de las diferentes facciones son cartas neutrales 
        if(GameManajer.IsInHand(GameManager.GetComponent<GameManajer>().CardsHandAssassin,gameObject) && GameManager.GetComponent<GameManajer>().AssassinPlay && !IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
        {
            IsPlayed = true;
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
        
        if(GameManajer.IsInHand(GameManager.GetComponent<GameManajer>().CardsHandTemplar,gameObject) &&  GameManager.GetComponent<GameManajer>().TemplarsPlay && !IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
        {
            IsPlayed = true;
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
            //EL bloque de codigo siguiente es para cambiar dos cartas al inicio del juego
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
     
   //Metodo para el zoom de las cartas 
    void OnMouseEnter() 
      {
          if(GameManager.GetComponent<GameManajer>().AssassinPlay)
          ZoomAssassin.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite; 
          else
          ZoomTemplar.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
      }
    
}
