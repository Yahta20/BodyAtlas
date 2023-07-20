using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SckeletonInput : MonoBehaviour
{
    GameObject controled;
    NInput input;
    
    


    private void OnEnable()
    {
        input = new NInput();
        controled = this.gameObject;
        input.Enable();
        //input.Gamplay.Zoom.performed += Zooming;
        //input.Gamplay.moving.performed += Moving;
        //input.Gamplay.Rotation.performed += Rotating;
        //MeshListUpdate();
    }



    /*
    private void Rotating(InputAction.CallbackContext context)
    {
        var chang = context.ReadValue<float>();
    }
    private void Zooming(InputAction.CallbackContext context)
    {
        var chang = context.ReadValue<float>();

    }
    private void Moving(InputAction.CallbackContext context)
    {
        var chang = context.ReadValue<float>();
    }
     */


    


    private void Update()
    {
        Interactive();
    }

    private void Interactive()
    {
        var zom= input.Gamplay.Zoom      .ReadValue<float>(); 
        var mov= input.Gamplay.moving    .ReadValue<float>();
        var rot= input.Gamplay.Rotation.ReadValue<float>();
    
        controled.transform.RotateAround(transform.position, Vector3.up, rot);//new Quaternion.//Vector3(0, chang * 5, 0); ;
        controled.transform.Translate(0, mov*0.1f, zom*0.1f,Space.World);
        //controled.transform.Translate()

    }
    private void OnDisable()
    {
        input.Disable();
        //input.Gamplay.Zoom.performed -= Zooming;
        //input.Gamplay.moving.performed -= Moving;
        //input.Gamplay.Rotation.performed -= Rotating;
    }
}
