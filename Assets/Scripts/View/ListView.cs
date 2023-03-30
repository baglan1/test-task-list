using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SimpleFileBrowser;
using TMPro;
using UnityEngine;

public class ListView : MonoBehaviour
{
    [SerializeField] GameObject elementPrefab; 
    [SerializeField] Transform content;
    [SerializeField] DropZone dropZone;
    [SerializeField] TMP_Text titleTextComp;

    List<ElementView> elementViews = new List<ElementView>();

    MyList list;

    void Start() {
        dropZone.OnEnterEvent.AddListener(OnEnterCallback);
        dropZone.OnLeaveEvent.AddListener(OnExitCallback);
    }

    public void SetList(MyList list) {
        RemoveAll();

        foreach(var element in list) {
            AddElement(element);
        }

        this.list = list;
        UpdateName();
    }

    void AddElement(MyElement element) {
        var elementGo = Instantiate(elementPrefab, content);

        var elementView = elementGo.GetComponent<ElementView>();
        elementView.SetElement(element);

        elementViews.Add(elementView);
    }

    void RemoveAll() {
        if (elementViews is null) return;

        for (int i = elementViews.Count - 1; i >= 0; i--) {
            var elementView = elementViews[i];
            elementViews.RemoveAt(i);

            Destroy(elementView.gameObject);
        }
    }

    public void SortByText(bool ascending) {
        list.SortByText(ascending);

        SetList(list);
    }

    public void SortByNumber(bool ascending) {
        list.SortByNumber(ascending);

        SetList(list);
    }

    void OnEnterCallback(ElementView elementView) {
        if (elementViews.Contains(elementView)) return;

        elementViews.Add(elementView);
        list.Add(elementView.Element);

        UpdateName();
    }

    void OnExitCallback(ElementView elementView) {
        if (!elementViews.Contains(elementView)) return;

        elementViews.Remove(elementView);
        list.Remove(elementView.Element);

        UpdateName();
    }

    void UpdateName() {
        if (this.list is null) {
            titleTextComp.text = string.Empty;
            return;
        }

        var title = list.Name + $" ({list.Count})";
        titleTextComp.text = title;
    }

    public void OnExportClick() {
        FileBrowser.DisplayedEntriesFilter += FileBrowserFilter;
        FileBrowser.ShowSaveDialog((paths) => { Export(paths.First()); }, ExportCancel, FileBrowser.PickMode.Files,
            false, null, list.Name + ".json");
    }

    public void OnImportClick() {
        FileBrowser.DisplayedEntriesFilter += FileBrowserFilter;
        FileBrowser.ShowLoadDialog((paths) => { Import(paths.First()); }, ImportCancel, FileBrowser.PickMode.Files);
    }

    void Export(string path) { 
        FileBrowser.DisplayedEntriesFilter -= FileBrowserFilter;

        var jsonString = JsonConvert.SerializeObject(list);

        File.WriteAllText(path, jsonString); 
        
    }

    void ExportCancel() {
        FileBrowser.DisplayedEntriesFilter -= FileBrowserFilter;

    }

    void Import(string path) {
        FileBrowser.DisplayedEntriesFilter -= FileBrowserFilter;
        var jsonString = File.ReadAllText(path);

        // TODO: check file structure
        var tempList = JsonConvert.DeserializeObject<MyList>(jsonString);

        SetList(tempList);
    }

    void ImportCancel() {
        FileBrowser.DisplayedEntriesFilter -= FileBrowserFilter;
    }

    bool FileBrowserFilter(FileSystemEntry entry) {
        if (entry.IsDirectory )
		    return true;

        if (entry.Name.EndsWith(".json"))
            return true;

        return false;
    }
}
