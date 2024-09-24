using System.Collections;
using System.Collections.Generic;
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

}
