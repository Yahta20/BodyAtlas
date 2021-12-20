using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LoadingLogo : MonoBehaviour
{
    public Image logoIn;
    public Image logoOut;

    // Start is called before the first frame update
    void Start()
    {
        logoIn = GetComponent<Image>();
        var boneUpdate = Observable.EveryLateUpdate()
            .Subscribe(
            s => {
                buildvision();
                

            })
            .AddTo(this);
    }

    private void buildvision()
    {
        var logoORT =logoOut.rectTransform;
        var logoIRT = logoIn.rectTransform;

        logoORT.sizeDelta = new Vector2(logoIRT.sizeDelta.x * 0.428f, 
                                        logoIRT.sizeDelta.y * 0.571f);
        logoORT.anchoredPosition = new Vector2(0,-(logoIRT.sizeDelta.y*0.0325f));
        logoORT.Rotate(0, 3, 0); // Quaternion.EulerRotation(0, 3, 0);
        logoOut.color = new Color(logoOut.color.r, logoOut.color.g, logoOut.color.b, logoIn.color.a); 
        
    }
}
