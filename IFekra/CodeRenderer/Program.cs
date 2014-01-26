using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRenderer
{
    class Program
    {
        static void Main(string[] args)
        {
            Page page = new Page(new CodeRenderer.MarkupStructure.Html("test.html"));
            Page another = (Page)page.Clone();
            
            //Console.WriteLine("Title = " + page.Title);
            /*foreach (Element element in page.Body.Elements)
            {
                Console.Write(((Text)element).text);
                if (((Text)element).text != "\n")
                    Console.Write(" ");
            }*/
            //page.Print();
            Console.WriteLine(page);
            Console.WriteLine(another);
            Console.WriteLine("\n-----\nEnd\n");
            Console.ReadLine();
        }
    }
}
