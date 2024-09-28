using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Hand {
    public List<GameObject>CardsHand;
    public GameObject[] PositionHand;
    public GameObject HandPlayer;
    public bool[] MaskPositionHand;
    
    public Hand(List<GameObject> cardsHand, GameObject[] positionHand, GameObject handPlayer, bool[] maskPositionHand){
        
        CardsHand = cardsHand;
        PositionHand = positionHand;
        HandPlayer = handPlayer;
        MaskPositionHand = maskPositionHand;
    }

    public List<GameObject> Find(Predicate<GameObject> predicate){

        List<GameObject> listed = new();

        foreach (var item in CardsHand)
        {
            if(predicate.Invoke(item)){
                listed.Add(item);
            }
        }
        
        return listed;
    }

    public void Push(GameObject card){

        for (int i = 0; i < CardsHand.Count; i++)
        {
            if(CardsHand[i] is null && i < CardsHand.Count-1){
                
                if(CardsHand[i+1] is not null){
                    CardsHand[i+1].transform.position = PositionHand[i].transform.position;
                    CardsHand[i] =  CardsHand[i+1];
                    CardsHand[i+1] = null;
                    MaskPositionHand[i] = true;
                    MaskPositionHand[i+1] = false;
                }
            }
        }

        InstanceCard(card,PositionHand[CardsHand.Count-1],CardsHand.Count-1);
    }

     public void SendBottom(GameObject card){

        for (int i = CardsHand.Count-1 ; i >= 0; i--)
        {
            if(CardsHand[i] is null && i > 0){
                
                if(CardsHand[i-1] is not null){
                    CardsHand[i-1].transform.position = PositionHand[i].transform.position;
                    CardsHand[i] = CardsHand[i-1];
                    CardsHand[i-1] = null;
                    MaskPositionHand[i] = true;
                    MaskPositionHand[i-1] = false;
                }
            }
        }
        InstanceCard(card,PositionHand[0],0);

    }

    public void Add(GameObject card){

        for (int i = 0; i < CardsHand.Count; i++)
        {
            if(CardsHand[i] is null){
                InstanceCard(card,PositionHand[i],i);
                return;
            }
        }
    }

    public GameObject Pop(){

        for (int i = CardsHand.Count-1; i >= 0; i++)
        {
            if(CardsHand[i] is not null){
                var card = GameObject.Instantiate(CardsHand[i], new UnityEngine.Vector3(1000,1000,1000),UnityEngine.Quaternion.identity);
                CardsHand[i].transform.position = new UnityEngine.Vector3(1000,1000,1000);
                CardsHand[i] = null;
                return card;
            }
        }

        throw new Exception("Hand doesnt contain elements for Pop function");
    }

    public void Remove(GameObject card){

        for (int i = 0; i < CardsHand.Count; i++)
        {
            if(CardsHand[i].name.Equals(card.name)){

                //CardsHand[i].transform.position = new Vector3(1000,1000,1000);
                CardsHand[i] = null;
                return;
            }
        }

        throw new Exception("Hand doesnt contain elements for Remove function");
    }

    public void Shuffle()
    {
        var random = new System.Random();
        for (int i = 0; i < CardsHand.Count; i++)
        {
            int randomIndex1 = random.Next(0,CardsHand.Count-1);
            int randomIndex2 = random.Next(0,CardsHand.Count-1);

            if(CardsHand[randomIndex1] is not null && CardsHand[randomIndex2] is not null){
                var card = CardsHand[randomIndex1];
                UnityEngine.Vector3 transformcard = CardsHand[randomIndex1].transform.position;

                CardsHand[randomIndex1].transform.position = CardsHand[randomIndex2].transform.position;
                CardsHand[randomIndex1] = CardsHand[randomIndex2];
                CardsHand[randomIndex2].transform.position = transformcard;
                CardsHand[randomIndex2]=card;
            }
        }
    }
    
    public void InstanceCard(GameObject card,GameObject positioGO, int index){

        GameObject drawCard = GameObject.Instantiate(card,positioGO.transform.position,UnityEngine.Quaternion.identity);
        drawCard.transform.SetParent(HandPlayer.transform, false);
        drawCard.transform.position = PositionHand[index].transform.position;
        drawCard.transform.localScale = PositionHand[index].transform.localScale;
        CardsHand[index] = drawCard;
        MaskPositionHand[index] = true;
    }

}
