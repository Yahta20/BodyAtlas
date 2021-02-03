using System;


public enum Lang {
    lat  = 0,
    ua   = 1,
    ru  = 2,
    en  = 3,
};

public enum LangUI
{
    ua = 0,
    eng = 1
};

public sealed class GameEnviroment
{
    public static GameEnviroment instance;

    public static GameEnviroment Singelton
    {
        get
        {
            if (instance == null)
            {
                instance = new GameEnviroment();
            }
            return instance;
        }
    }

    public Lang languageInfo { get; private set; }

    public LangUI languageUi { get; private set; }

    public void setLanguage(Lang l)
    {
        languageInfo = l;
    }

    public bool key { get; private set; }

    public static string password { get; private set; }

    public void setPassword(string s) { password = s; }

}






