using System.Collections.Generic;
using List.Model;
using List.Utility;
using TMPro;
using UnityEngine;

namespace List.View
{
    /// <summary>
    /// View component of the list.
    /// </summary>
    public class ListView : MonoBehaviour
    {
        [SerializeField] GameObject elementPrefab;
        [SerializeField] Transform content;
        [SerializeField] DropZone dropZone;
        [SerializeField] TMP_Text titleTextComp;

        List<ElementView> elementViews = new List<ElementView>();

        MyList list;

        void Start()
        {
            dropZone.OnEnterEvent.AddListener(OnEnterCallback);
            dropZone.OnLeaveEvent.AddListener(OnExitCallback);
        }

        /// <summary>
        /// Sets the modeland updates the view.
        /// </summary>
        /// <param name="list">Model of the view.</param>
        public void SetList(MyList list)
        {
            RemoveAll();

            foreach (var element in list)
            {
                AddElement(element);
            }

            this.list = list;
            UpdateName();
        }

        void AddElement(MyElement element)
        {
            var elementGo = Instantiate(elementPrefab, content);

            var elementView = elementGo.GetComponent<ElementView>();
            elementView.SetElement(element);

            elementViews.Add(elementView);
        }

        void RemoveAll()
        {
            if (elementViews is null) return;

            for (int i = elementViews.Count - 1; i >= 0; i--)
            {
                var elementView = elementViews[i];
                elementViews.RemoveAt(i);

                Destroy(elementView.gameObject);
            }
        }

        /// <summary>
        /// Sorts the list elements by the text.
        /// </summary>
        /// <param name="ascending">Flag which indicates whether the elements are sorted
        /// in ascending order or not.
        ///</param>
        public void SortByText(bool ascending)
        {
            list.SortByText(ascending);

            SetList(list);
        }

        /// <summary>
        /// Sorts the list elements by the number.
        /// </summary>
        /// <param name="ascending">Flag which indicates whether the elements are sorted
        /// in ascending order or not.
        ///</param>
        public void SortByNumber(bool ascending)
        {
            list.SortByNumber(ascending);

            SetList(list);
        }

        void OnEnterCallback(ElementView elementView)
        {
            if (elementViews.Contains(elementView)) return;

            elementViews.Add(elementView);
            list.Add(elementView.Element);

            UpdateName();
        }

        void OnExitCallback(ElementView elementView)
        {
            if (!elementViews.Contains(elementView)) return;

            elementViews.Remove(elementView);
            list.Remove(elementView.Element);

            UpdateName();
        }

        void UpdateName()
        {
            if (this.list is null)
            {
                titleTextComp.text = string.Empty;
                return;
            }

            var title = list.Name + $" ({list.Count})";
            titleTextComp.text = title;
        }

        /// <summary>
        /// Called on export button click.
        /// </summary>
        public void OnExportClick()
        {
            JsonImportExportUtility.ExportList(list);
        }

        /// <summary>
        /// Called on import button click.
        /// </summary>
        public void OnImportClick()
        {
            JsonImportExportUtility.ImportList(SetList);
        }
    }
}