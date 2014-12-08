using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace LZWEncoder
{
    class LZW_Encode
    {
        private Dictionary<string, int> InitDictionary = new Dictionary<string, int>();
        private string output = string.Empty;
        public LZW_Encode( string Content, string path )
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            List<char> ContentChar = Content.ToList();
            ContentChar.Sort();
            foreach (char ecahWord in ContentChar)
            {
                dictionary = bulidDictionary(dictionary, ecahWord.ToString());
            }
            InitDictionary = new Dictionary<string, int>(dictionary);
            encoder(dictionary, Content, path);
        }
        ~LZW_Encode() { }

        public Dictionary<string, int> getDictionary
        {
            get { return InitDictionary; }
        }
        public string getOutput
        {
            get { return output; }
        }

        private Dictionary<string, int> bulidDictionary( Dictionary<string, int> dictionary, string key)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key.ToString(), dictionary.Count() + 1);
            }
            return dictionary;
        }

        private string encoder( Dictionary<string, int> dictionary, string Content, string path)
        {
            int pointer = 0;
            string oneWord = string.Empty;
            while (pointer < Content.Count())
            {
                oneWord = Content[pointer].ToString();
                pointer++;
                while (dictionary.ContainsKey(oneWord) && pointer < Content.Count())
                {
                    oneWord += Content[pointer++].ToString();
                }
                if (dictionary.ContainsKey(oneWord))
                {
                    output += dictionary[oneWord] + " ";
                }
                else
                {
                    output += dictionary[oneWord.Substring(0, oneWord.Count() - 1)] + " ";
                    bulidDictionary(dictionary, oneWord);
                    pointer--;
                }
                using (StreamWriter sw = new StreamWriter(path + @"\EncodeDictionary.txt", true, Encoding.Default))
                {
                    sw.WriteLine("Now is " + Content[pointer - 1]);
                    foreach (KeyValuePair<string, int> show in dictionary)
                    {
                        sw.WriteLine(show.Key + "  " + show.Value);
                    }
                    sw.WriteLine();
                }
            }
            return output;
        }

    }
}
