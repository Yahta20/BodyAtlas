using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomBeh : MonoBehaviour
{
<<<<<<< Updated upstream
=======
    public static ClassroomBeh Instance;

    public Material regularMat;
    public Material chosenMat;
    public Material backgroundMat;

    public GameObject chosenObj { get; private set; }
    private GameObject emptyObj;
    public List<BoneBih> objOnScene;

    public IObservable <bool> isChosenObject {get; private set;}

    //public BoneBih just;
>>>>>>> Stashed changes
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
