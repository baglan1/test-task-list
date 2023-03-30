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

        public void Add(string text, int num)
        {
            var newElement = new MyElement(text, num);

            list.Add(newElement);
        }

        public void Add(MyElement element)
        {
            list.Add(element);
        }

        public void Remove(MyElement element)
        {
            list.Remove(element);
        }

        public IEnumerator<MyElement> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public void SortByText(bool ascending)
        {
            if (ascending)
                list = list.OrderBy(x => x.Text).ToList();
            else
                list = list.OrderByDescending(x => x.Text).ToList();
        }

        public void SortByNumber(bool ascending)
        {
            if (ascending)
                list = list.OrderBy(x => x.Number).ToList();
            else
                list = list.OrderByDescending(x => x.Number).ToList();
        }
    }
}