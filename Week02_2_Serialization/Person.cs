using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


namespace Week02_2_Serialization
{
    /// <summary>
    /// Represents a person.
    /// </summary>
    [XmlRoot]
    [XmlType]
    [JsonObject]
    [Serializable]
    public class Person
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Person" /> class.
        /// </summary>
        public Person()
        {

        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlElement]
        [JsonProperty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [XmlElement]
        [JsonProperty]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>The date of birth.</value>
        [XmlIgnore]
        [JsonIgnore]
        public DateTimeOffset DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the date of birth XML.
        /// </summary>
        /// <value>The date of birth XML.</value>
        [XmlElement("DOB")]
        [JsonProperty("DOB")]
        public string DateOfBirthXml
        {
            get
            {
                return this.DateOfBirth.ToString("o");
            }
            set
            {
                this.DateOfBirth = DateTimeOffset.Parse(value);
            }
        }
    }

}
