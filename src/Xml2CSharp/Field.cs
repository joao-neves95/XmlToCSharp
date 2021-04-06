// ***********************************************************************
// Assembly         : Xml2CSharp
// Author           : msyoung
// Created          : 08-04-2018
//
// Last Modified By : msyoung
// Last Modified On : 08-04-2018
// ***********************************************************************
// <copyright file="Field.cs" company="">
//     Copyright ©  2014
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Xml2CSharp
{
    /// <summary>
    /// Class Field.
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the XML.
        /// </summary>
        /// <value>The type of the XML.</value>
        public XmlType XmlType { get; set; }

        /// <summary>
        /// Gets or sets the namespace.
        /// </summary>
        /// <value>The namespace.</value>
        public string Namespace { get; set; }

        /// <summary>
        /// Gets or sets the name of the XML.
        /// </summary>
        /// <value>The name of the XML.</value>
        public string XmlName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is generic collection.
        /// </summary>
        /// <value><c>true</c> if this instance is generic collection; otherwise, <c>false</c>.</value>
        public bool IsGenericCollection { get; set; }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool Equals(Field other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Type, other.Type) && XmlType == other.XmlType && string.Equals(Namespace, other.Namespace) && string.Equals(XmlName, other.XmlName);
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
            return Equals((Field)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Type != null ? Type.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)XmlType;
                hashCode = (hashCode * 397) ^ (Namespace != null ? Namespace.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (XmlName != null ? XmlName.GetHashCode() : 0);
                return hashCode;
            }
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Name: {0}, Type: {1}, XmlType: {2}, Namespace: {3}, XmlName: {4}", Name, Type, XmlType, Namespace, XmlName);
        }
    }
}
