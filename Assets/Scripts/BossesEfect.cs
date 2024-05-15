using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class BossesEfect : MonoBehaviour
{
    private string NameCard;
    private UnityCard.EnumTypeCard TypeCard;
    private UnityCard.EnumFactionCard factionCard;
    private GameObject GameManager;
    private GameObject Strongest;
    private GameObject Board;

    public GameObject ZoomAssassin;
    public GameObject ZoomTemplar;
    public bool IsPlayed;
    [SerializeField] BossCard bossCard;
    void Start()
    {
        NameCard = bossCard.NameCard;
        TypeCard = bossCard.TypeCard;
        factionCard = bossCard.FactionCard;


        ZoomAssassin = GameObject.FindGameObjectWithTag("Zoom Assassin");
        ZoomTemplar  = GameObject.FindGameObjectWithTag("Zoom Templar");
        GameManager = GameObject.FindGameObjectWithTag("Game Manager");
        Board       = GameObject.FindGameObjectWithTag("board");
    }
    private void OnMouseDown() {

            if(!IsPlayed && gameObject.GetComponent<BossesEfect>().factionCard == UnityCard.EnumFactionCard.Assassins && GameManager.GetComponent<GameManajer>().AssassinPlay)
            {
              if(Board.GetComponent<BoardScript>().AssassinsMAttack.Count != 0 || Board.GetComponent<BoardScript>().AssassinsRAttack.Count != 0 || Board.GetComponent<BoardScript>().AssassinsSAttack.Count != 0)
              {
                IsPlayed = true;
                Strongest = UnityCardScript.Biggest(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack);
                GameManager.GetComponent<GameManajer>().AssassinPoints += Strongest.GetComponent<UnityCardScript>().PointAttackCard;
                GameManager.GetComponent<GameManajer>().AssassinPlay = false;
                GameManager.GetComponent<GameManajer>().TemplarsPlay = true;
              }
            }
            if(!IsPlayed && gameObject.GetComponent<BossesEfect>().factionCard == UnityCard.EnumFactionCard.Templar && GameManager.GetComponent<GameManajer>().TemplarsPlay )
            {
                IsPlayed = true;
                GameManager.GetComponent<GameManajer>().AssassinPoints -= 7;
                GameManager.GetComponent<GameManajer>().AssassinPlay = true;
                GameManager.GetComponent<GameManajer>().TemplarsPlay = false;
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
