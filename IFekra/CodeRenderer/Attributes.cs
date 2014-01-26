using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeRenderer
{
    class Attributes: ICloneable
    {
        public Dictionary<string, string> Map;

        public Attributes(Attributes Attributes)
        {
            this.Map = new Dictionary<string, string>(Attributes.Map);
        }
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
        public override string ToString()
        {
            string ret = "";
            foreach (KeyValuePair<string, string> pair in Map)
            {
                if (pair.Key == "style") continue; // Don't print the non-inherited style
                ret += " " + pair.Key + " = \"" + pair.Value + "\"";

            }
            return ret;
        }
        
        public object Clone()
        {
            return new Attributes(this);
        }

    }
}
