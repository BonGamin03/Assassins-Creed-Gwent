using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CompilerManager 
{
    private static readonly Player player1 = GameManajer.GameManger.AssassinPlayer;
    private static readonly Player player2 = GameManajer.GameManger.TemplarPlayer;

    public delegate void OperationPuSeRe (List<GameObject> list,GameObject gameObject);


    public static Player GetPlayer() => GameManajer.GameManger.AssassinPlay ? player1 :player2;
    public static Player GetOtherPlayer() => GameManajer.GameManger.AssassinPlay ? player2 :player1;

    public static Player GetPlayerById(double id) => player1.Id == id ? player1 : player2;
    public static Field GetFieldById(double id) => player1.Id == id ? player1.Field : player2.Field;
    public static List<GameObject> GetDeckbyId(double id) => player1.Id == id ? player1.Deck.GetComponent<DeckScript>().deck : player2.Deck.GetComponent<DeckScript>().deck;
    public static List<GameObject> GetGraveyard(double id) => player1.Id == id ? player1.Graveyard.GetComponent<Graveyard>().DeadCards : player2.Graveyard.GetComponent<Graveyard>().DeadCards;

    public static double GetTriggerPlayer() => GetPlayer().Id;

    public static List<GameObject> Find (Predicate<GameObject> predicate, List<GameObject> List) => EvalPred(predicate,List);

    public static void Push(List<GameObject> list, GameObject card) => list.Add(card);
    public static void SendBottom(List<GameObject> list, GameObject card){
        list.Add(card);
        list.Reverse();
    }

    public static GameObject Pop(List<GameObject> gameObjects){

        gameObjects.Remove(gameObjects.Last());

        return gameObjects.Last();
    }

    public static void Remove(List<GameObject> list, GameObject card) => list.Remove(card);

    public static void Shuffle(List<GameObject> list){

        var random = new System.Random();
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex1 = random.Next(0,list.Count-1);
            int randomIndex2 = random.Next(0,list.Count-1);

            var card = list[randomIndex1];
            list[randomIndex1] = list[randomIndex2];
            list[randomIndex2] = card;
        }
    }

    private static List<GameObject> EvalPred(Predicate<GameObject> predicate, List<GameObject> list){

        var list1 = list.ToList();

        foreach (var item in list1)
        {
            if(predicate.Invoke(item))
            list.Remove(item);
        }

        return list;
    }
}