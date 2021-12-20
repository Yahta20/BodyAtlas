using System;
using System.Collections.Generic;

public sealed class TextUI
{
    public static TextUI instance;//TextUI.instance.languageInfo

    public static TextUI Singelton
    {
        get
        {
            if (instance == null)
            {
                instance = new TextUI();
            }
            return instance;
        }
    }

    public List<Dictionary<Lang, string>> Labels { get; private set; }


    public TextUI() {
        Labels = new List<Dictionary<Lang, string>>();
        var dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Choose mode");
        dictPage.Add(Lang.en,  "Choose mode");
        dictPage.Add(Lang.ua,  "Оберіть режим");
        dictPage.Add(Lang.ru,  "Выберите режим");
        Labels.Add(dictPage);

        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Choose language");
        dictPage.Add(Lang.en, "Choose language");
        dictPage.Add(Lang.ua,  "Оберіть мову");
        dictPage.Add(Lang.ru,  "Выберите язык");
        Labels.Add(dictPage);

        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Atlas");
        dictPage.Add(Lang.en,  "Atlas");
        dictPage.Add(Lang.ua,  "Атлас");
        dictPage.Add(Lang.ru,  "Атлас");
        Labels.Add(dictPage);

        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Exam");
        dictPage.Add(Lang.en,  "Exam");
        dictPage.Add(Lang.ua,  "Перевірка");
        dictPage.Add(Lang.ru,  "Проверка");
        Labels.Add(dictPage);

        //dictPage = new Dictionary<Lang, string>();
        //dictPage.Add(Lang.lat, "Exam"    );
        //dictPage.Add(Lang.en, "Exam"     );
        //dictPage.Add(Lang.ua, "Перевірка");
        //dictPage.Add(Lang.ru, "Проверка" );
        //Labels.Add(dictPage);

        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Chose type of test");
        dictPage.Add(Lang.en,  "Chose type of test");
        dictPage.Add(Lang.ua,  "Оберіть тип теста" );
        dictPage.Add(Lang.ru,  "Выберите тип теста");
        Labels.Add(dictPage);


        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Test of random questions\nbones and dots on them");//"Тест из вопросов о костях"
        dictPage.Add(Lang.en, "Test of random questions\nbones and dots on them" );
        dictPage.Add(Lang.ua, "Тест з випадкових питань\nкостей і точок на них"  );
        dictPage.Add(Lang.ru, "Тест из случайных вопросов\nкостей и точек на них");
        Labels.Add(dictPage);

        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Test of questions about bones");//"Тест из вопросов о костях"
        dictPage.Add(Lang.en, "Test of questions about bones" );
        dictPage.Add(Lang.ua, "Тест з питань про кістки"     );
        dictPage.Add(Lang.ru, "Тест из вопросов о костях"     );
        Labels.Add(dictPage);

        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Test of questions about points on bones");//"Test of questions about points on bones"
        dictPage.Add(Lang.en,  "Test of questions about points on bones");
        dictPage.Add(Lang.ua, "Тест з питань про точки на кістках");
        dictPage.Add(Lang.ru,  "Тест из вопросов о точках на костях"    );
        Labels.Add(dictPage);

        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Choose the correct answer");//"Test of questions about points on bones"
        dictPage.Add(Lang.en, "Choose the correct answer");
        dictPage.Add(Lang.ua, "Виберіть правильну відповідь");
        dictPage.Add(Lang.ru, "Выберите правильный ответ");
        Labels.Add(dictPage);

        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat,  "Description of the selected test");
        dictPage.Add(Lang.en, "Description of the selected test");
        dictPage.Add(Lang.ua, "Опис обраного тесту");
        dictPage.Add(Lang.ru, "Описание выбранного теста");
        Labels.Add(dictPage);

        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Exit");
        dictPage.Add(Lang.en,  "Exit");
        dictPage.Add(Lang.ua,  "Вихід");
        dictPage.Add(Lang.ru,  "Выход");
        Labels.Add(dictPage);

        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "Retry");
        dictPage.Add(Lang.en,  "Retry");
        dictPage.Add(Lang.ua, "Повторити");
        dictPage.Add(Lang.ru, "Повторить");
        Labels.Add(dictPage);


        dictPage = new Dictionary<Lang, string>();
        dictPage.Add(Lang.lat, "correct ansvers");
        dictPage.Add(Lang.en, "correct ansvers");
        dictPage.Add(Lang.ua, "правельних відповідей");
        dictPage.Add(Lang.ru, "правельных ответов");
        Labels.Add(dictPage);




    }


    public string getLabel(string label) {
        foreach (var item in Labels)
        {
            if (label == item[Lang.lat])
            {
                return item[GameManager.Singelton.currentLang];
            }
        }
        return "none";
    }
}
