using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choseButBeh : MonoBehaviour
{
    
    private GameState gs;
    
    public void setGameState(GameState s) { gs = s; }

    public void transferData() {
        GameEnviroment.Singelton.setGameState(gs);

        if (GameEnviroment.Singelton.currentType == GameType.Atlas)
        {
            StartMenuBeh.Instance.ClasroomScene();
        }

        if (GameEnviroment.Singelton.currentType == GameType.Exam)
        {
            StartMenuBeh.Instance.langChose();
        }
    }

    //void Start()
    //{
    //}
    //void Update()
    //{
    //}
    
}
