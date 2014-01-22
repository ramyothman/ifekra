using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeRenderer.MarkupStructure
{
    enum TokenType
    {
        TAG,TEXT
    }
    abstract class Token
    {
        public TokenType type;
        abstract public void Print(int depth);
    }
    class Text : Token
    {
        public string text;
        public Text(string text) 
        {
            this.text = text;
            this.type = TokenType.TEXT;
        }
        override public void Print(int depth = 0) 
        {
            for (int i = 0; i < depth; i++)
                Console.Write("\t");
            Console.WriteLine(text); 
        }
    }
    class Tag : Token
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
            Console.WriteLine(type); 
        }
        
    }
}
