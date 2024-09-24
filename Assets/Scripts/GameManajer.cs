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

public class GameManajer : MonoBehaviour 
{
    public static GameManajer GameManger;
    public Field AssassinField;
    public Field TemplarField;
    public GameObject HandPlayerAssassin;
    public GameObject HandPlayerTemplar;
    public GameObject Templarsdeck;
    public GameObject Assassinsdeck;
    public List<GameObject> CardsHandAssassin;
    public List<GameObject> CardsHandTemplar;
    public GameObject Board; 
    public Sprite AssassinsCardsBack;
    public Sprite TemplarsCardsBack;
    public bool AssassinPlay;
    public bool TemplarsPlay;
    public  int AssassinPoints;
    public  int TemplarPoints;
    public  int AssassinRoundWins;
    public  int TemplarRoundWins;
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

    public Player AssassinPlayer { get; internal set; }
    public Player TemplarPlayer { get; internal set; }
    public Hand AssassinHand { get; private set; }
    public Hand TemplarHand { get; private set; }
    public GameObject TemplarsGraveyard { get; private set; }
    public GameObject AssassinsGraveyard { get; private set; }

    void Start()
    {   //Falta crear la instancia de los cementerios para pasarselos a los players
      

        GameManger = this; //Cuidado con esto 
        AssassinPlay             = true;
        Assassinsdeck            = GameObject.FindGameObjectWithTag("Assassins");
        Templarsdeck             = GameObject.FindGameObjectWithTag("TemplarsDeck");
        Board                    = GameObject.FindGameObjectWithTag("board");
      
        var board = Board.GetComponent<BoardScript>();

        AssassinField = new(board.AssassinsMAttack,board.AssassinsRAttack,board.AssassinsSAttack,board.maskAssassinMattack,board.maskAssassinRattack,board.maskAssassinSattack,board.AssassinsWeatherM,board.MaskAssassinsWeatherM,board.AssassinsWeatherR,board.MaskAssassinsWeatherR,board.AssassinsWeatherS,board.MaskAssassinsWeatherS, board.AssassinsAumentM, board.MaskAssassinsAumentM, board.AssassinsAumentR,board.MaskAssassinsAumentR,board.AssassinsAumentS,board.MaskAssassinsAumentS);
        TemplarField = new(board.TemplarsMAttack, board.TemplarsRAttack, board.TemplarSAttack,board.maskTemplarMattack,board.maskTemplarRattack,board.maskTemplarSattack,board.TemplarsWeatherM,board.MaskTemplarsWeatherM,board.TemplarsWeatherR,board.MaskTemplarsWeatherR,board.TemplarsWeatherS,board.MaskTemplarsWeatherS, board.TemplarsAumentM, board.TemplarsAumentM, board.TemplarsAumentR,board.MaskTemplarsAumentR,board.TemplarsAumentS,board.MaskTemplarsAumentS);
        AssassinPlayer = new (1, AssassinPlay, new Hand(CardsHandAssassin, PositionHandAssassin, HandPlayerAssassin, maskPositionHandAssassin), AssassinField, Assassinsdeck, AssassinsGraveyard);
        TemplarPlayer = new (2, TemplarsPlay,  new Hand(CardsHandTemplar, PositionHandTemplar, HandPlayerTemplar, maskPositionHandTemplar), TemplarField, Templarsdeck, TemplarsGraveyard);

        for (int i = 0; i < 10; i++)
        {
            int RandomIndexCardAssassin = UnityEngine.Random.Range(0,Assassinsdeck.GetComponent<DeckScript>().deck.Count);
            int RandomIndexCardTemplar = UnityEngine.Random.Range(0,Templarsdeck.GetComponent<DeckScript>().deck.Count);

            GameObject drawCardAssassin = Instantiate(Assassinsdeck.GetComponent<DeckScript>().deck[RandomIndexCardAssassin],PositionHandAssassin[i].transform.position,UnityEngine.Quaternion.identity);
            drawCardAssassin.transform.SetParent(HandPlayerAssassin.transform,false);
            drawCardAssassin.transform.position = PositionHandAssassin[i].transform.position;
            Assassinsdeck.GetComponent<DeckScript>().RemoveAt(RandomIndexCardAssassin);
            AddCardToHand(CardsHandAssassin,drawCardAssassin);
            maskPositionHandAssassin[i] = true;

            GameObject drawCardTemplar = Instantiate(Templarsdeck.GetComponent<DeckScript>().deck[RandomIndexCardTemplar],PositionHandTemplar[i].transform.position,Templarsdeck.transform.rotation);
            drawCardTemplar.transform.SetParent(HandPlayerTemplar.transform, false);
            drawCardTemplar.transform.position = PositionHandTemplar[i].transform.position;
            Templarsdeck.GetComponent<DeckScript>().RemoveAt(RandomIndexCardTemplar);
            AddCardToHand(CardsHandTemplar,drawCardTemplar);
            maskPositionHandTemplar[i] = true;
        }

    }

