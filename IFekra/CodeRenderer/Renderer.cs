using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRenderer
{
    class Renderer
    {
        static Page page;

        #region Html
        public static void Render(Page page,CodeRenderer.MarkupStructure.Html html)
        {
            Renderer.page = page;
            foreach (CodeRenderer.MarkupStructure.Tag tag in html.RootTag.Content)
            {
                switch (tag.name)
                {
                    case "head":
                        page.Html.Head = (Head)RenderHead(tag,page.Html);
                        break;
                    case "body":
                        page.Html.Body = (Body)RenderTag(tag,page.Html);
                        break;
                    default:
                        throw new NotImplementedException();
                        break;
                }
            }

        }
        protected static Head RenderHead(CodeRenderer.MarkupStructure.Tag HeadTag,Element ParentElement)
        {
            Head Head = new Head(HeadTag.Attributes,ParentElement);
            foreach (CodeRenderer.MarkupStructure.Tag tag in HeadTag.Content)
                if (tag.name == "title")
                    Head.Title = RenderTitle(tag);
                else throw new NotImplementedException();
            return Head;
        }
        protected static Element RenderTag(CodeRenderer.MarkupStructure.Tag tag,Element ParentElement)
        {
            Element element = Element.CreateElement(tag,ParentElement);
            bool isPrevText = false;
            switch (element.type)
            {
                case ElementType.DIVISION:

                    Section division = (Section)element;
                    foreach(CodeRenderer.MarkupStructure.Token token in tag.Content)
                        if (token is CodeRenderer.MarkupStructure.Text)
                        //if (token.type == CodeRenderer.MarkupStructure.TokenType.TEXT)
                        {
                            if (isPrevText)
                                division.Add(new Text(" ",division));
                            division.Add(new Text((CodeRenderer.MarkupStructure.Text)token,division));
                            isPrevText = true;
                        }
                        else
                        {
                            division.Add(RenderTag((CodeRenderer.MarkupStructure.Tag)token,division));
                            isPrevText = false;
                        }

                    break;
                default:
                    //throw new NotImplementedException();
                    break;
            }
            return element;
        }
        protected static string RenderTitle(CodeRenderer.MarkupStructure.Tag TitleTag)
        {
            string Title = "";
            foreach (CodeRenderer.MarkupStructure.Text text in TitleTag.Content)
            {
                if (text.text.Trim().Length == 0) continue;
                if (Title.Length > 0)
                    Title += " ";
                Title += text.text;
            }
            return Title;
        }
        
        #endregion
    }
}
