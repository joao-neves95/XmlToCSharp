// ***********************************************************************
// Assembly         : Xml2CSharp
// Author           : msyoung
// Created          : 08-04-2018
//
// Last Modified By : msyoung
// Last Modified On : 08-04-2018
// ***********************************************************************
// <copyright file="ClassInfoWriter.cs" company="">
//     Copyright ©  2014
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.IO;

namespace Xml2CSharp
{
	/// <summary>
	/// Class ClassInfoWriter.
	/// </summary>
	public class ClassInfoWriter
	{
		/// <summary>
		/// The class information
		/// </summary>
		private readonly IEnumerable<Class> _classInfo;
		/// <summary>
		/// The custom name space
		/// </summary>
		private readonly string _customNameSpace;

		/// <summary>
		/// Initializes a new instance of the <see cref="ClassInfoWriter"/> class.
		/// </summary>
		/// <param name="classInfo">The class information.</param>
		/// <param name="customNameSpace">The custom name space.</param>
		public ClassInfoWriter(IEnumerable<Class> classInfo, string customNameSpace = null)
		{
			_classInfo = classInfo;
			_customNameSpace = customNameSpace;

		}

		/// <summary>
		/// Writes the specified text writer.
		/// </summary>
		/// <param name="textWriter">The text writer.</param>
		public void Write(TextWriter textWriter)
		{
			string tabChar = null;

			using (textWriter)
			{
				if (!string.IsNullOrEmpty(_customNameSpace))
				{
					textWriter.WriteLine("namespace {0}", _customNameSpace);
					textWriter.WriteLine("{");
					tabChar = "\t";
				}

				foreach (var @class in _classInfo)
				{
					textWriter.WriteLine("{0}[XmlRoot(ElementName=\"{1}\", Namespace=\"{2}\")]", tabChar, @class.XmlName, @class.Namespace);
					textWriter.WriteLine("{0}public class {1} {{", tabChar, @class.Name);
					foreach (var field in @class.Fields)
					{
						textWriter.WriteLine("{0}\t[Xml{0}({1}Name=\"{2}\", Namespace=\"{3}\")]", tabChar, field.XmlType, field.XmlName, field.Namespace);
						textWriter.WriteLine("{0}\tpublic {1} {2};", tabChar, field.Type, field.Name);
					}

					textWriter.Write(tabChar);
					textWriter.WriteLine("}");
					textWriter.WriteLine("");
				}

				if (!string.IsNullOrEmpty(_customNameSpace))
				{
					textWriter.WriteLine("}");
				}
			}
		}
	}
}