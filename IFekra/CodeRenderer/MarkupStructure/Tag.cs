using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeRenderer.MarkupStructure
{
    
    public abstract class Token
    {
        abstract public void Print(int depth);
    }
    class Text : Token
    {
        public string text;
        public Text(string text) 
        {
            this.text = text;
        }
        override public void Print(int depth = 0) 
        {
            for (int i = 0; i < depth; i++)
                Console.Write("\t");
            Console.WriteLine(text); 
        }
    }
    public class Tag : Token
    {
        public string name;
        public List<Token> Content;
        public List<TagAttribute> Attributes;
        public Tag(string name) 
        {
            this.Content = new List<Token>();
            this.name = name.ToLower();
            //this.Attributes = new List<Attribute>();
        }
        override public void Print(int depth = 0) 
        {
            for (int i = 0; i < depth; i++)
                Console.Write("\t");
            Console.WriteLine(this.GetType().ToString()); 
        }
        
    }
}
