using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class StateUI : MonoBehaviour
{

    public List <MonoBehaviour> list;
    StateMashine curMashine;

    void Start()
    {
        curMashine = new();
        curMashine.AddState(new FChose(curMashine, list[0] as ChosePanelB));
        curMashine.AddState(new FAudit(curMashine, 
            list[0] as ChosePanelB,
            list[1] as AuditPanelB,
            list[2] as ResultPanelB
            ));

        foreach (var item in list) { 
            item.gameObject.SetActive(false);
        }

        curMashine.SetState<FChose>();
    }


    // Update is called once per frame
    void Update()
    {
        curMashine.Update();
    }
}
