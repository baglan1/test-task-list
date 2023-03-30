using List.Model;
using List.View;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    [SerializeField] ListView[] listViews;

    // Start is called before the first frame update
    void Start()
    {
        PopulateWithSeeds();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PopulateWithSeeds() {
        if (listViews is not null) {
            var listNames = new string[] {
                "Groceries", "Products", "Fruits"
            };

            foreach(var list in listViews) {
                var index = Random.Range(0, listNames.Length);
                var myList = GetSeedList(listNames[index]);

                list.SetList(myList);
            }
        }
    }

    MyList GetSeedList(string name) {
        var texts = new string[] {
            "apple", "banana", "peanut", "tangerine", "orange"
        };

        var myList = new MyList(name);
        var elementCount = Random.Range(5, 10);
        
        for (int i = 0; i < elementCount; i++) {
            var index = Random.Range(0, texts.Length);
            var element = new MyElement(texts[index], Random.Range(0, 10));

            myList.Add(element);
        }

        return myList;
    }
}
