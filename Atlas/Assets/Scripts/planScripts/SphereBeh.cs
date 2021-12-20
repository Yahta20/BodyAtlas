using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

enum AccesState { 
    free=0,
    locked=1,
}
public class SphereBeh : MonoBehaviour
{
    private AccesState currentState;
    public static SphereBeh Instance { get; private set; }

    public float speedmov;
    public float mouseSpeed;

    public Vector3 clicPlace;
    public Vector3 ScrollclicPlace;

    public Transform startCameraPlase;

    public GameObject soloObject;
    public List<GameObject> groupObject;

    public Camera camera;
    public GameObject controlObject;

    public string[] GameMenState = new string[4];
    public Vector3 offset;

    [Space]
    public Moving movi;
    [Range(-90, 0)]
    public float minViewAngle = -60f; // How much can the user look down (in degrees)
    [Range(0, 90)]
    public float maxViewAngle = 60f;
    [Range(25, 60)]
    public float cameraMaxZoom = 25f;
    [Range(25, 60)]
    public float cameraMinZoom = 60f;
    private Vector3 velocity= Vector3.one;

    void Awake()
    {
        Instance = this;
        startCameraPlase = transform;
        currentState = AccesState.free;
        camera = GetComponent<Camera>();
    }

    void Start()
    {
        
        movi = Moving.Instance;

        movi.Movement
            .Where(v => v!= Vector2.zero)
            .Subscribe(input => {
                Movement(input);
            }).AddTo(this);

        movi.zoomScroll
            .Where(v => v != 0)
            .Subscribe(sub =>
            {
                var xPassLine = Screen.width-rightPanel.Instance.rtPanel.sizeDelta.x;
                var fov = camera.fieldOfView;
                
                if (xPassLine > Input.mousePosition.x 
                     |!rightPanel.Instance.state
                ) { 

                    fov -= sub*0.2f;
                    fov = fov > cameraMinZoom ? cameraMinZoom : fov;
                    fov = fov < cameraMaxZoom ? cameraMaxZoom : fov;
                }
                camera.fieldOfView = fov;
            }).AddTo(this);

        movi.MouseClickR
           .Where(v => v != Vector2.zero)
           .Subscribe(s => {
                clicPlace = s;
           })
           .AddTo(this);

        movi.MouseDragR
            .Where(v=>v!=Vector2.zero)
            .Subscribe(s=> {
                var currentPos = s;
                var deltaX = currentPos.x - clicPlace.x;
                var deltaY = currentPos.y - clicPlace.y;
                if (ClassroomBeh.Instance.chosenObj.name == "empty")
                {
                    RotationByPress(
                        new Vector2(deltaY, deltaX)
                        );
                }
                if (ClassroomBeh.Instance.chosenObj.name != "empty")
                {
                    RotateBone(new Vector2(deltaX, deltaY));
                    //print("ewfweefwef"+ deltaX+"|"+deltaY);
                }
                clicPlace = s;
            })
            .AddTo(this);

        movi.yMoving
            .Where(y => y != 0)
            .Subscribe(s => {
                var ySpeed = s * Vector3.forward * Time.deltaTime * speedmov;
                transform.Translate(ySpeed);
            })
            .AddTo(this);


        movi.ScrollClick
            .Where(sp=> sp != Vector2.zero)
            .Subscribe(s => {
                ScrollclicPlace = s;
            })
            .AddTo(this);
     
        movi.ScrollDrag
            .Where(v => v != Vector2.zero)
            .Subscribe(s => {
                var currentPos = s;
                var deltaX = currentPos.x - ScrollclicPlace.x;
                var deltaY = currentPos.y - ScrollclicPlace.y;
                Movement(new Vector2(-deltaX, -deltaY));
                ScrollclicPlace = s;
            })
            .AddTo(this);

        var chosen = Observable.EveryFixedUpdate()
            .Subscribe(_=>chekState()).AddTo(this);

        var loced = Observable.EveryLateUpdate()
            .Subscribe(_ => lockState()).AddTo(this);


    }

