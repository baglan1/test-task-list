using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace List.Model
{
    /// <summary>
    /// Custom list class.
    /// </summary>
    [JsonObject]
    public class MyList : IEnumerable<MyElement>
    {
        // private fields
        [JsonProperty]
        string name;

        [JsonProperty]
        List<MyElement> list = new List<MyElement>();

        // properties
        [JsonIgnore]
        public string Name
        {
            get
            {
                return name;
            }
        }

        [JsonIgnore]
        public int Count
        {
            get
            {
                if (list is null) return 0;

                return list.Count;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the list.</param>
        public MyList(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Json constructor of the list.
        /// </summary>
        /// <param name="name">Name of the list.</param>
        /// <param name="list">Members of the list.</param>
        [JsonConstructor]
        public MyList(string name, List<MyElement> list)
        {
            this.name = name;
            this.list = list;
        }

        /// <summary>
        /// Method for adding an element.
        /// </summary>
        /// <param name="element">New element.</param>
        public void Add(MyElement element)
        {
            if (list.Contains(element))
                throw new ArgumentException($"{nameof(element)} already exists in the list.");

            list.Add(element);
        }

        /// <summary>
        /// Method for removing an element.
        /// </summary>
        /// <param name="element">Element that exists in the list.</param>
        public void Remove(MyElement element)
        {
            if (!list.Contains(element))
                throw new ArgumentException($"{nameof(element)} does not exist in the list.");

            list.Remove(element);
        }

        /// <inheritdoc />
        public IEnumerator<MyElement> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        /// <summary>
        /// Sorts the list elements by the text.
        /// </summary>
        /// <param name="ascending">Flag which indicates whether the elements are sorted
        /// in ascending order or not.
        ///</param>
        public void SortByText(bool ascending)
        {
            if (ascending)
                list = list.OrderBy(x => x.Text).ToList();
            else
                list = list.OrderByDescending(x => x.Text).ToList();
        }

        /// <summary>
        /// Sorts the list elements by the number.
        /// </summary>
        /// <param name="ascending">Flag which indicates whether the elements are sorted
        /// in ascending order or not.
        ///</param>
        public void SortByNumber(bool ascending)
        {
            if (ascending)
                list = list.OrderBy(x => x.Number).ToList();
            else
                list = list.OrderByDescending(x => x.Number).ToList();
        }
    }
}