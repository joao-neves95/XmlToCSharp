using System.Collections.Generic;
using System.IO;

namespace Xml2CSharp
{
	public class ClassInfoWriter
	{
		private readonly IEnumerable<Class> _classInfo;
		private readonly string _customNameSpace;

		public ClassInfoWriter(IEnumerable<Class> classInfo, string customNameSpace = null)
		{
			_classInfo = classInfo;
			_customNameSpace = customNameSpace;

		}

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