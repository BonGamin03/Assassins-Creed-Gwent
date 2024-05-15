using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using TMPro;
using Unity.Jobs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameManajer : MonoBehaviour //********************Me falat implementar que si juega un clima y no ha cambiado carta no pueda hacerlo en otras palabras hacer wque la condicion bajo la cual se puede jugar una carta al igual que las otras antes tienes qu ehaber cambiado de carta 
{
    public GameObject HandPlayerAssassin;
    public GameObject HandPlayerTemplar;
    public GameObject Templarsdeck;
    public GameObject Assassinsdeck;
    public GameObject[] CardsHandAssassin;
    public GameObject[] CardsHandTemplar;
    public GameObject Board;
    public Canvas canvas; // Solo lo necesito para si apagarlo cundo gane la ronda 
    public Sprite AssassinsCardsBack;
    public Sprite TemplarsCardsBack;
    public bool AssassinPlay;
    public bool TemplarsPlay;
    public  int AssassinPoints;
    public  int TemplarPoints;
    public  int AssassinRoundWins;
    public  int TemplarRoundWins;
    public int RealNumberCardsInHandAssassin =0;
    public int RealNumberCardsInHandTemplar =0;
    public int CounterPassRound; 
    public TMP_Text AssassinPointsText;
    public TMP_Text TemplarsPointsText;
    public GameObject[] PositionHandAssassin;
    public bool[] maskPositionHandAssassin;
    public bool[] maskPositionHandTemplar;
    public GameObject[] PositionHandTemplar;

    public GameObject LureForChange;
    public GameObject UnityCardForChange;
    public GameObject AssassinInterfaceOfWin;
    public GameObject TemplarInterfaceOfWin;

    //This is for change at the end of game
    public GameObject CardForChangeAtTheBeggining;
    public bool AlreadyChangedAssassin;
    public int MaxChangeAssassin;
    public int MaxChangeTemplar;
    public bool AlreadyChangedTemplar;
    public bool LureIsReady;
     
   

    
    void Start()
    {
        AssassinPlay             = true;
        Assassinsdeck            = GameObject.FindGameObjectWithTag("Assassins");
        Templarsdeck             = GameObject.FindGameObjectWithTag("TemplarsDeck");
        Board                    = GameObject.FindGameObjectWithTag("board");
                            
        for (int i = 0; i < 10; i++)
        {
            int RandomIndexCard =UnityEngine.Random.Range(0,Assassinsdeck.GetComponent<DeckScript>().deck.Count);

            GameObject drawCardAssassin = Instantiate(Assassinsdeck.GetComponent<DeckScript>().deck[RandomIndexCard],PositionHandAssassin[i].transform.position,UnityEngine.Quaternion.identity);
            drawCardAssassin.transform.SetParent(HandPlayerAssassin.transform,false);
            drawCardAssassin.transform.position = PositionHandAssassin[i].transform.position;
            Assassinsdeck.GetComponent<DeckScript>().RemoveAt(RandomIndexCard);
            AddCardToHand(CardsHandAssassin,drawCardAssassin);
            maskPositionHandAssassin[i] = true;

            GameObject drawCardTemplar = Instantiate(Templarsdeck.GetComponent<DeckScript>().deck[RandomIndexCard],PositionHandTemplar[i].transform.position,Templarsdeck.transform.rotation);
            drawCardTemplar.transform.SetParent(HandPlayerTemplar.transform, false);
            drawCardTemplar.transform.position = PositionHandTemplar[i].transform.position;
            Templarsdeck.GetComponent<DeckScript>().RemoveAt(RandomIndexCard);
            AddCardToHand(CardsHandTemplar,drawCardTemplar);
            maskPositionHandTemplar[i] = true;
        }
    }

    // Update is called once per frame Vector3(-216,91,0)
    void Update()
    {
        if(AssassinRoundWins == 2)
        {
            AssassinPlay = true;
            AssassinInterfaceOfWin.SetActive(true);
            Board.SetActive(false);
            AssassinPointsText.gameObject.transform.localScale = new UnityEngine.Vector3(0,0,0);
            TemplarsPointsText.gameObject.transform.localScale = new UnityEngine.Vector3(0,0,0);
        }
        if(TemplarRoundWins == 2)
        {
            AssassinPlay = true;
            TemplarInterfaceOfWin.SetActive(true);
            Board.SetActive(false);
            AssassinPointsText.gameObject.transform.localScale = new UnityEngine.Vector3(0,0,0);
            TemplarsPointsText.gameObject.transform.localScale = new UnityEngine.Vector3(0,0,0);
        }
        if(AssassinPlay)    
        {   
            TemplarPoints += SumPoints(Board.GetComponent<BoardScript>().TemplarsMAttack,Board.GetComponent<BoardScript>().TemplarsRAttack,Board.GetComponent<BoardScript>().TemplarSAttack);
            TemplarsPointsText.text = TemplarPoints.ToString();
            AssassinPointsText.text= AssassinPoints.ToString();
            
            
            
        }

        if(TemplarsPlay)
        {
            AssassinPoints += SumPoints(Board.GetComponent<BoardScript>().AssassinsMAttack,Board.GetComponent<BoardScript>().AssassinsRAttack,Board.GetComponent<BoardScript>().AssassinsSAttack);
            AssassinPointsText.text= AssassinPoints.ToString();
            TemplarsPointsText.text = TemplarPoints.ToString();   
        }

        if(CounterPassRound ==2)
        {
            RestartPlay();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < maskPositionHandAssassin.Length; j++)
                {
                    if(!maskPositionHandAssassin[j] && PositionsNoNull(CardsHandAssassin)<10)
                    {
                        int index = UnityEngine.Random.Range(0,Assassinsdeck.GetComponent<DeckScript>().deck.Count);
                        GameObject drawCardAssassin = Instantiate(Assassinsdeck.GetComponent<DeckScript>().deck[index],PositionHandAssassin[j].transform.position,UnityEngine.Quaternion.identity);
                        drawCardAssassin.transform.SetParent(HandPlayerAssassin.transform, false);
                        drawCardAssassin.transform.position = PositionHandAssassin[j].transform.position;
                        drawCardAssassin.transform.localScale = PositionHandAssassin[j].transform.localScale;
                        CardsHandAssassin[j] = drawCardAssassin;
                        Assassinsdeck.GetComponent<DeckScript>().RemoveAt(index);
                        maskPositionHandAssassin[j] = true;
                        break;
                    }
                }

                for (int j= 0; j < maskPositionHandTemplar.Length; j++)
                {
                    if(!maskPositionHandTemplar[j] && PositionsNoNull(CardsHandTemplar)<10)
                    {
                        int index = UnityEngine.Random.Range(0,Templarsdeck.GetComponent<DeckScript>().deck.Count);
                        GameObject drawCardTemplar = Instantiate(Templarsdeck.GetComponent<DeckScript>().deck[index],PositionHandTemplar[j].transform.position,Templarsdeck.transform.rotation);
                        drawCardTemplar.transform.SetParent(HandPlayerTemplar.transform, false);
                        drawCardTemplar.transform.position = PositionHandTemplar[j].transform.position;
                        drawCardTemplar.transform.localScale = PositionHandTemplar[j].transform.localScale;
                        CardsHandTemplar[j] = drawCardTemplar;
                        Templarsdeck.GetComponent<DeckScript>().RemoveAt(index);
                        maskPositionHandTemplar[j] = true;
                        break;
                    }
                }
            }
        
            if(AssassinPoints > TemplarPoints)
            {
                AssassinRoundWins++;
                AssassinPlay= true;
                TemplarsPlay= false;
            }

            if(TemplarPoints > AssassinPoints)
            {
                TemplarRoundWins++;
                AssassinPlay= false;
                TemplarsPlay= true;
            }
            AssassinPoints = 0;
            TemplarPoints  = 0;
            CounterPassRound =0;
        }
    
    }
        
    public int SumPoints (List<GameObject>UnityMattack,List<GameObject>UnityRattack,List<GameObject>UnitySattack)
    {
        int points = 0;
        for (int i = 0; i < UnityMattack.Count; i++)
        {
            if (UnityMattack[i] != null)
            {
            if (!UnityMattack[i].CompareTag("Lure Card") && !UnityMattack[i].GetComponent<UnityCardScript>().AlreadySum )
            {
                points += UnityMattack[i].GetComponent<UnityCardScript>().PointAttackCard;
                UnityMattack[i].GetComponent<UnityCardScript>().AlreadySum = true;
            }
            }
        }
        for (int i = 0; i < UnityRattack.Count; i++)
        {
            if (!UnityRattack[i].CompareTag("Lure Card") && UnityRattack[i] != null)
            {
             if (!UnityRattack[i].GetComponent<UnityCardScript>().AlreadySum )
            {
                points += UnityRattack[i].GetComponent<UnityCardScript>().PointAttackCard;
                UnityRattack[i].GetComponent<UnityCardScript>().AlreadySum = true;
            }
            }
        }
        for (int i = 0; i < UnitySattack.Count; i++)
        {
            if (UnitySattack[i] != null)
            {
             if (!UnitySattack[i].CompareTag("Lure Card") && !UnitySattack[i].GetComponent<UnityCardScript>().AlreadySum )
            {
                points += UnitySattack[i].GetComponent<UnityCardScript>().PointAttackCard;
                UnitySattack[i].GetComponent<UnityCardScript>().AlreadySum = true;
            }
            }
        }
        return points;
    }

     public static bool IsInHand(GameObject[] TheHand, GameObject WeatherCard)
    {
        for (int i = 0; i < TheHand.Length; i++)
        {
            if(TheHand[i] != null && TheHand[i] == WeatherCard)
            return true;
        }
       
        return false;
    }

    private void RestartPlay()
    {
        //Assassins Section 
        for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsMAttack.Count; i++)
        {
            Destroy(Board.GetComponent<BoardScript>().AssassinsMAttack[i]);
        }
        Board.GetComponent<BoardScript>().AssassinsMAttack.Clear();

        for (int i = 0; i < Board.GetComponent<BoardScript>().maskAssassinMattack.Length; i++)
        {
            Board.GetComponent<BoardScript>().maskAssassinMattack[i] = false;
        }
        for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsRAttack.Count; i++)
        {
            Destroy(Board.GetComponent<BoardScript>().AssassinsRAttack[i]);
        }
        Board.GetComponent<BoardScript>().AssassinsRAttack.Clear();
        for (int i = 0; i < Board.GetComponent<BoardScript>().maskAssassinRattack.Length; i++)
        {
            Board.GetComponent<BoardScript>().maskAssassinRattack[i] = false;
        }
        for (int i = 0; i < Board.GetComponent<BoardScript>().AssassinsSAttack.Count; i++)
        {
            Destroy(Board.GetComponent<BoardScript>().AssassinsSAttack[i]);
        }
        Board.GetComponent<BoardScript>().AssassinsSAttack.Clear();
        for (int i = 0; i < Board.GetComponent<BoardScript>().maskAssassinSattack.Length; i++)
        {
            Board.GetComponent<BoardScript>().maskAssassinSattack[i] = false;
        }
        for (int i = 0; i < Board.GetComponent<BoardScript>().WeatherAssassins.Count; i++)
        {
            Destroy(Board.GetComponent<BoardScript>().WeatherAssassins[i]);
        }
        Board.GetComponent<BoardScript>().WeatherAssassins.Clear();
        Board.GetComponent<BoardScript>().MaskAssassinsWeatherM = false;
        Board.GetComponent<BoardScript>().MaskAssassinsWeatherR = false;
        Board.GetComponent<BoardScript>().MaskAssassinsWeatherS = false;

        for (int i = 0; i < Board.GetComponent<BoardScript>().AumentAssassins.Count; i++)
        {
            Destroy(Board.GetComponent<BoardScript>().AumentAssassins[i]);
        }
        Board.GetComponent<BoardScript>().AumentAssassins.Clear();
        Board.GetComponent<BoardScript>().MaskAssassinsAumentM = false;
        Board.GetComponent<BoardScript>().MaskAssassinsAumentR = false;
        Board.GetComponent<BoardScript>().MaskAssassinsAumentS = false;


       
    // Templars Section

    for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarsMAttack.Count; i++)
        {
            Destroy(Board.GetComponent<BoardScript>().TemplarsMAttack[i]);
        }
        Board.GetComponent<BoardScript>().TemplarsMAttack.Clear();
        for (int i = 0; i < Board.GetComponent<BoardScript>().maskTemplarMattack.Length; i++)
        {
            Board.GetComponent<BoardScript>().maskTemplarMattack[i] = false;
        }
        for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarsRAttack.Count; i++)
        {
            Destroy(Board.GetComponent<BoardScript>().TemplarsRAttack[i]);
        }
        Board.GetComponent<BoardScript>().TemplarsRAttack.Clear();
        for (int i = 0; i < Board.GetComponent<BoardScript>().maskTemplarRattack.Length; i++)
        {
            Board.GetComponent<BoardScript>().maskTemplarRattack[i] = false;
        }
        for (int i = 0; i < Board.GetComponent<BoardScript>().TemplarSAttack.Count; i++)
        {
            Destroy(Board.GetComponent<BoardScript>().TemplarSAttack[i]);
        }
        Board.GetComponent<BoardScript>().TemplarsMAttack.Clear();
        for (int i = 0; i < Board.GetComponent<BoardScript>().maskTemplarSattack.Length; i++)
        {
            Board.GetComponent<BoardScript>().maskTemplarSattack[i] = false;
        }
        for (int i = 0; i < Board.GetComponent<BoardScript>().WeatherTemplars.Count; i++)
        {
            Destroy(Board.GetComponent<BoardScript>().WeatherTemplars[i]);
        }
        Board.GetComponent<BoardScript>().WeatherTemplars.Clear();
        Board.GetComponent<BoardScript>().MaskTemplarsWeatherM = false;
        Board.GetComponent<BoardScript>().MaskTemplarsWeatherR = false;
        Board.GetComponent<BoardScript>().MaskTemplarsWeatherS = false;

        for (int i = 0; i < Board.GetComponent<BoardScript>().AumentTemplars.Count; i++)
        {
            Destroy(Board.GetComponent<BoardScript>().AumentTemplars[i]);
        }
        Board.GetComponent<BoardScript>().AumentTemplars.Clear();
        Board.GetComponent<BoardScript>().MaskTemplarsAumentM = false;
        Board.GetComponent<BoardScript>().MaskTemplarsAumentR = false;
        Board.GetComponent<BoardScript>().MaskTemplarsAumentS = false;

    }
    //Metodos para hacer modificaciones en los Arrays CardsHandAssassin y CardsHandTemplar

    public static void AddCardToHand(GameObject[] CardsHandNeutral, GameObject gameObject)
    {
        for (int i = 0; i < CardsHandNeutral.Length; i++)
        {
            if(CardsHandNeutral[i] == null)
            {
                CardsHandNeutral[i] = gameObject;
                return;
            }
        }
    }

    public static int PositionOfGameObject(GameObject[] CardsHandNeutral, GameObject gameObject)
    {
        for (int i = 0; i < CardsHandNeutral.Length; i++)
        {
            if(CardsHandNeutral[i] == gameObject)
            return i;
        }
        return -1; // Verify this
    }

    public int PositionsNoNull (GameObject[] CardsHandeNeutral)
    {
        int n = 0;
        for (int i = 0; i < CardsHandeNeutral.Length; i++)
        {
            if(CardsHandeNeutral[i] != null)
            n++;
        }
        return n;
    }

    public static int PositionInNull(GameObject[] CardsHandNeutral)
    {
        for (int i = 0; i < CardsHandNeutral.Length; i++)
        {
            if(CardsHandNeutral[i] == null)
            return i;
        }
        return 0;
    }
   
}
    