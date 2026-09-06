using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TextEditor
{
   
        [XmlRoot("Text")]
        [XmlInclude(typeof(Word))]
        [XmlInclude(typeof(Punctuation))]
        public class Text
        {
            [XmlElement("Sentence")]

            public List<Sentence> SentenceToken;
            public Text() { SentenceToken = new List<Sentence>(); }

            public void AddSentence(Sentence sentence) { SentenceToken.Add(sentence); }

            public override string ToString()
            {
                return string.Join(" ", SentenceToken);
            }


            public void SentencesInAscendingOfWords() /////////////////////////////////////////////
            {
                Console.WriteLine("----- Предложения текста в порядке возрастания количества слов в предложениях: -----\n");

                Dictionary<int, Sentence> sentenceWordCounts = new Dictionary<int, Sentence>();
                List<int> listWordCounts = new List<int>();

                foreach (Sentence sentence in SentenceToken)
                {
                    int wordCount = sentence.Tokens.Count(token => token is Word);
                    sentenceWordCounts[wordCount] = sentence;
                    listWordCounts.Add(wordCount);
                }

                listWordCounts.Sort();
                foreach (int worldCount in listWordCounts)
                {
                    Console.WriteLine($"{worldCount} слов: {sentenceWordCounts[worldCount]}\n");
                }
            }

            public void SentencesInAscendingOfSentenceLength() ///////////////////////////////////////////////////////
            {
                Console.WriteLine("----- Предложения текста в порядке возрастания длины предложения: -----\n");

                Dictionary<int, Sentence> sentencesLength = new Dictionary<int, Sentence>();
                List<int> listSentencesLength = new List<int>();

                foreach (Sentence sentence in SentenceToken)
                {
                    int sentenceLength = sentence.ToString().Length;
                    sentencesLength[sentenceLength] = sentence;
                    listSentencesLength.Add(sentenceLength);
                }

                listSentencesLength.Sort();
                foreach (int sentenceLength in listSentencesLength)
                {
                    Console.WriteLine($"Длина предложения - {sentenceLength}: {sentencesLength[sentenceLength]}\n");
                }
            }

            private int EnterGivenLength()
            {
                int givenLength = -1;
                bool isValidInput = false;
                while (!isValidInput)
                {
                    Console.Write("Введите длину искомых слов: ");
                    string tryGivenLength = Console.ReadLine();

                    if (int.TryParse(tryGivenLength, out givenLength) && givenLength > 0)
                    {
                        isValidInput = true;
                    }
                    else { Console.WriteLine("\nНекорректный ввод. Убедитесь что ввели целое положительное число!"); }
                }
                return givenLength;
            }

            public void FindWordsInInterrogativeSentences() //////////////////////////////////////////////
            {
                Console.WriteLine("----- Слова заданной длины во всех вопросительных предложениях: -----\n");

                List<Sentence> interrogativeSentences = new List<Sentence>();
                bool isSentenceInterrogative = false;
                foreach (Sentence sentence in SentenceToken)
                {
                    if (sentence.Tokens.Any(token => token.ToString() == "?"))
                    {
                        isSentenceInterrogative = true;
                        interrogativeSentences.Add(sentence);
                    }
                }

                if (!isSentenceInterrogative)
                {
                    Console.WriteLine("В данном тексте вопросительные предложения отстуствуют!\n");
                }
                else
                {
                    int givenLength = EnterGivenLength();

                    List<Object> words = new List<Object>();
                    foreach (Sentence sentence in interrogativeSentences)
                    {
                        foreach (object word in sentence.Tokens)
                        {
                            if (word is Word && !words.Contains(word.ToString()) && word.ToString().Length == givenLength)
                            {
                                words.Add(word.ToString());
                            }
                        }
                    }

                    if (words.Count == 0) { Console.WriteLine("\nСлов с заданной длиной нет!\n"); }
                    else { Console.WriteLine($"\nСледующие слова имеют заданную вами длину: {String.Join(", ", words)}\n"); }
                }
            }

            public void RemoveWordsWithConsonantLetter() ///////////////////////////////////////////////////
            {
                Console.WriteLine("----- Удаление из текста всех слов заданной длины, начинающихся с согласной буквы: -----\n");

                string consonantLetters = "бвгджзйклмнпрстфхцчшщБВГДЖЗЙКЛМНПРСТФХЦЧШЩbcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ";

                int givenLength = EnterGivenLength();
                foreach (Sentence sentence in SentenceToken)
                {
                    for (int i = 0; i < sentence.Tokens.Count; i++)
                    {
                        object word = sentence.Tokens[i];
                        if (word is Word && consonantLetters.Contains(word.ToString()[0]) && word.ToString().Length == givenLength)
                        {
                            sentence.Tokens.RemoveAt(i);
                        }
                    }
                }
                Console.WriteLine(string.Join(" ", SentenceToken) + "\n");
            }

            public void ReplaceWordsWithSubstring() /////////////////////////////////
            {
                Console.WriteLine("----- Замена слов заданной длины на указанную подстроку: -----\n");
                int givenLength = EnterGivenLength();
                Console.Write("Введите подстроку для замены: ");
                string substring = Console.ReadLine();
                foreach (Sentence sentence in SentenceToken)
                {
                    for (int i = 0; i < sentence.Tokens.Count; i++)
                    {
                        if (sentence.Tokens[i] is Word word && word.Letters.Length == givenLength)
                        {
                            sentence.Tokens[i] = new Word(substring);
                        }
                    }
                }

                Console.WriteLine("Обновленный текст:\n" + string.Join(" ", SentenceToken) + "\n");
            }

            public void RemoveStopwords()
            {
                Console.WriteLine("----- Удаление из текста стоп слов: -----\n");

                string[] stopWord = File.ReadAllLines("stopwords_ru.txt");

                foreach (Sentence sentence in SentenceToken)
                {
                    for (int i = 0; i < sentence.Tokens.Count; i++)
                    {
                        object word = sentence.Tokens[i];
                        if (stopWord.Contains(word.ToString()))
                        {
                            sentence.Tokens.RemoveAt(i);
                        }
                    }


                }
                Console.WriteLine(string.Join(" ", SentenceToken));
            } //////////////////////////////

            public void TextToXML()
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Text));
                StreamWriter fileStream = new StreamWriter("Text.xml");
                xmlSerializer.Serialize(fileStream, this);
                fileStream.Close();
            } ///

            public void WordFrequency()
            {
                Dictionary<string, int> wordsInfFreq = new Dictionary<string, int>();
                Dictionary<string, SortedSet<int>> wordsInfLines = new Dictionary<string, SortedSet<int>>();

                for (int i = 0; i < SentenceToken.Count; i++)
                {
                    foreach (object word in SentenceToken[i].Tokens)
                    {
                        if (word is Word)
                        {
                            string lowerWord = word.ToString().ToLower();

                            if (!wordsInfLines.ContainsKey(lowerWord) && !wordsInfFreq.ContainsKey(lowerWord))
                            {
                                wordsInfFreq[lowerWord] = 0;
                                wordsInfLines[lowerWord] = new SortedSet<int>();
                            }

                            wordsInfFreq[lowerWord]++;
                            wordsInfLines[lowerWord].Add(i + 1);

                        }
                    }
                }
                foreach (KeyValuePair<string, int> wordInfFreq in wordsInfFreq)
                {
                    string lines = "";
                    foreach (int line in wordsInfLines[wordInfFreq.Key]) { lines += $"{line} "; }

                    Console.WriteLine($"{wordInfFreq.Key}..................................{wordInfFreq.Value}: {lines}");
                }

            }

        }
    

}
