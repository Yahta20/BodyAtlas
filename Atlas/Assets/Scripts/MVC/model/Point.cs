using System.Collections.Generic;

[System.Serializable]
public class Point
{
    public string lat;
    public string ua;
    public string en;
    public string ru;
    public Dictionary<Lang, string> pointDic { get; private set; } = new Dictionary<Lang, string>();
    public void fillName()
    {
        pointDic.Add(Lang.lat, lat);
        pointDic.Add(Lang.ua,  ua);
        pointDic.Add(Lang.en,  en);
        pointDic.Add(Lang.ru,  ru);
    }
}
