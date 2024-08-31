using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AumentCardScript : MonoBehaviour
{
    public string NameCard;
    public UnityCard.EnumEfects EfectCard;
     public UnityCard.EnumTypeCard TypeCard; 
    public UnityCard.EnumFactionCard FactionCard;
    public UnityCard.EnumTypeAttackCard RowAument;
    
    public bool IsPlayed;
    public GameObject GameManager;
    public GameObject Board;  
    public Sprite CardsFront;
    public Sprite AssassinsCardsBack;
    public Sprite TemplarsCardsBack;
    public GameObject ZoomAssassin;
    public GameObject ZoomTemplar;
    [SerializeField] AumentCard aumentCard;
    void Start() 
    {
      NameCard        = aumentCard.NameCard;
      RowAument       = aumentCard.RowAument;
      TypeCard        = aumentCard.TypeCard;
      EfectCard       = aumentCard.EfectCard;
      FactionCard     = aumentCard.FactionCard;

      Board        = GameObject.FindGameObjectWithTag("board");
      ZoomAssassin = GameObject.FindGameObjectWithTag("Zoom Assassin");
      ZoomTemplar  = GameObject.FindGameObjectWithTag("Zoom Templar");
      GameManager  = GameObject.FindGameObjectWithTag("Game Manager");
    }

    void Update()
    {
         if(GameManager.GetComponent<GameManajer>().AssassinPlay)
      {
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Length;i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null && GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Aument Card") )
                {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<AumentCardScript>().CardsFront;
                }
            }
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Length; i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] != null && GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].CompareTag("Aument Card"))
                {
                        GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<AumentCardScript>().TemplarsCardsBack;
                }
            }
      }
      
      if(GameManager.GetComponent<GameManajer>().TemplarsPlay)
      {
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Length;i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] != null && GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].CompareTag("Aument Card") )
                {
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<AumentCardScript>().CardsFront;
                }
            }
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Length; i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null && GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Aument Card"))
                {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<AumentCardScript>().AssassinsCardsBack;
                }
            }
      }
    }

    private void OnMouseDown() {

      if(GameManager.GetComponent<GameManajer>().CounterPassRound > 0)
      {
        GameManager.GetComponent<GameManajer>().CounterPassRound -=1;
      }
      
      if(gameObject.GetComponent<AumentCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins && GameManager.GetComponent<GameManajer>().AssassinPlay && !IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
      {
          IsPlayed = true;
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

      if(gameObject.GetComponent<AumentCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar && GameManager.GetComponent<GameManajer>().TemplarsPlay && !IsPlayed && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
      {
          IsPlayed = true;
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

        if(!GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin && GameManager.GetComponent<GameManajer>().AssassinPlay && gameObject.GetComponent<AumentCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins)
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
        if(!GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar && GameManager.GetComponent<GameManajer>().TemplarsPlay && gameObject.GetComponent<AumentCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar)
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
      
    public static bool IsGoldUnity(GameObject gameObject)
    {
      if(gameObject.GetComponent<UnityCardScript>().TypeUnity == UnityCard.EnumTypeUnity.Gold)
      return true;

      return false;
    }

}
