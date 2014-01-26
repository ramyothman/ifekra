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
        public Attributes Attributes;   // Contains the non-inherited (old) style
        public Style Style;             // Contains the inherited style
        public Element ParentElement;
        public List<Element> Elements;

        public Element() { }
        public Element(Element Element) // without Parent
        {
            this.type = Element.type;
            this.Attributes = (Attributes)Element.Attributes.Clone();
            this.Style = (Style)Element.Style.Clone();
            this.Elements = new List<Element>();
            foreach (Element ChildRef in Element.Elements)
            {
                Element ChildClone = (Element)(ChildRef.Clone());
                ChildClone.ParentElement = this;
                this.Elements.Add(ChildClone);
            }
            this.ParentElement = null;
        }

        public abstract object Clone();
        

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

        public string AttributesString()
        {
            return this.Attributes.ToString() + this.Style.ToString();
        }

        
    }
    
    class RootElement : Element, ICloneable
    {
        public Head Head;
        public Body Body;

        public RootElement(RootElement Root) : base(Root)
        {
            this.Head = (Head)Root.Head.Clone();
            this.Body = (Body)Root.Body.Clone();
        }
        public override object Clone()
        {
            return new RootElement(this);
        }

        public RootElement(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes) : base(Attributes, null) { }
        public override void Print()
        {
            Head.Print();
            Body.Print();
        }
        public override string ToString()
        {
            string ret = "";
            ret += "<html" + AttributesString() + ">\n";
            ret += Head.ToString();
            ret += Body.ToString();
            ret += "</html>";
            return ret;
        }



        

    }

    class Head : Element , ICloneable
    {
        public string Title;

        public Head(Head Head) : base(Head)
        {
            this.Title = new string(Head.Title.ToCharArray());
        }
        public override object Clone()
        {
            return new Head(this);
        }
        public Head(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement) : base(Attributes, ParentElement) { }
        public override void Print()
        {
            Console.WriteLine("Title: \"{0}\"\n", Title);
        }
        public override string ToString()
        {
            string ret = "<head" + AttributesString() + ">\n";
            ret += base.ToString();
            ret += "/head\n";
            return ret;
        }

    }
    class Body : Element , ICloneable
    {

        public Body(Body Body) : base(Body) { }
        public override object Clone()
        {
            return new Body(this);
        }

        public Body(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement) : base(Attributes, ParentElement) { }
        public override void Print()
        {
            foreach (Element element in Elements)
                element.Print();
        }
        public override string ToString()
        {
            string ret = "<body" + AttributesString() + ">\n";
            ret += base.ToString();
            ret += "</body>\n";
            return ret;
        }


    }
    
    class NewLine : Element , ICloneable
    {
        public NewLine(Element ParentElement) 
        { 
            this.type = ElementType.NEWLINE; 
        }
        public override object Clone()
        {
            return new NewLine(null);
        }
        public override void Print() { Console.WriteLine(); }
        public override string ToString()
        {
            return "<br/>\n";
        }
    }
    class Division : Element , ICloneable
    {
        public Division(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement)
            :base(Attributes,ParentElement)
        {
            this.type = ElementType.HasElements;
        }
        public Division(Division Division) : base(Division) { }
        public override object Clone()
        {
            return new Division(this);
        }
        public override void Print()
        {
            base.Print();
        }
        public override string ToString()
        {
            string ret = "";
            ret += "<div" + AttributesString() + ">\n";
            ret += base.ToString();
            ret += "</div>\n";
            return ret;
        }

    }
    class Text : Element , ICloneable
    {
        public string text;
        public static string[] keys = { "&amp", "&lt", "&gt", "&quot" };
        public static string[] values = { "&", "<", ">", "\"" };

        public Text(Text Text)
            : base(Text)
        {
            this.text = new string(Text.text.ToCharArray());
        }
        public override object Clone()
        {
            return new Text(this);
        }
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
    class Image : Element , ICloneable
    {
        public string Path, Alt;

        public Image(Image Image)
            : base(Image)
        {
            this.Path = new string(Image.Path.ToCharArray());
            this.Alt = new string(Image.Alt.ToCharArray());
        }
        public override object Clone()
        {
            return new Image(this);
        }
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
            ret += "<img" + AttributesString() + "/>\n";
            return ret;
        }

    }
    class Paragraph : Element , ICloneable
    {
        public Paragraph(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes,Element ParentElement) : base(Attributes,ParentElement) {  }

        public Paragraph(Paragraph Paragraph)
            : base(Paragraph) { }
        public override object Clone()
        {
            return new Paragraph(this);
        }
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
            string ret = "<p" + AttributesString() + ">\n";
            ret += base.ToString();
            ret += "</p>\n";
            return ret;
        }
    }
    

    
    

}
