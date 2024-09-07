using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
   public int Id{get;set;}
   public GameObject Camera{get;set;}
   public GameObject Hand{get;set;}
   public GameObject Field{get;set;}
   public GameObject Deck{get;set;}
   public GameObject Graveyard{get;set;}

   public Player(GameObject camera, GameObject hand, GameObject field, GameObject deck, GameObject graveyard)
   {
        Camera = camera;
        Hand = hand;
        Field = field;
        Deck = deck;
        Graveyard = graveyard; 

        if(Camera.transform.rotation.z == 1){Id = 1;} else {Id = 0;} 
   }

    public Player()
    {
    }
}
