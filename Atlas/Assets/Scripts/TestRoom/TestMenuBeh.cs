using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMenuBeh : MonoBehaviour
{
    public static TestMenuBeh Instance;

    public RectTransform rtPanel;
    public List<GameObject> infoPanels;

    public Text MainText;
    public Text Description;


    enum StateOfMenu { 
        start=0,
        choseBone=1,
        exit=2
    }

    StateOfMenu currentState;

    void Awake()
    {
        Instance = this; 
        rtPanel = GetComponent<RectTransform>();
        currentState=StateOfMenu.start;
    }
    private void Start()
    {
        
    }


}
