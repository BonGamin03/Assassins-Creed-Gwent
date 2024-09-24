using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player 
{
   public double Id{get;set;}
   public bool IsHisTurn{get; set;}
   public Hand Hand {get;set;}
   public Field Field{get;set;}
   public GameObject Deck{get;set;}
   public GameObject Graveyard{get;set;}
   public  delegate void OperationFuncPlay();
   public Player(int id, bool isHisTurn, Hand hand, Field field, GameObject deck, GameObject graveyard)
   {
          IsHisTurn = isHisTurn;
          Id = id;
          Hand = hand;
          Field = field;
          Deck = deck;
          Graveyard = graveyard; 
   }

    public Player(){}

    public List<GameObject> HandOfPlayer() => Hand.CardsHand;

    public Field FieldOfPlayer() => Field;

    public List<GameObject> GraveyardPlayer() => Graveyard.GetComponent<Graveyard>().DeadCards;
    public List<GameObject> DeckOfPlayer() => Deck.GetComponent<DeckScript>().deck;

}
