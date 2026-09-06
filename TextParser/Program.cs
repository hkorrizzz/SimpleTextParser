using System.Collections;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace TextEditor
{

    class Program
    {
        static void Main(string[] args)
        {
            string inputText = File.ReadAllText("text.txt");

            TextParser parsing = new TextParser(inputText);
            Text textForCommand = parsing.Parse();

            //textForCommand.SentencesInAscendingOfWords();               //в порядке возрастания количества слов

            //textForCommand.SentencesInAscendingOfSentenceLength();      //в порядке возрастания длины предложения

            //textForCommand.FindWordsInInterrogativeSentences();         //слова заданной длины в вопр

            //textForCommand.RemoveWordsWithConsonantLetter();            //слова заданной длины с согласной буквы

            //textForCommand.ReplaceWordsWithSubstring();                 //заменить слова заданной длины на подстроку

            //textForCommand.RemoveStopwords();                           //Удалить стоп-слова из текста

            //textForCommand.TextToXML();                                 //xml

            textForCommand.WordFrequency();                               //4 лаба
        }
    }
}




