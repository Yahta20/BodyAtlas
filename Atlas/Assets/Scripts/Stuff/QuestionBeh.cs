using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class QuestionBeh : MonoBehaviour
{
    public Image back;
    public Text name;


    void Start()
    {
        var testManager = TestManager.Instance;

        back.OnPointerDownAsObservable().
            Subscribe(s => {
                testManager.setAnswer(name.text);
            }).
            AddTo(this);
    }

    public void setName(string s) {
        name.text = s;
    }
}