    private void lockState()
    {
        if (ClassroomBeh.Instance == null)
        {
            return;
        }
        else
        {
            controlObject = ClassroomBeh.Instance.gameObject;
        }
        if (currentState == AccesState.locked)
        {
            if (GameMenState[1] == "" & GameMenState[2] == "" & GameMenState[3] == "") {
                var bounds = new Bounds(controlObject.transform.position,Vector3.one);
                offset = new Vector3(6.6f, 12, -42f);
                var newpoint = offset;
                
                gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position,
                    newpoint,
                    ref velocity,
                    0.3f
                    );
                gameObject.transform.rotation = Quaternion.identity;
                if (compareRange(gameObject.transform.position, newpoint,2))
                {

                    gameObject.transform.position = newpoint;
                    currentState = AccesState.free;
                }
                groupObject = new List<GameObject>();
            }
                    
            if (GameMenState[1]!="" & GameMenState[0] != ""&GameMenState[2] == "" & GameMenState[3] == "")
            {
                var bonelist =  GameManager.Instance.anatomy.getItemlist(GameMenState[1]);
                groupObject = ClassroomBeh.Instance.getChosenGroup(bonelist);
                var bounds = new Bounds(groupObject[0].transform.position, Vector3.one);
                for (int i = 1; i < groupObject.Count; i++)
                {
                    bounds.Encapsulate(groupObject[i].transform.position);
                }
                //print(bounds.center);
                offset = new Vector3(0, 0, -18);
                var newpoint = offset+bounds.center;
                gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position,
                    newpoint,
                    ref velocity,
                    0.3f
                    );
                gameObject.transform.LookAt(bounds.center);
                if (compareRange(gameObject.transform.position, newpoint, 2))
                {

                    gameObject.transform.position = newpoint;
                    currentState = AccesState.free;
                }

            }
            if (GameMenState[1] != "" & GameMenState[0] != "" & GameMenState[2] != "" & GameMenState[3] == "")
            {
                soloObject = ClassroomBeh.Instance.getSelectedObj(GameMenState[2]);
                var bounds = new Bounds(soloObject.transform.position, Vector3.one);
                offset = new Vector3(0, 0, -8f);
                var newpoint = offset + bounds.center;
                
                gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position,
                    newpoint,
                    ref velocity,
                    0.3f
                    );
                gameObject.transform.LookAt(bounds.center);
                if (compareRange(gameObject.transform.position, newpoint, 2))
                {
                
                    gameObject.transform.position = newpoint;
                }
                currentState = AccesState.free;
            }
        }
    }
                



    [ContextMenu("pos")]
    private void Propor() {
        var bounds = new Bounds(controlObject.transform.position, Vector3.one);
        Vector3 viewPos = camera.WorldToScreenPoint(controlObject.transform.position);
        Vector3 viewPo = camera.WorldToViewportPoint(controlObject.transform.position);
        print($"bounds>{bounds.size}/bounds>{bounds.center}?");
        print($"viewPos>{viewPos}?viewPo>{viewPo}");

    }

    private void chekState() {
        
        if (!compareLists())
        {
            currentState = AccesState.locked;
            GameMenState = GameManager.Instance.getState();
        }
    }

    private bool compareLists() {
        var gstate = GameManager.Instance.getState();
        for (int q = 0; q < gstate.Length; q++)
        {
            if (gstate[q]!=GameMenState[q])
            {
                return false;
            }
        }
        return true;
    }

    private bool compareRange(Vector3 v1, Vector3 v2, int acc) {
        var resolt = false;

        if (
                (int)(v1.x * Mathf.Pow(10, acc))  == (int)(v1.x * Mathf.Pow(10, acc))
            &   (int)(v1.y * Mathf.Pow(10, acc))  == (int)(v1.y * Mathf.Pow(10, acc))
            &   (int)(v1.z * Mathf.Pow(10, acc))  == (int)(v1.z * Mathf.Pow(10, acc)))
                {
            return true;
        }
        return false;
    }

    private void RotateBone(Vector2 v)
    {
        ClassroomBeh.Instance.chosenObj.transform.Rotate(new Vector3(v.y, -v.x, 0), Space.World);
    }

    private void RotationByPress(Vector2 inputVector) {

        transform.RotateAround(controlObject.transform.position, Vector3.up*inputVector.y, Time.deltaTime * 50);

        transform.rotation = 
            Quaternion.Euler
            (transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,0);

    } 
    public void Movement(Vector2 input)
    {
        var inputVelocity = input * speedmov;
        var x = inputVelocity.x;
        var z = inputVelocity.y * Vector3.up;//

        var playerVelocity = //new Vector3(x, 0, z.z);
        inputVelocity.x * this.gameObject.transform.right + // x (+/-) corresponds to strafe right/left
        inputVelocity.y * this.gameObject.transform.up; // y (+/-) corresponds to forward/back

        var distance = playerVelocity * Time.fixedDeltaTime;
        transform.Translate(distance, Space.World);
    }

}
