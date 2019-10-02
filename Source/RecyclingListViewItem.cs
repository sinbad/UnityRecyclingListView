using System;
using UnityEngine;

/// <summary>
/// You should subclass this to provide fast access to any data you need to populate
/// this item on demand.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class RecyclingListViewItem : MonoBehaviour {

    private RectTransform rectTransform;
    public RectTransform RectTransform {
        get {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            return rectTransform;
        }
    }

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }
    
    
}
