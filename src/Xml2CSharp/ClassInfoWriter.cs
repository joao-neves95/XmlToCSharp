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
using System.Linq;

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

		public void WriteHeader(TextWriter textWriter)
		{
			textWriter.WriteLine("using System;");
			textWriter.WriteLine("using System.Xml.Serialization;");
			if (_classInfo.SelectMany(x => x.Fields).Any(x => x.IsGenericCollection)) textWriter.WriteLine("using System.Collections.Generic;");
			
			textWriter.WriteLine();


			if (!string.IsNullOrEmpty(_customNameSpace))
			{
				textWriter.WriteLine("namespace {0}", _customNameSpace);
				textWriter.WriteLine("{");
			}
		}

		public void WriteFooter(TextWriter textWriter)
		{
			if (!string.IsNullOrEmpty(_customNameSpace))
			{
				textWriter.WriteLine("}");
			}
		}

		/// <summary>
		/// Writes the specified text writer.
		/// </summary>
		/// <param name="textWriter">The text writer.</param>
		public void Write(TextWriter textWriter)
		{
			string tabChar = null;

			if (!string.IsNullOrEmpty(_customNameSpace))
			{
				tabChar = "\t";
			}

			foreach (var @class in _classInfo)
			{
				textWriter.Write("{0}[XmlRoot(ElementName=\"{1}\"", tabChar, @class.XmlName);

				if (!string.IsNullOrEmpty(@class.Namespace)) textWriter.Write(", Namespace=\"{0}\"", @class.Namespace);

				textWriter.Write(")]");
				textWriter.WriteLine();

				textWriter.WriteLine("{0}public class {1}", tabChar, @class.Name);
				textWriter.WriteLine("{0}{{", tabChar);

				foreach (var field in @class.Fields)
				{
					textWriter.Write("{0}\t[Xml{1}({1}Name=\"{2}\"", tabChar, field.XmlType, field.XmlName);

					if (!string.IsNullOrEmpty(field.Namespace)) textWriter.Write(", Namespace=\"{0}\"", field.Namespace);

					textWriter.Write(")]");
					textWriter.WriteLine();

					textWriter.WriteLine("{0}\tpublic {1} {2} {{ get; set; }}", tabChar, field.Type, field.Name);
				}

				textWriter.Write(tabChar);
				textWriter.WriteLine("}");
				textWriter.WriteLine("");
			}
		}
	}
}