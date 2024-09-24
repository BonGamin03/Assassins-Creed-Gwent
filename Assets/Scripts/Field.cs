using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Field 
{
    public List<GameObject> MAttack{get; set;}
    public List<GameObject> RAttack{get;set;}
    public List<GameObject> SAttack{get;set;}
    public GameObject[] MattackPos{get;set;}
    public GameObject[] RattackPos{get;set;}
    public GameObject[] SattackPos{get;set;}
    public bool[] MaskMattack{get;set;}
    public bool[] MaskRattack{get;set;}
    public bool[] MaskSattack{get;set;}
    public GameObject WeatherM{get;set;}
    public bool MaskWeatherM{get;set;}
    public GameObject WeatherR{get;set;}
    public bool MaskWeatherR{get;set;}
    public GameObject WeatherS{get;set;}
    public bool MaskWeatherS{get;set;}
    public GameObject AumentM{get;set;}
    public bool MaskAumentM{get;set;}
    public GameObject AumentR{get;set;}
    public bool MaskAumentR{get;set;}
    public GameObject AumentS{get;set;}
    public bool MaskAumentS{get;set;}

    public List<GameObject> AllCards{
        get => MAttack.Concat(RAttack).Concat(SAttack).ToList();
    }

    public Field(List<GameObject> mAttack, List<GameObject> rAttack, List<GameObject> sAttack, bool[] maskMattack, bool[] maskRattack, bool[] maskSattack, GameObject weatherM, bool maskWeatherM, GameObject weatherR, bool maskWeatherR, GameObject weatherS, bool maskWeatherS, GameObject aumentM, bool maskAumentM, GameObject aumentR, bool maskAumentR, GameObject aumentS, bool maskAumentS)
    {
        MAttack = mAttack;
        RAttack = rAttack;
        SAttack = sAttack;
        MaskMattack = maskMattack;
        MaskRattack = maskRattack;
        MaskSattack = maskSattack;
        WeatherM = weatherM;
        MaskWeatherM = maskWeatherM;
        WeatherR = weatherR;
        MaskWeatherR = maskWeatherR;
        WeatherS = weatherS;
        MaskWeatherS = maskWeatherS;
        AumentM = aumentM;
        MaskAumentM = maskAumentM;
        AumentR = aumentR;
        MaskAumentR = maskAumentR;
        AumentS = aumentS;
        MaskAumentS = maskAumentS;
    }

    public void Remove( GameObject card)
    {
        Vector3 vector = new Vector3(1000,1000,1000);
        if(MAttack.Find(X => X.name.Equals(card.name))){
            MAttack.Remove(card);
        }else if(RAttack.Find(X => X.name.Equals(card.name))){
            RAttack.Remove(card);
        }else if(SAttack.Find(X => X.name.Equals(card.name))){
            SAttack.Remove(card);
        }

        card.transform.position = vector;
    }
}
