using System;
using System.IO;
using System.Linq;
using List.Model;
using Newtonsoft.Json;
using SimpleFileBrowser;

namespace List.Utility
{
    public static class JsonImportExportUtility
    {
        public static void ExportList(MyList list)
        {
            FileBrowser.DisplayedEntriesFilter += FileBrowserFilter;
            FileBrowser.ShowSaveDialog((paths) => { Export(paths.First(), list); }, ExportCancel,
                FileBrowser.PickMode.Files, false, null, list.Name + ".json");
        }

        public static void ImportList(Action<MyList> action)
        {
            FileBrowser.DisplayedEntriesFilter += FileBrowserFilter;
            FileBrowser.ShowLoadDialog((paths) => { Import(paths.First(), action); }, ImportCancel,
                FileBrowser.PickMode.Files);
        }

        static void Export(string path, MyList list)
        {
            FileBrowser.DisplayedEntriesFilter -= FileBrowserFilter;

            var jsonString = JsonConvert.SerializeObject(list);

            File.WriteAllText(path, jsonString);

        }

        static void ExportCancel()
        {
            FileBrowser.DisplayedEntriesFilter -= FileBrowserFilter;
        }

        static void Import(string path, Action<MyList> action)
        {
            FileBrowser.DisplayedEntriesFilter -= FileBrowserFilter;
            var jsonString = File.ReadAllText(path);

            var tempList = JsonConvert.DeserializeObject<MyList>(jsonString);

            action(tempList);
        }

        static void ImportCancel()
        {
            FileBrowser.DisplayedEntriesFilter -= FileBrowserFilter;
        }

        static bool FileBrowserFilter(FileSystemEntry entry)
        {
            if (entry.IsDirectory)
                return true;

            if (entry.Name.EndsWith(".json"))
                return true;

            return false;
        }
    }
}