using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class MyElement
{
    string text;

    public string Text {
        get {
            return this.text;
        }
    }

    int number;

    public int Number {
        get {
            return number;
        }
    }

    [JsonConstructor]
    public MyElement(string text, int number) {
        this.text = text;
        this.number = number;
    }
}
