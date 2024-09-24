using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureCardScript : MonoBehaviour
{
    public  string NameCard;
    public UnityCard.EnumTypeCard TypeCard;  
    public UnityCard.EnumEfects EfectCard;
    public UnityCard.EnumFactionCard FactionCard;

    public bool IsPlayed;
    public GameObject GameManager;
    public GameObject Board;  
    public Sprite CardsFront;
    public Sprite AssassinsCardsBack;
    public Sprite TemplarsCardsBack;
    public GameObject ZoomAssassin;
    public GameObject ZoomTemplar;
    [SerializeField] LureCard LureCard;

    public int Owner {get => LureCard.FactionCard == UnityCard.EnumFactionCard.Assassins ? 1 : 2;}
    void Start() 
    {
      NameCard        = LureCard.NameCard;
      TypeCard        = LureCard.TypeCard;
      EfectCard       = LureCard.EfectCard;
      FactionCard     = LureCard.FactionCard;

      Board        = GameObject.FindGameObjectWithTag("board");
      ZoomAssassin = GameObject.FindGameObjectWithTag("Zoom Assassin");
      ZoomTemplar = GameObject.FindGameObjectWithTag("Zoom Templar");
      GameManager  = GameObject.FindGameObjectWithTag("Game Manager");
    }

    void Update()
    {
         if(GameManager.GetComponent<GameManajer>().AssassinPlay)
      {
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Count;i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null && GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Lure Card") )
                {
                    GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<LureCardScript>().CardsFront;
                }
            }
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Count; i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] != null && GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].CompareTag("Lure Card"))
                {
                    if (!GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<LureCardScript>().IsPlayed)
                    {
                        GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<LureCardScript>().TemplarsCardsBack;
                    }
                }
            }
      }

      if(GameManager.GetComponent<GameManajer>().TemplarsPlay)
      {
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandTemplar.Count;i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandTemplar[i] != null && GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].CompareTag("Lure Card") )
                {
                    GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandTemplar[i].GetComponent<LureCardScript>().CardsFront;
                }
            }
            for (int i = 0; i < GameManager.GetComponent<GameManajer>().CardsHandAssassin.Count; i++)
            {
                if(GameManager.GetComponent<GameManajer>().CardsHandAssassin[i] != null && GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].CompareTag("Lure Card"))
                {
                    if (!GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<LureCardScript>().IsPlayed)
                    {
                        GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<SpriteRenderer>().sprite = GameManager.GetComponent<GameManajer>().CardsHandAssassin[i].GetComponent<LureCardScript>().AssassinsCardsBack;
                    }
                }
            }
      }
    }
    //El siguiente bloque tiene como funcion que al presionarse el senuelo se activa el boolean LureIsReady y al presionar autoseguido una carta de unidad que ya ha sido jugada, esta regresa a la mano
    private void OnMouseDown()
    {
        if(GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
        {
            GameManager.GetComponent<GameManajer>().LureIsReady  = true;
            GameManager.GetComponent<GameManajer>().LureForChange = gameObject;
        }

        if(!GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin && GameManager.GetComponent<GameManajer>().AssassinPlay && gameObject.GetComponent<LureCardScript>().FactionCard == UnityCard.EnumFactionCard.Assassins)
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
            if(!GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar && GameManager.GetComponent<GameManajer>().TemplarsPlay && gameObject.GetComponent<LureCardScript>().FactionCard == UnityCard.EnumFactionCard.Templar)
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
