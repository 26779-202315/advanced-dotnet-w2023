using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week02_1_Attributes
{
    /// <summary>
    /// Represents a custom attribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    // declare our custom attribute class, by inheriting from System.Attribute.
    // we use the AttributeUsage class to specify where our attribute can be applied
    // by using the AttributeTargets enum, we specify that
    // our attribute can only be applied to classes
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class MyCustomAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyCustomAttribute"/> class.
        /// </summary>
        public MyCustomAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MyCustomAttribute"/> class.
        /// </summary>
        /// <param name="myValue">My value.</param>
        public MyCustomAttribute(string myValue)
        {
            this.MyValue = myValue;
        }

        /// <summary>
        /// Gets or sets my value.
        /// </summary>
        /// <value>My value.</value>
        public string MyValue { get; set; }
    }
}
