
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;


public class Moving : MonoBehaviour
{
    /*
     Class для обработки данных ввода 
     
     */
    public static Moving Instance { get; private set; }

    private Vector3 lastMouse = new Vector3(255, 255, 255);

    

    float camSens = 0.25f;
    public IObservable <Vector2> Movement       { get; private set; }
    public IObservable <Vector2> Mouselook      { get; private set; }
    public IObservable <Vector2> MouseClickL    { get; private set; }
    public IObservable <Vector2> MouseClickR    { get; private set; }
    public IObservable <Vector2> MouseDragR     { get; private set; }
    public IObservable <Vector2> MouseDragL     { get; private set; }
    public IObservable <Vector2> CursorPos      { get; private set; }
    public IObservable <Vector2> ScrollClick    { get; private set; }
    public IObservable <Vector2> ScrollDrag     { get; private set; }


 
    public IObservable   <bool>  supportPanel   { get; private set; }
    public IObservable   <bool>  mainPanel      { get; private set; }
    public IObservable  <float>  zoomScroll     { get; private set; }
    public IObservable  <float>  yMoving        { get; private set; }

    void Awake()
    {
        Instance = this;
        Event e = Event.current;

        CursorPos = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                return 
                new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            });
        ScrollDrag = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                if (Input.GetMouseButton(2))
                {
                    Vector2 click = Input.mousePosition;
                    return click;
                }
                return Vector2.zero;
            });

        ScrollClick = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                if (Input.GetMouseButtonDown(2))
                {
                    Vector2 click = Input.mousePosition;
                    return click;
                }

                return Vector2.zero;
            });


        yMoving = this.FixedUpdateAsObservable()
            .Select(_ =>
           {
               float x = 0;
               
               x = Input.GetAxis("Mouse ScrollWheel")*6;
               if (Input.GetKey(KeyCode.E))
               {
                   x = 1;
               }
               if (Input.GetKey(KeyCode.Q))
               {
                   x = -1;
               }
               return x;
           });
                


        
        zoomScroll = this.FixedUpdateAsObservable()
            .Select(_ =>
           {
               float x = 0;
               if (Input.GetKey(KeyCode.Z))
               {
                   x = 1;
               }
               if (Input.GetKey(KeyCode.X))
               {
                   x = -1;
               }
                //print(x);
                return x;//  ("Mouse ScrollWheel");
            });
        
        Movement = this.FixedUpdateAsObservable()
            .Select(_ =>{
                var x = Input.GetAxis("Horizontal");
                var y = Input.GetAxis("Vertical");
                
                return new Vector2(x, y).normalized;
            });
        
        Mouselook = this.UpdateAsObservable()
            .Select(_ =>{
                    var x = Input.GetAxis("Mouse X");
                    var y = Input.GetAxis("Mouse Y");
                    return new Vector2(x, y);
                });
        
        MouseClickL = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 click = Input.mousePosition;
                    return click;
                }

                return Vector2.zero;
            });
        
        MouseClickR = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Vector2 click = Input.mousePosition;
                    return click;
                }

                return Vector2.zero;
            });
        
        MouseDragR = this.FixedUpdateAsObservable()
            .Select(_ =>
            {
                if (Input.GetMouseButton(1))
                {
                    Vector2 click = Input.mousePosition;
                    return click;
                }
                return Vector2.zero; 
            });

        MouseDragL = this.FixedUpdateAsObservable()
            .Select(_ =>
            {

                if (Input.GetMouseButton(0))
                {
                    Vector2 click = Input.mousePosition;
                    return click;
                }
                return Vector2.zero; 
            });

        supportPanel = this.LateUpdateAsObservable()
            .Select(_ =>
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    return true;
                }
                return false;
            });
        mainPanel = this.LateUpdateAsObservable()
            .Select(_ =>
            { 
                if (Input.GetKeyDown(KeyCode.Escape))//GetKey())
                {
                    return true;
                }
                return false;
            });
        

    }
    void Start()
    {
    
    }
    void Update()
    {
       // lastMouse = Input.mousePosition - lastMouse;
       // lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
       // lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
       // transform.eulerAngles = lastMouse;
       // lastMouse = Input.mousePosition;
    }
               
}













                

        

                
                
    
    


