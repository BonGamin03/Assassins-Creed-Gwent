using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class BossesEfect : MonoBehaviour
{
    internal string NameCard;
    private UnityCard.EnumTypeCard TypeCard;
    internal UnityCard.EnumFactionCard factionCard;
    private GameObject GameManager;
    private GameObject Strongest;
    private GameObject Board;
    public UnityCard.EnumEfects Effect;
    public GameObject ZoomAssassin;
    public GameObject ZoomTemplar;
    public bool IsPlayed;
    public BossCard bossCard;

    public int Owner {get => bossCard.FactionCard == UnityCard.EnumFactionCard.Assassins ? 1 : 2;}
    void Start()
    {
        NameCard = bossCard.NameCard;
        TypeCard = bossCard.TypeCard;
        factionCard = bossCard.FactionCard;
        Effect = bossCard._Effect;

        ZoomAssassin = GameObject.FindGameObjectWithTag("Zoom Assassin");
        ZoomTemplar  = GameObject.FindGameObjectWithTag("Zoom Templar");
        GameManager = GameObject.FindGameObjectWithTag("Game Manager");
        Board       = GameObject.FindGameObjectWithTag("board");
    }
    private void OnMouseDown() {
        Effects.GetTipe(gameObject);
    }
      void OnMouseEnter() 
      {
          if(GameManager.GetComponent<GameManajer>().AssassinPlay)
          ZoomAssassin.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite; 
          else
          ZoomTemplar.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
      }


}
