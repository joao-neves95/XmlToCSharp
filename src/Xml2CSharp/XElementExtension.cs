// ***********************************************************************
// Assembly         : Xml2CSharp
// Author           : msyoung
// Created          : 08-04-2018
//
// Last Modified By : msyoung
// Last Modified On : 08-04-2018
// ***********************************************************************
// <copyright file="XElementExtension.cs" company="">
//     Copyright ©  2014
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Xml2CSharp
{
	/// <summary>
	/// Class XElementExtension.
	/// </summary>
	public static class XElementExtension
	{
		/// <summary>
		/// Extracts the class information.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>IEnumerable&lt;Class&gt;.</returns>
		public static IEnumerable<Class> ExtractClassInfo(this XElement element)
		{
			var @classes = new HashSet<Class>();
			ElementToClass(element, classes);
			return @classes;
		}

		/// <summary>
		/// Determines whether the specified element is empty.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns><c>true</c> if the specified element is empty; otherwise, <c>false</c>.</returns>
		public static bool IsEmpty(this XElement element)
		{
			return !element.HasAttributes && !element.HasElements;
		}

		/// <summary>
		/// Elements to class.
		/// </summary>
		/// <param name="xElement">The x element.</param>
		/// <param name="classes">The classes.</param>
		/// <returns>Class.</returns>
		private static Class ElementToClass(XElement xElement, ICollection<Class> classes)
		{
			var @class = new Class
			{
				Name = xElement.Name.LocalName,
				XmlName = xElement.Name.LocalName,
				Fields =  ReplaceDuplicatesWithLists(ExtractFields(xElement, classes)).ToList(),
				Namespace = xElement.Name.NamespaceName
			};

			SafeName(@class, @classes);
			
			if (xElement.Parent == null || (!@classes.Contains(@class) && @class.Fields.Any()))
				@classes.Add(@class);

			return @class;

		}

		/// <summary>
		/// Extracts the fields.
		/// </summary>
		/// <param name="xElement">The x element.</param>
		/// <param name="classes">The classes.</param>
		/// <returns>IEnumerable&lt;Field&gt;.</returns>
		private static IEnumerable<Field> ExtractFields(XElement xElement, ICollection<Class> classes)
		{
			foreach (var element in xElement.Elements().ToList())
			{
				var tempClass = ElementToClass(element, classes);
				var type = element.IsEmpty() ? "String" : tempClass.Name;
				
				yield return new Field
				{
					Name = tempClass.Name,
					Type = type,
					XmlName = tempClass.XmlName,
					XmlType = XmlType.Element,
					Namespace = tempClass.Namespace
				};
			}

			foreach (var attribute in xElement.Attributes().ToList())
			{
				yield return new Field
				{
					Name = attribute.Name.LocalName,
					XmlName = attribute.Name.LocalName,
					Type = attribute.Value.GetType().Name,
					IsGenericCollection = false,
					XmlType = XmlType.Attribute,
					Namespace = attribute.Name.NamespaceName
				};
			}
		}

		/// <summary>
		/// Replaces the duplicates with lists.
		/// </summary>
		/// <param name="fields">The fields.</param>
		/// <returns>IEnumerable&lt;Field&gt;.</returns>
		private static IEnumerable<Field> ReplaceDuplicatesWithLists(IEnumerable<Field> fields)
		{
			return fields.GroupBy(field => field.Name, field => field,
				(key, g) =>
					g.Count() > 1
						? new Field()
						{
							Name = key,
							Namespace = g.First().Namespace,
							Type = string.Format("IList<{0}>", g.First().Type),
							IsGenericCollection = true,
							XmlName = g.First().Type,
							XmlType = XmlType.Element
						} : 
						g.First()).ToList();
		}

		/// <summary>
		/// Safes the name.
		/// </summary>
		/// <param name="class">The class.</param>
		/// <param name="classes">The classes.</param>
		private static void SafeName(Class @class, IEnumerable<Class> classes)
		{
			var count = classes.Count(c => c.XmlName == @class.Name);
			if (count > 0 && !@classes.Contains(@class))
			{
				@class.Name = StripBadCharacters(@class) + (count + 1);
			}
			else
			{
				@class.Name = StripBadCharacters(@class);
			}
		}

		/// <summary>
		/// Strips the bad characters.
		/// </summary>
		/// <param name="class">The class.</param>
		/// <returns>System.String.</returns>
		private static string StripBadCharacters(Class @class)
		{
			return @class.Name.Replace("-", "");
		}
	}
}