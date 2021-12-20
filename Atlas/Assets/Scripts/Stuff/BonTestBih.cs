
using UnityEngine;
using UniRx;


public class BonTestBih : MonoBehaviour
{
    public GameObject PupetBone;
    
    [Range(0, 5)]
    public float speedMoving=0.02f;
    [Range(25, 60)]
    public float cameraMaxZoom = 25f;
    [Range(25, 60)]
    public float cameraMinZoom = 60f;
    public Camera camera;

    public Vector3 clicPlaceL;
    public Vector3 clicPlaceS;

    void Awake()
    {
        if (camera == null) {
            camera = FindObjectOfType<Camera>();
        }
    }

    void Start()
    {
        var movi = Moving.Instance;


       movi.MouseClickR.
            Where(w => w != Vector2.zero).
            Subscribe(s=> {
                clicPlaceL = s;
            }).
            AddTo(this);

        movi.MouseDragR.
            Where(w => w != Vector2.zero).
            Subscribe(s => {
                var currentPos = s;
                var deltaX = currentPos.x - clicPlaceL.x;
                var deltaY = currentPos.y - clicPlaceL.y;
                    RotationByPress(
                        new Vector2(deltaX, deltaY)
                        );
                clicPlaceL = s;
            }).
            AddTo(this);
        /*
         */

        movi.ScrollClick
            .Where(sp => sp != Vector2.zero)
            .Subscribe(s => {
                clicPlaceS = s;
            })
            .AddTo(this);

        movi.ScrollDrag
            .Where(v => v != Vector2.zero)
            .Subscribe(s => {
                var currentPos = s;
                var deltaX = currentPos.x - clicPlaceS.x;
                var deltaY = currentPos.y - clicPlaceS.y;
                Movement(new Vector2(deltaX, deltaY));
                clicPlaceS = s;
            })
            .AddTo(this);

        movi.yMoving
                .Where(v => v != 0)
                .Subscribe(sub =>
                {
                    var xPassLine = Screen.width;//- rightPanel.Instance.rtPanel.sizeDelta.x;// Input.mousePosition.x;
                    var fov = camera.fieldOfView;

                    //print(sub);
                    //if (xPassLine > Input.mousePosition.x
                    //     | !rightPanel.Instance.state
                    //)
                    //{

                        fov -= sub * 0.33f;
                        fov = fov > cameraMinZoom ? cameraMinZoom : fov;
                        fov = fov < cameraMaxZoom ? cameraMaxZoom : fov;
                    //}
                    camera.fieldOfView = fov;
                }).AddTo(this);



        //print(s);

    }

    public void Movement(Vector2 input)
    {
        var inputVelocity = input * speedMoving;
        

        var playerVelocity = //new Vector3(x, 0, z.z);
        inputVelocity.x * this.gameObject.transform.right + // x (+/-) corresponds to strafe right/left
        inputVelocity.y * this.gameObject.transform.up; // y (+/-) corresponds to forward/back


        var distance = playerVelocity * Time.fixedDeltaTime;
        //transform.Translate(distance, Space.World);

        transform.position = transform.position+(Vector3)inputVelocity;
    }


    private void RotationByPress(Vector2 inputVector)
    {
        transform.Rotate(new Vector3(inputVector.y, -inputVector.x, 0),Space.World);

        //var curr = transform.rotation;
        //transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, transform.rotation * Quaternion.Euler(-inputVector.y, -inputVector.x, 0), Time.deltaTime * speedRotat);
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
    }
}
