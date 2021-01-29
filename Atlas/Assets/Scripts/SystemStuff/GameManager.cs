using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Lang currentLang { get; private set;}
    public Lang UiLang { get; private set; }


    void Awake()
    {
        currentLang = Lang.lat;
        GameEnviroment.Singelton.setLanguage(currentLang);
    }

    
}
