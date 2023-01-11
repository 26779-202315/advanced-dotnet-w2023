using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week02_1_Attributes
{
    /// <summary>
    /// Represents a help attribute.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HelpAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpAttribute"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        public HelpAttribute(string content)
        {
            this.Content = content;
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; }
    }
}
