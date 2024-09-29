using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SckeletonInput : MonoBehaviour
{
    GameObject controled;
    NInput input;
    public CinemachineVirtualCamera camera;
    //Light _light;



    private void OnEnable()
    {
        //_light = GetComponent<Light>();

        input = new NInput();
        controled = camera.gameObject;//this.gameObject;
        input.Enable();
        lightConfig();

        //input.Gamplay.Zoom.performed += Zooming;
        //input.Gamplay.moving.performed += Moving;
        //input.Gamplay.Rotation.performed += Rotating;
        //MeshListUpdate();
    }

    private void lightConfig()
    {
        //_light.enabled = true;
        //_light.type = LightType.Directional;
    
    }

    private void Start()
    {
        Control.Instance.OnChangePoint += PointChangin;
    }

    private void OnDestroy()
    {
        Control.Instance.OnChangePoint -= PointChangin;
    }
    private void PointChangin(GameObject obj)
    {
        camera.transform.position = new Vector3 (controled.transform.position.x, obj.transform.position.y, controled.transform.position.z);
        camera.LookAt = obj.transform;
    }

    private void Update()
    {
        Interactive();
    }

    private void Interactive()
    {
        var zom= input.Gamplay.Zoom         .ReadValue<float>(); 
        var mov= input.Gamplay.moving       .ReadValue<float>();
        var rot= input.Gamplay.Rotation     .ReadValue<float>();
    
        //controled.transform.RotateAround(transform.position, Vector3.up, rot);//new Quaternion.//Vector3(0, chang * 5, 0); ;
        controled.transform.RotateAround(camera.LookAt.position, Vector3.up, rot*0.5f);//new Quaternion.//Vector3(0, chang * 5, 0); ;
        controled.transform.Translate(0, 0, zom * 0.07f, Space.Self);
        controled.transform.Translate(0, -mov*0.07f, 0,Space.World);
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
