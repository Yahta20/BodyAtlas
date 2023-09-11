using System;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UI;

public class AuditPanelB : MonoBehaviour
{
    public Text Qlabel;
    public ScrollRect AnsverPool;
    RectTransform baseRect;

    private void Start()
    {
        baseRect = GetComponent<RectTransform>();
        PrintPanel(CanvasBehavior.Instance.getSize());
        CanvasBehavior.Instance.OnSizeChanged += PrintPanel;
    }

    public void PrintPanel(Vector2 screen)
    {
        baseRect.sizeDelta = new Vector2(screen.x*0.25f, screen.y);
        baseRect.anchoredPosition = Vector2.zero;

        Qlabel.rectTransform.sizeDelta =  new Vector2(baseRect.sizeDelta.x, baseRect.sizeDelta.y * 0.10f);
        AnsverPool.GetComponent<RectTransform>().sizeDelta = new Vector2(baseRect.sizeDelta.x, baseRect.sizeDelta.y * 0.90f);

    }



}



