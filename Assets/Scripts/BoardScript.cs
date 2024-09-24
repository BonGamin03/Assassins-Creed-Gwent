using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;


public class BoardScript : MonoBehaviour {
        
        public static BoardScript InsBoard;
        public List<GameObject> AssassinsMAttack = new(7);
        public List<GameObject> TemplarsMAttack = new(7);
        public List<GameObject> AssassinsRAttack = new(7);
        public List<GameObject> TemplarsRAttack = new(7);
         public List<GameObject> AssassinsSAttack = new(7);
        public List<GameObject> TemplarSAttack = new(7); 
        public GameObject[] AssassinMattackPos;
        public GameObject[] AssassinRattackPos;
        public GameObject[] AssassinSattackPos;
        public bool[] maskAssassinMattack;
        public bool[] maskAssassinRattack;
        public bool[] maskAssassinSattack;
        public GameObject[] TemplarMattackpos;
        public GameObject[] TemplarRattackpos;
        public GameObject[] TemplarSattackpos;
        public bool[] maskTemplarMattack;
        public bool[] maskTemplarRattack;
        public bool[] maskTemplarSattack;


        // Weather Cards Fields

        public GameObject AssassinsWeatherM;
        public bool MaskAssassinsWeatherM;
        public GameObject AssassinsWeatherR;
        public bool MaskAssassinsWeatherR;
        public GameObject AssassinsWeatherS;
        public bool MaskAssassinsWeatherS;
        
        public GameObject TemplarsWeatherM;
        public bool MaskTemplarsWeatherM;
        public GameObject TemplarsWeatherR;
        public bool MaskTemplarsWeatherR;
        public GameObject TemplarsWeatherS;
        public bool MaskTemplarsWeatherS;
        public List<GameObject> WeatherAssassins;
        public List<GameObject> WeatherTemplars;
        
        //Aument Cards Fields 
        public GameObject AssassinsAumentM;
        public bool MaskAssassinsAumentM;
        public GameObject AssassinsAumentR;
        public bool MaskAssassinsAumentR;
        public GameObject AssassinsAumentS;
        public bool MaskAssassinsAumentS;

        public GameObject TemplarsAumentM;
        public bool MaskTemplarsAumentM;
        public GameObject TemplarsAumentR;
        public bool MaskTemplarsAumentR;
        public GameObject TemplarsAumentS;
        public bool MaskTemplarsAumentS;
        public List<GameObject> AumentAssassins;
        public List<GameObject>AumentTemplars;


        void Start(){
                InsBoard = this;
                
        }
      
}
