using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeRenderer.MarkupStructure;
namespace CodeRenderer
{
    class Page
    {
        public RootElement Html;

        public Page(CodeRenderer.MarkupStructure.Html html)
        {
            this.Html = new RootElement(html.RootTag.Attributes);
            Renderer.Render(this, html);
        }
        public void Print()
        {
            Html.Print();
        }


    }
}
