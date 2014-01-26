using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeRenderer.MarkupStructure;
namespace CodeRenderer
{
    class Page : ICloneable
    {
        public RootElement Html;

        public Page(Page Page)
        {
            this.Html = (RootElement)Page.Html.Clone();
        }
        public object Clone()
        {
            return new Page(this);
        }
        
        public Page(CodeRenderer.MarkupStructure.Html html)
        {
            this.Html = new RootElement(html.RootTag.Attributes);
            Renderer.Render(this, html);
        }
        public void Print()
        {
            Html.Print();
        }
        public override string ToString()
        {
            return Html.ToString();
        }


    }
}
