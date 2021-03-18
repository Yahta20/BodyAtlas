using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class TestManager : MonoBehaviour
{
    public static TestManager Instance;

    public bool[] stepOfTest = new bool[10];
    public enum TypeOfTest {
        Chousing = 0,
        Random = 1,
        Bones = 2,
        Points = 3,
        Finish = 4
    }

    TypeOfTest currentState;

    void Awake()
    {
        currentState = TypeOfTest.Chousing;
    }








    void Start()
    {
        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {

                switch (currentState)
                {
                    case TypeOfTest.Chousing:

                        break;
                    case TypeOfTest.Random:
                        
                        break;
                    case TypeOfTest.Bones:
                        
                        break;
                    case TypeOfTest.Points:
                        
                        break;
                    case TypeOfTest.Finish:
                        
                        break;
                }


            })
            .AddTo(this);
    }

    public void setState(TypeOfTest l) => currentState = l;

















}