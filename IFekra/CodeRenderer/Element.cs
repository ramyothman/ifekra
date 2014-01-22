using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRenderer
{
    enum ElementType
    {
        TEXT, IMAGE, DIVISION, NEWLINE
    }
    
    abstract class Element
    {
        public ElementType type;
        public Attributes Attributes;
        public Style Style;
        public Element ParentElement;

        public Element() { }
        public Element(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement) 
        { 
            this.ParentElement = ParentElement;
            this.Attributes = new Attributes(Attributes, ParentElement);
            this.Style = new Style(this.Attributes, ParentElement);
        }
        public static Element CreateElement(CodeRenderer.MarkupStructure.Tag tag, Element ParentElement)
        {
            Element element;
            switch (tag.name)
            {
                case "br":
                    element = new NewLine(ParentElement);
                    break;
                case "img":
                    element = new Image(tag.Attributes,ParentElement);
                    break;
                case "body":
                    element = new Body(tag.Attributes,ParentElement);
                    break;
                case "div":
                    element = new Division(tag.Attributes,ParentElement);
                    break;
                case "p":
                    element = new Paragraph(tag.Attributes,ParentElement);
                    break;
                default:
                    throw new NotImplementedException();
                    break;
            }
            return element;
        }
        public abstract void Print();
    }
    
    class RootElement : Element
    {
        public Head Head;
        public Body Body;
        public RootElement(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes) : base(Attributes, null) { }
        public override void Print()
        {
            Head.Print();
            Body.Print();
        }

    }
    class NewLine : Element
    {
        public NewLine(Element ParentElement) 
        { 
            this.type = ElementType.NEWLINE; 
        }
        public override void Print() { Console.WriteLine(); }
    }
    class Division : Element
    {
        public List<Element> Elements;
        public Division(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes,Element ParentElement) : base(Attributes,ParentElement)
        { 
            this.type = ElementType.DIVISION; 
            this.Elements = new List<Element>(); 
        }
        public virtual void Add(Element element)
        {
            Elements.Add(element);
        }
        public override void Print()
        {
            Console.WriteLine();
            Console.Write(">> Division Style: ");
            Style.Print();
            
            Console.WriteLine();
            foreach (Element element in Elements)
                element.Print();
            Console.WriteLine();
            return;
        }


    }
    class Text : Element
    {
        public string text;
        public static string[] keys = { "&amp", "&lt", "&gt", "&quot" };
        public static string[] values = { "&", "<", ">", "\"" };
        public Text(string text, Element ParentElement)
            : base(null, ParentElement)
        {
            this.type = ElementType.TEXT;
            this.text = text;
        }
        public Text(CodeRenderer.MarkupStructure.Text text, Element ParentElement)
            : base(null, ParentElement)
        {
            this.type = ElementType.TEXT;
            this.text = HtmlToText(text.text);
        }
        public static string HtmlToText(string text)
        {
            for (int i = 0; i < keys.Length; i++)
                if (keys[i] == text)
                    return values[i];
            return text;
        }
        public override void Print()
        {
            Console.Write(text);
        }
    }
    class Image : Element
    {
        public string Path, Alt;
        public Image(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement)
            : base(Attributes, ParentElement)
        {
            foreach (CodeRenderer.MarkupStructure.TagAttribute attribute in Attributes)
            {
                switch (attribute.Key)
                {
                    case "src":
                        this.Path = attribute.Value;
                        break;
                    case "alt":
                        this.Alt = attribute.Value;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        public override void Print()
        {
            Console.Write("Image @ " + "\"" + Path + "\"");
            return;
        }
    }
    
    class Head : Division
    {
        public string Title;
        public Head(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement) : base(Attributes,ParentElement) { }
        public override void Print()
        {
            Console.WriteLine("Title: \"{0}\"\n", Title);
        }

    }
    class Body : Division
    {
        public Body(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes,Element ParentElement) : base(Attributes,ParentElement) { }
        public override void Print()
        {
            foreach (Element element in Elements)
                element.Print();
        }

    }
    class Paragraph : Division
    {
        public Paragraph(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes,Element ParentElement) : base(Attributes,ParentElement) {  }
        public override void Add(Element element)
        {
            base.Add(element);
        }
        public override void Print()
        {
            base.Print();
        }
    }
    

    
    

}
