using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRenderer
{
    class Attributes
    {
        public Dictionary<string, string> Map;
        public Attributes(List<CodeRenderer.MarkupStructure.TagAttribute> Attributes, Element ParentElement = null)
        {
            Map = new Dictionary<string, string>();
            if (ParentElement != null)
                Map = new Dictionary<string,string>(ParentElement.Attributes.Map);
            if (Attributes != null)
            {
                foreach (CodeRenderer.MarkupStructure.TagAttribute Attribute in Attributes)
                {
                    if (Map.ContainsKey(Attribute.Key))
                        Map[Attribute.Key] = Attribute.Value;
                    else Map.Add(Attribute.Key, Attribute.Value);
                }
            }
        }
        
    }
}
