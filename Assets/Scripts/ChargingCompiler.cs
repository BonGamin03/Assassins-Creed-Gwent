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
        //InterFaceCompiler = GameObject.FindGameObjectWithTag("CompilingUI");//creo que no son necesarios
        //InterfaceAssigment = GameObject.FindGameObjectWithTag("Assigning");//
        // CompiledCards = GameObject.FindGameObjectWithTag("CompiledCards");
    }

     public void OnClick(){
        
        compiledCards = CardsCreator.CardCreator(input.text);
        InterFaceCompiler.SetActive(false);
        if(compiledCards.Count > 0){Text.text = compiledCards.Peek().name;}
        else{InterfaceAssigment.SetActive(false);}
        
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

    public void Portal(string tag){
        GameObject test = Instantiate(compiledCards.Dequeue(),new Vector3(1000,1000,100),Quaternion.identity);
        GameObject gameObject = GameObject.FindGameObjectWithTag(tag);
        gameObject.GetComponent<DeckScript>().deck.Add(test);
        if(compiledCards.Count == 0) {
        InterfaceAssigment.SetActive(false);
            return;
        }
        Text.text = compiledCards.Peek().name;
    }
}
