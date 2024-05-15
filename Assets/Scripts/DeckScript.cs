using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public List<GameObject> deck;
    public UnityCard.EnumFactionCard factionDeck;
    
    public int Count
    {
        get{
            return deck.Count;
        }
    }

    public GameObject IndexingOptions(int index)
    {
        return deck[index];
    }
    public void Add(GameObject gameObject)
    {
        deck.Add(gameObject);
    }
   public void RemoveAt(int index)
   {
        deck.RemoveAt(index);
   }
   public void Remove(GameObject gameObject)
   {
        deck.Remove(gameObject);
   }

}
