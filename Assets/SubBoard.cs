using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBoard : MonoBehaviour
{
    public List<GameObject> MAttack = new(7);
    public List<GameObject> RAttack = new(7);
    public List<GameObject> SAttack = new(7);
    public GameObject[] MattackPos;
    public GameObject[] RattackPos;
    public GameObject[] SattackPos;
    public bool[] maskMattack;
    public bool[] maskRattack;
    public bool[] maskSattack;
    public GameObject WeatherM;
    public bool MaskWeatherM;
    public GameObject WeatherR;
    public bool MaskWeatherR;
    public GameObject WeatherS;
    public bool MaskWeatherS;
    public GameObject AumentM;
    public bool MaskAumentM;
    public GameObject AumentR;
    public bool MaskAumentR;
    public GameObject AumentS;
    public bool MaskAumentS;
    
}
