using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRenderer
{
    class Style
    {
        public Dictionary<string, string> Map;
        public Style(Attributes Attributes, Element ParentElement) 
        {
            this.Map = new Dictionary<string, string>();
            if (ParentElement != null)
                this.Map = new Dictionary<string,string>(ParentElement.Style.Map);
            if (Attributes.Map.ContainsKey("style"))
            {
                List<KeyValuePair<string, string>> StyleList = ParseStyle(Attributes.Map["style"]);
                foreach (KeyValuePair<string, string> StyleToken in StyleList)
                {
                    if (Map.ContainsKey(StyleToken.Key))
                        Map[StyleToken.Key] = StyleToken.Value;
                    else Map.Add(StyleToken.Key, StyleToken.Value);
                }
            }
        }
        public void Print()
        {
            foreach (KeyValuePair<string, string> StyleToken in Map)
                Console.Write("{0}:{1}; ", StyleToken.Key, StyleToken.Value);
            Console.WriteLine();
        }
        protected static List<KeyValuePair<string,string>> ParseStyle(string StyleString)
        {
            List<KeyValuePair<string, string>> StyleList = new List<KeyValuePair<string, string>>();
            string[] tokens = CodeRenderer.MarkupStructure.StringTokenizer.Split(StyleString, new string[] { ":", ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i+=4)
                StyleList.Add(new KeyValuePair<string,string>(tokens[i], tokens[i + 2]));
            return StyleList;
        }
        
        
    }
}
