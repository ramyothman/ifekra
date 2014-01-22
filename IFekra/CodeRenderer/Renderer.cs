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
        public static void Render(Page page,Html.Html html)
        {
            Renderer.page = page;
            foreach (Html.Tag tag in html.RootTag.Content)
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
        protected static Head RenderHead(Html.Tag HeadTag,Element ParentElement)
        {
            Head Head = new Head(HeadTag.Attributes,ParentElement);
            foreach (Html.Tag tag in HeadTag.Content)
                if (tag.name == "title")
                    Head.Title = RenderTitle(tag);
                else throw new NotImplementedException();
            return Head;
        }
        protected static Element RenderTag(Html.Tag tag,Element ParentElement)
        {
            Element element = Element.CreateElement(tag,ParentElement);
            bool isPrevText = false;
            switch (element.type)
            {
                case ElementType.DIVISION:

                    Division division = (Division)element;
                    foreach(Html.Token token in tag.Content)
                        if (token.type == Html.TokenType.TEXT)
                        {
                            if (isPrevText)
                                division.Add(new Text(" ",division));
                            division.Add(new Text((Html.Text)token,division));
                            isPrevText = true;
                        }
                        else
                        {
                            division.Add(RenderTag((Html.Tag)token,division));
                            isPrevText = false;
                        }

                    break;
                default:
                    //throw new NotImplementedException();
                    break;
            }
            return element;
        }
        protected static string RenderTitle(Html.Tag TitleTag)
        {
            string Title = "";
            foreach (Html.Text text in TitleTag.Content)
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
