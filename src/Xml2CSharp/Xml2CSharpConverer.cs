using System.Collections.Generic;
using System.Xml.Linq;

namespace Xml2CSharp
{
    public class Xml2CSharpConverer
    {
        public IEnumerable<Class> Convert(string xml, string customNameSpace = null)
        {
            var xElement = XElement.Parse(xml);

            return xElement.ExtractClassInfo(customNameSpace);
        }

    }
}