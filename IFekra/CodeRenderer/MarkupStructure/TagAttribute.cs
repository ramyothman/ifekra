using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeRenderer.MarkupStructure
{
    class TagAttribute
    {
        KeyValuePair<string, string > pair;
        public string Key { get { return pair.Key; } }
        public string Value { get { return pair.Value; } }
        

        public TagAttribute() { }
        public TagAttribute(string Key, string Value) 
        { 
            pair = new KeyValuePair<string, string>(Key, Value); 
        }

        #region Parsing Attributes
        static public List<TagAttribute> ParseAttributes(List<string> TokensList) { return ParseAttributes(TokensList.ToArray()); }
        static public List<TagAttribute> ParseAttributes(string[] tokens)
        {
            List<TagAttribute> attributes = new List<TagAttribute>();
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i].Trim().Length == 0) continue;
                string key = tokens[i];
                while (tokens[i++] != "=") ;
                while (tokens[i++] != "\"") ;
                string value = "";
                for (; tokens[i] != "\""; i++)
                    value += tokens[i];
                attributes.Add(new TagAttribute(key, value));
            }
            return attributes;
        }
        #endregion

        public void Print(int depth = 0) 
        {
            for (int i = 0; i < depth; i++)
                Console.Write("\t");
            Console.WriteLine(Key + " = " + Value); 
        }
    }
}
