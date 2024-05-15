using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public Camera AssassinsView;
    public Camera TemplarsView;
    private Camera CurrentCamera;
    private GameObject GameManager;
    void Start()
    {
       GameManager = GameObject.FindGameObjectWithTag("Game Manager");
       CurrentCamera = AssassinsView;
       AssassinsView.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GetComponent<GameManajer>().AssassinPlay)
        {
        
            CurrentCamera = AssassinsView;
            AssassinsView.gameObject.SetActive(true);
            TemplarsView.gameObject.SetActive(false);
        }else
        {
            CurrentCamera = TemplarsView;
            AssassinsView.gameObject.SetActive(false);
            TemplarsView.gameObject.SetActive(true);
        }
    }
}
