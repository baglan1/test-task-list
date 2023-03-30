using Newtonsoft.Json;

namespace List.Model
{
    /// <summary>
    /// Element of a custom list.
    /// </summary>
    public class MyElement
    {
        // private fields
        string text;
        int number;

        // properties
        public string Text
        {
            get
            {
                return this.text;
            }
        }

        public int Number
        {
            get
            {
                return number;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">Text of the element.</param>
        /// <param name="number">Number of the element.</param>
        [JsonConstructor]
        public MyElement(string text, int number)
        {
            this.text = text;
            this.number = number;
        }
    }
}