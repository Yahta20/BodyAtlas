using System;



public enum GameState { 
    Hand=0,
    Leg,
    End
}

public enum GameType
{
    Atlas = 0,
    Exam,
}





public sealed class GameEnviroment
{
    public static GameEnviroment instance;//GameEnviroment.instance.languageInfo

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

    public static string password { get; private set; }
    
    public Lang   languageInfo { get; private set; }

    public LangUI languageUi { get; private set;}

    public GameState currentstate { get; private set; }

    public GameType currentType { get; private set; }

    public int  getLanguage() {
        switch (languageInfo)
        {
            case (Lang.lat):
                //languageInfo = Lang.lat;
                return 0;
                break;
            case (Lang.ua):
                //languageInfo = Lang.ua;
                return 1;
                break;
            case (Lang.ru):
                //languageInfo = Lang.ru;
                return 2;
                break;
            case (Lang.en):
                //languageInfo = Lang.en;
                return 3;
                break;
            default:
                languageInfo = Lang.lat;
                return 0;
        }
    }

    public bool key {get; private set;}

    public void setPassword(string s) { password = s; }

    public void setUILanguage(LangUI l) {languageUi = l;}

    public void setLanguage(Lang l){languageInfo = l;}
    
    public void setLanguage(int i)
    {
       int  b = i % 4;
        switch (b) {
            case (0) :
                languageInfo = Lang.lat;
                    break;
            case (1):
                languageInfo = Lang.ua;
                    break;
            case (2):
                languageInfo = Lang.ru;
                break;
            case (3):
                languageInfo = Lang.en;
                break;
        }
    }

    public void setGameState(GameState gs) { currentstate = gs; }

    public void setGameType (GameType gs) { currentType = gs; }

}