    // Update is called once per frame Vector3(-216,91,0)
    void Update()
    {
        if(AssassinRoundWins == 2 && TemplarRoundWins != 2)
        {
            AssassinPlay = true;
            AssassinInterfaceOfWin.SetActive(true);
            Board.SetActive(false);
            AssassinPointsText.gameObject.transform.localScale = new UnityEngine.Vector3(0,0,0);
            TemplarsPointsText.gameObject.transform.localScale = new UnityEngine.Vector3(0,0,0);
        }
        if(TemplarRoundWins == 2 && AssassinRoundWins != 2)
        {
            AssassinPlay = true;
            TemplarInterfaceOfWin.SetActive(true);
            Board.SetActive(false);
            AssassinPointsText.gameObject.transform.localScale = new UnityEngine.Vector3(0,0,0);
            TemplarsPointsText.gameObject.transform.localScale = new UnityEngine.Vector3(0,0,0);
        }
        if(TemplarRoundWins == 2 && AssassinRoundWins == 2)
        {
            Board.SetActive(false);
            Debug.Log("Empate");
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
                        GameObject drawCardAssassin = Instantiate(Assassinsdeck.GetComponent<DeckScript>().deck[Assassinsdeck.GetComponent<DeckScript>().deck.Count-2],PositionHandAssassin[j].transform.position,UnityEngine.Quaternion.identity);
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

             if(TemplarPoints == AssassinPoints)
            {
                TemplarRoundWins++;
                AssassinRoundWins++;
                AssassinPlay= true;
                TemplarsPlay= false;
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

     public static bool IsInHand(List<GameObject> TheHand, GameObject WeatherCard)
    {
        for (int i = 0; i < TheHand.Count; i++)
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

    public static void AddCardToHand(List<GameObject> CardsHandNeutral, GameObject gameObject)
    {
        for (int i = 0; i < CardsHandNeutral.Count; i++)
        {
            if(CardsHandNeutral[i] == null)
            {
                CardsHandNeutral[i] = gameObject;
                return;
            }
        }
    }

    public static int PositionOfGameObject(List<GameObject> CardsHandNeutral, GameObject gameObject)
    {
        for (int i = 0; i < CardsHandNeutral.Count; i++)
        {
            if(CardsHandNeutral[i] == gameObject)
            return i;
        }
        return 0; // Verify this
    }

    public int PositionsNoNull (List<GameObject> CardsHandeNeutral)
    {
        int n = 0;
        for (int i = 0; i < CardsHandeNeutral.Count; i++)
        {
            if(CardsHandeNeutral[i] != null)
            n++;
        }
        return n;
    }

    public static int PositionInNull(List<GameObject> CardsHandNeutral)
    {
        for (int i = 0; i < CardsHandNeutral.Count; i++)
        {
            if(CardsHandNeutral[i] == null)
            return i;
        }
        return 0;
    }
   
//    public static void StartSimulator(GameObject gameObject)
//    {
//       Board        = GameObject.FindGameObjectWithTag("board");
//       ZoomAssassin = GameObject.FindGameObjectWithTag("Zoom Assassin");
//       ZoomTemplar = GameObject.FindGameObjectWithTag("Zoom Templar");
//       GameManager  = GameObject.FindGameObjectWithTag("Game Manager");
//    }
}
    