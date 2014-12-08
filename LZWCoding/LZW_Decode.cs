using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LZWDecoder
{
    class LZW_Decode
    {
        private string input = string.Empty;
        private string path = string.Empty;
        public LZW_Decode( Dictionary<string, int> dictionary, string output, string path)
        {
            decoder(dictionary, output, path);
        }
        ~LZW_Decode() { }
        public string getInput
        {
            get { return input; }
        }
        public string decoder( Dictionary<string, int> dictionary, string output, string path )
        {
            List<string> codeWord = output.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            int preValue = 0;
            string oneWord = string.Empty;
            string nowKey = string.Empty;
            string preKey = string.Empty;
            foreach (string oneCodeWord in codeWord)
            {
                nowKey = FindKey(dictionary, Convert.ToInt16(oneCodeWord));
                preKey = FindKey(dictionary, preValue);
                if (preKey.Equals("error")) { preKey = string.Empty; }  //一開始時遇到的問題
                if (nowKey.Equals("error"))     //遇到abababababab......這種狀況
                {
                    nowKey = preKey;
                    input += preKey + preKey.Substring( 0, 1 );
                }
                else
                    input += nowKey;
                string newKey = preKey + nowKey.Substring( 0, 1 );  //字典在長時，只需用到現在解碼的第一個字母
                if (!dictionary.ContainsKey(newKey))
                {
                    dictionary.Add( newKey, dictionary.Count() + 1 );
                }
                preValue = Convert.ToInt16(oneCodeWord);
                using (StreamWriter sw = new StreamWriter(path + @"\DecodeDictionary.txt", true, Encoding.Default))
                {
                    sw.WriteLine("Now is " + oneCodeWord);
                    foreach (KeyValuePair<string, int> show in dictionary)
                    {
                        sw.WriteLine(show.Key + "  " + show.Value);
                    }
                    sw.WriteLine();
                }
            }

            return input;
        }
        private string FindKey( Dictionary<string, int> dictionary, int oneCodeWord)
        {
            foreach (KeyValuePair<string, int> Key in dictionary)
            {
                if (Key.Value == oneCodeWord)
                {
                    return Key.Key;
                }
            }
            return "error";
        }
    }
}
