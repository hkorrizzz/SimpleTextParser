using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor
{
    class TextParser
    {
        public string txt;
        public TextParser(string _txt)
        {
            txt = _txt;
        }

        public Text Parse()
        {
            Text finishedSentense = new Text();

            string newWord = "";
            string newSign = "";
            Sentence newSentence = new Sentence();  //////////////////////////

            for (int i = 0; i < txt.Length; i++)
            {
                if ((txt[i] >= 'А' && txt[i] <= 'я') || (txt[i] >= 'A' && txt[i] <= 'z'))
                {
                    newWord += txt[i];
                }
                else
                {
                    if (newWord.Length > 0)
                    {
                        newSentence.AddWord(new Word(newWord));   /////////////////////////
                        newWord = "";
                    }
                    if (txt[i] == '.' || txt[i] == '!' || txt[i] == '?' || txt[i] == ';' || txt[i] == ':' || txt[i] == ',' || txt[i] == '–' || txt[i] == '(' || txt[i] == ')' || txt[i] == '«' || txt[i] == '»')
                    {
                        newSign += txt[i];
                        newSentence.AddPunctuation(new Punctuation(newSign));
                        newSign = "";
                        if (txt[i] == '.' || txt[i] == '!' || txt[i] == '?')
                        {
                            finishedSentense.AddSentence(newSentence);
                            newSentence = new Sentence();
                        }
                    }
                }
            }
            return finishedSentense;
        }

    }
}
