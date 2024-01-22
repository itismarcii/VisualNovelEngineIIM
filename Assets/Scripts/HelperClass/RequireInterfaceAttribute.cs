using UnityEngine;

namespace HelperClass
{
    /// <summary>
    /// Attribute that require implementation of the provided interface.
    /// </summary>
    public class RequireInterfaceAttribute : PropertyAttribute
    {
        // Interface type.
        public System.Type RequiredType { get; private set; }

        /// <summary>
        /// Requiring implementation of the <see cref="T:HelperClass.RequireInterfaceAttribute"/> interface.
        /// </summary>
        /// <param name="type">Interface type.</param>
        public RequireInterfaceAttribute(System.Type type)
        {
            this.RequiredType = type;
        }
    }
}