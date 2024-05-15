using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonChange : MonoBehaviour
{
    public GameObject GameManager;
    public bool PassAssassinsWithoutPlay;
    public bool PassTemplarsWihtoutPlay;

    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("Game Manager");
    }

    public void OnClick()
    {
        if(GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin && GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
        {
            GameManager.GetComponent<GameManajer>().CounterPassRound ++;
            if(GameManager.GetComponent<GameManajer>().AssassinPlay)
            { 
                PassAssassinsWithoutPlay = true;
                GameManager.GetComponent<GameManajer>().AssassinPlay = false;
                GameManager.GetComponent<GameManajer>().TemplarsPlay = true;
            }else{
                PassTemplarsWihtoutPlay = true;
                GameManager.GetComponent<GameManajer>().AssassinPlay = true;
                GameManager.GetComponent<GameManajer>().TemplarsPlay = false;
            }
        }
        if(GameManager.GetComponent<GameManajer>().AssassinPlay && !GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin)
        {
            GameManager.GetComponent<GameManajer>().AlreadyChangedAssassin = true;
        }
        if(GameManager.GetComponent<GameManajer>().TemplarsPlay && !GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar)
        {
            GameManager.GetComponent<GameManajer>().AlreadyChangedTemplar = true;
        }

    }
}
