using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public enum StateOfLoading { 
    hide=0,
    show=1
}

public class LoadScreen : MonoBehaviour
{
    public static LoadScreen Instance;

    public Image Backgrond;
    public Text FonText;
    public Image Loaing;
    //public Image LoaingIn;

    StateOfLoading currentstate;
    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        currentstate = StateOfLoading.show;
    }
    void Start()
    {
        
        Backgrond = GetComponent<Image>();
        var screan = UIManager.Instance.screenSize.
            Where(w => w != Vector2.zero).
            Subscribe(s => {
                updateFoo(s);
                stateChek();
            }).
            AddTo(this);
            
    }

    private void stateChek()
    {
        if (currentstate == StateOfLoading.show)
        {
            gameObject.SetActive(true);
            float acol = Backgrond.color.a;
            acol+=0.3f;
            acol = acol > 255 ? 255 : acol;
            Backgrond.color = new Color(Backgrond.color.r, Backgrond.color.g, 
                Backgrond.color.b, 255);
        }
        if (currentstate == StateOfLoading.hide)
        {
            float acol = Backgrond.color.a;
            //acol--;
            acol -= 0.3f;
            acol = acol < 0 ? 0 : acol;
            Backgrond.color = new Color(Backgrond.color.r, Backgrond.color.g,
                Backgrond.color.b, acol);
            if (acol<=0)
            {
                gameObject.SetActive(false);
            }

        }
    }

    private void updateFoo(Vector2 s)
    {
        Backgrond.rectTransform.sizeDelta = s;
        Backgrond.rectTransform.anchoredPosition = Vector2.zero;

        FonText.rectTransform.sizeDelta = new Vector2(
            (s.x * 0.38f)   *   0.38f,
            (s.y*0.38f)     *   0.38f
            );
        FonText.rectTransform.anchoredPosition = new Vector2(
            0,
            2*s.y / 6
            ) ;

        Loaing.rectTransform.sizeDelta = new Vector2(
            (s.x * 0.38f) ,
            (s.x * 0.38f) 
            );
        Loaing.rectTransform.anchoredPosition = new Vector2(
            0,
            -((FonText.rectTransform.anchoredPosition.y+s.y/2)- Loaing.rectTransform.sizeDelta.y)/2
            );
         FonText.color = new Color(FonText.color.r, FonText.color.g, FonText.color.b, Backgrond.color.a);
         Loaing.color = new Color(Loaing.color.r, Loaing.color.g, Loaing.color.b, Backgrond.color.a);
    }

    public void changeState(StateOfLoading soc) {
        currentstate = soc;
    }

     
}
