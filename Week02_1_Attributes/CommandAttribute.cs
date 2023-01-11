using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week02_1_Attributes
{
    /// <summary>
    /// Represents a command attribute.
    /// </summary>
    // indicate that we only want our CommandAttribute to be applied
    // to methods, and we want multiple instances of our CommandAttribute to be applied
    // to allow for supporting multiple commands on a given method
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class CommandAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public CommandAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }

}
