// ***********************************************************************
// Assembly         : Xml2CSharp
// Author           : msyoung
// Created          : 08-04-2018
//
// Last Modified By : msyoung
// Last Modified On : 08-04-2018
// ***********************************************************************
// <copyright file="Class.cs" company="">
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
	/// Class Class.
	/// </summary>
	public class Class
    {
		/// <summary>
		/// Equalses the specified other.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		protected bool Equals(Class other)
        {
            return string.Equals(XmlName, other.XmlName) &&  Fields.Matches(other.Fields);
        }

		/// <summary>
		/// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Class) obj);
        }

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
		public override int GetHashCode()
        {
            return (XmlName != null ? XmlName.GetHashCode() : 0);
        }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the fields.
		/// </summary>
		/// <value>The fields.</value>
		public IEnumerable<Field> Fields { get; set; }
		/// <summary>
		/// Gets or sets the name of the XML.
		/// </summary>
		/// <value>The name of the XML.</value>
		public string XmlName { get; set; }
		/// <summary>
		/// Gets or sets the namespace.
		/// </summary>
		/// <value>The namespace.</value>
		public string Namespace { get; set; }

		/// <summary>
		/// Determines whether the specified element is repeated.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns><c>true</c> if the specified element is repeated; otherwise, <c>false</c>.</returns>
		public bool IsRepeated(XElement element)
        {
            return element.ElementsAfterSelf(Name).Any();
        }

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>A <see cref="System.String" /> that represents this instance.</returns>
		public override string ToString()
        {
            return string.Format("Name: {0}, Fields: {1}, Namespace: {2}, XmlName: {3}", Name, Fields, Namespace, XmlName);
        }
    }

	/// <summary>
	/// Class FieldsExtensions.
	/// </summary>
	public static class FieldsExtensions
    {
		/// <summary>
		/// Matcheses the specified other.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="other">The other.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		public static bool Matches(this IEnumerable<Field> input, IEnumerable<Field> other)
        {
            return input.OrderBy(f => f.Name).SequenceEqual(other.OrderBy(f => f.Name));
        }
    }
}