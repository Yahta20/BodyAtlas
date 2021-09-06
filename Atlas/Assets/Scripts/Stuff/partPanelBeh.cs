using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class partPanelBeh : MonoBehaviour
{


    public RectTransform contentRT;
    public GameObject buttonPrefab;

    public List<GameObject> lgo = new List<GameObject>();


    void Start()
    {
        
        for (GameState gs = GameState.Hand; gs < GameState.End; gs++)
        {
            var go = Instantiate(buttonPrefab);
            
            lgo.Add(go);

            go.transform.GetChild(0).GetComponent<TMP_Text>().text= gs.ToString();
            
            go.GetComponent<choseButBeh>().setGameState(gs);

            //var we = go.transform.GetChild(0).GetComponent<TMP_Text>();
            //var str= gs.ToString();
            //we.text = str;
            
            go.transform.SetParent(contentRT);
        }
            
    }

    void Update()
    {
        
    }
}
