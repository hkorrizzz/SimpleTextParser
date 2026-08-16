using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TextEditor
{
    public class Word
    {
        [XmlElement("Word")]

        public string Letters;
        public Word() { }
        public Word(string value) { Letters = value; }

        public override string ToString()
        {
            return Letters;
        }
    }

    public class Punctuation
    {
        [XmlElement("Sign")]
        public string Sign;
        public Punctuation() { }
        public Punctuation(string value) { Sign = value; }

        public override string ToString()
        {
            return Sign;
        }
    }


    public class Sentence
    {
       [XmlElement("Token")]

        public List<object> Tokens;
        public Sentence() { Tokens = new List<object>(); }

        public void AddWord(Word w) { Tokens.Add(w); }
        public void AddPunctuation(Punctuation s) { Tokens.Add(s); }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < Tokens.Count; i++)
            {
                if (Tokens[i].ToString() == "–") { result += $" {Tokens[i]} "; }
                else
                {
                    if (Tokens[i].ToString() == "(" || Tokens[i].ToString() == "«") { result += $" {Tokens[i]}"; }
                    else
                    {
                        if (i != Tokens.Count - 1)
                        {
                            result += Tokens[i];
                            if (Tokens[i] is Word && Tokens[i + 1] is Word) { result += " "; }
                            if (Tokens[i] is Punctuation && Tokens[i + 1] is Word) { result += " "; }
                        }
                        else { result += Tokens[i]; }
                    }
                }
            }
            return result;
        }
    }
}
