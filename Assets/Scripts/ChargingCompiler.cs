using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChargingCompiler : MonoBehaviour
{
    public static ChargingCompiler chargingCompilerInstance;
    public Canvas canvas;
    public GameObject InterFaceCompiler;
    public GameObject InterfaceAssigment;
    public TMP_Text Text;
    [SerializeField] static Queue<GameObject> compiledCards = new();

    public TMP_InputField input;
    
    void Start() {

        chargingCompilerInstance = this;
    }

     public void OnClick(){
        
        compiledCards = CardsCreator.CardCreator(input.text);
        InterFaceCompiler.SetActive(false);
        if(compiledCards.Count > 0){Text.text = GivingName(compiledCards.Peek());}
        else{InterfaceAssigment.SetActive(false);}
        
    }

    private string GivingName(GameObject gameObject)
    {
        //GameObject instace = GameObject.Instantiate(gameObject,)
        if(gameObject.TryGetComponent(out WeatherCardScript card))
        return gameObject.GetComponent<WeatherCardScript>().weatherCard.NameCard;

        if(gameObject.TryGetComponent(out AumentCardScript card1))
        return gameObject.GetComponent<AumentCardScript>().aumentCard.NameCard;

        throw new Exception("Invalid Card");
    }

    // public void ChangingToAssigmentUI(){
    //     InterFaceCompiler.SetActive(false);
    // }

    public void AssassinAdd(){
        Portal("Assassins");
    }

    public void TemplarsAdd(){

       Portal("TemplarsDeck");
    }

    public void Portal(string   tag){
        GameObject test = Instantiate(compiledCards.Dequeue(),new Vector3(1000,1000,100),Quaternion.identity);
        GameObject gameObject = GameObject.FindGameObjectWithTag(tag);
        gameObject.GetComponent<DeckScript>().deck.Add(test);
        if(compiledCards.Count == 0) {
        InterfaceAssigment.SetActive(false);
            return;
        }
        Text.text = GivingName(compiledCards.Peek());
    }

    
}
