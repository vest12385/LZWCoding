using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZWDecoder
{
    class LZW_Decode
    {
        private string input = string.Empty;
        public LZW_Decode( Dictionary<string, int> dictionary, string output)
        {
            decoder(dictionary, output);
        }
        ~LZW_Decode() { }
        public string getInput
        {
            get { return input; }
        }
        public string decoder( Dictionary<string, int> dictionary, string output )
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
                if (preKey.Equals("error")) { preKey = string.Empty; }
                if (nowKey.Equals("error")) 
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
