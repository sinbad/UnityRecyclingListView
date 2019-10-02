using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public struct TestChildData {
    public string Title;
    public string Note1;
    public string Note2;

    public TestChildData(string t, string n1, string n2) {
        Title = t;
        Note1 = n1;
        Note2 = n2;
    }
}

public class TestPanel : MonoBehaviour {
    public RecyclingListView theList;
    private List<TestChildData> data = new List<TestChildData>();

    private void Start() {
        theList.ItemCallback = PopulateItem;

        RetrieveData();

        // This will resize the list and cause callbacks to PopulateItem for
        // items that are needed for the view
        theList.RowCount = data.Count;
    }

    private void RetrieveData() {
        data.Clear();
        int row = 0;

        // You'd obviously load real data here
        string[] randomTitles = new[] {
            "Hello World",
            "This is fine",
            "You look nice today",
            "Recycling is good",
            "Why not",
            "Go outside",
            "And do something",
            "Less boring instead"
        };
        for (int i = 0; i < 200; ++i) {
            data.Add(new TestChildData(randomTitles[Random.Range(0, randomTitles.Length)], $"Row {row++}", Random.Range(0, 256).ToString()));
        }
    }

    private void PopulateItem(RecyclingListViewItem item, int rowIndex) {
        var child = item as TestChildItem;
        child.ChildData = data[rowIndex];
    }
    
}
