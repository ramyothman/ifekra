using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRenderer
{
    enum ElementType
    {
        TEXT, IMAGE, HasElements, NEWLINE
    }
    
    abstract class Element
    {
        public ElementType type;
        public Attributes Attributes;
        public Style Style;
        public Element ParentElement;
        public List<Element> Elements;

        public Element() { }
        public Element(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement) 
        { 
            this.ParentElement = ParentElement;
            this.Attributes = new Attributes(Attributes, ParentElement);
            this.Style = new Style(this.Attributes, ParentElement);
            this.Elements = new List<Element>();
            this.type = ElementType.HasElements;
            
        }
        public virtual void Add(Element element)
        {
            Elements.Add(element);
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
        public virtual void Print()
        {
            Console.WriteLine();
            Console.Write(">> Element Style: ");
            Style.Print();

            Console.WriteLine();
            foreach (Element element in Elements)
                element.Print();
            Console.WriteLine();
            return;
        }
        public override string ToString()
        {
            string ret = "";
            foreach (Element element in Elements)
                ret += element.ToString();
            return ret;
        }
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
        public override string ToString()
        {
            string ret = "";
            ret += "<html" + this.Attributes.ToString() + ">\n";
            ret += Head.ToString();
            ret += Body.ToString();
            ret += "</html>";
            return ret;
        }

    }
    class NewLine : Element
    {
        public NewLine(Element ParentElement) 
        { 
            this.type = ElementType.NEWLINE; 
        }
        public override void Print() { Console.WriteLine(); }
        public override string ToString()
        {
            return "<br/>\n";
        }
    }
    class Division : Element
    {
        public Division(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement)
            :base(Attributes,ParentElement)
        {
            this.type = ElementType.HasElements;
        }
        public override void Print()
        {
            base.Print();
        }
        public override string ToString()
        {
            string ret = "";
            ret += "<div" + this.Attributes.ToString() + ">\n";
            ret += base.ToString();
            ret += "</div>\n";
            return ret;
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
        public override string ToString()
        {
            return this.text + "\n";
        }
    }
    class Image : Element
    {
        public string Path, Alt;
        public Image(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement)
            : base(Attributes, ParentElement)
        {
            this.type = ElementType.IMAGE;
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
        public override string ToString()
        {
            string ret = "";
            ret += "<img" + this.Attributes.ToString() + "/>\n";
            return ret;
        }

    }
    
    class Head : Element
    {
        public string Title;
        public Head(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement) : base(Attributes,ParentElement) { }
        public override void Print()
        {
            Console.WriteLine("Title: \"{0}\"\n", Title);
        }
        public override string ToString()
        {
            string ret = "<head" + this.Attributes + ">\n";
            ret += base.ToString();
            ret += "/head\n";
            return ret;
        }

    }
    class Body : Element
    {
        public Body(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes,Element ParentElement) : base(Attributes,ParentElement) { }
        public override void Print()
        {
            foreach (Element element in Elements)
                element.Print();
        }
        public override string ToString()
        {
            string ret = "<body" + this.Attributes.ToString() + ">\n";
            ret += base.ToString();
            ret += "</body>\n";
            return ret;
        }

    }
    class Paragraph : Element
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
        public override string ToString()
        {
            string ret = "<p" + this.Attributes.ToString() + ">\n";
            ret += base.ToString();
            ret += "</p>\n";
            return ret;
        }
    }
    

    
    

}
