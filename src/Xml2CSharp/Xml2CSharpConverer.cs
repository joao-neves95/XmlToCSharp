// ***********************************************************************
// Assembly         : Xml2CSharp
// Author           : msyoung
// Created          : 08-04-2018
//
// Last Modified By : msyoung
// Last Modified On : 08-04-2018
// ***********************************************************************
// <copyright file="Xml2CSharpConverer.cs" company="">
//     Copyright ©  2014
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Xml.Linq;

namespace Xml2CSharp
{
    /// <summary>
    /// Class Xml2CSharpConverer.
    /// </summary>
    public class Xml2CSharpConverer
    {
        /// <summary>
        /// Converts the specified XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>IEnumerable&lt;Class&gt;.</returns>
        public IEnumerable<Class> Convert(string xml)
        {
            var xElement = XElement.Parse(xml);

            return xElement.ExtractClassInfo();
        }
    }
}
