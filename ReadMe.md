
# RecyclingListView for Unity

> #### Note: I have stopped using Unity and so am not maintaining this library any more.

## What is this for?

One way of making a list is simply to create every item in the list and add it to
the content view of a Unity ScrollRect, perhaps with a ContentFitter to automatically
expand it, and a VerticalLayoutGroup to position them. There's one problem with
this: it gets *really* slow at high volumes.

Most UI systems don't do this under the hood; instead they only allocate just enough
visual resources to cover the visible area (plus a safety area), and then
recycle the child views as you scroll - views which drop out of view are re-used
for the parts that are just coming into view. This way you have a fixed amount
of overheads regardless of how big your list is. 

This repo contains a smallest-possible implementation of this concept, sometimes
called "virtualising" a list or just "recycling". Basically you keep the
"model" which contains all of the data, and the list calls you back to ask you
to provide details of a specific element when it needs it, because it's coming
into view.

![Example Image](demo.gif)

## How to use it


### The Demo

You can drag this whole repo into Unity Assets to provide a 
quick demo of this so you can pick it apart yourself, just load *Scenes/RecyclingList*.

### How it works

* You have a list of items (just data, no view), this is your Model. This can be on any class.
* You create a ScrollRect (Scroll View in the menu) and add a RecyclingListView to it
* You create a prefab which is a child item in your list (the View item). It must have a component of RecyclingListViewItem on it.
* You give the RecyclingListView a reference to that prefab; it pre-allocates them and keeps them in a pool
* You set a delegate on RecyclingListView which is the method it calls when one of these
items needs populating with data
* You simply tell the RecyclingListView the RowCount of your Model. It then calls the delegate for any of those items in view, giving you a row index to refer to.
* You can tell the view that the model has been updated (without changing its size) via Refresh if need be (either completely or for a subset)


### Step by step

1. Create a Canvas
2. Create a Scroll View underneath
3. Tune how you like (I removed the horizontal scroll bar)
4. Add RecyclingListView component to the scroll view
5. Create a child panel which will go inside the scroll view, temporarily inside Content of scroll view (just so you can get it looking like you want)
6. Create a subclass of RecyclingListViewItem to hold whatever data you need for this view
6. Add your RecyclingListViewItem subclass as a component to the root of the child
7. Make a prefab out of this child panel, then delete it from content
8. Select the scroll view and set the Child Prefab on RecyclingListView to your prefab
9. Create your component which will contain the model, somewhere. It doesn't matter where, so long as it has a reference to the RecyclingListView
10. Your model component should set RecyclingListView.ItemCallback, and set RecyclingListView.RowCount. The rest will happen mostly automatically as "How it works" above.
11. Change RowCount when you alter the length of the list, or Clear() and Refresh() to
    empty or update content in-place.


## Limitations

This is not a general purpose grid view like you'll find in the Asset Store, it's just
for lists. Specifically:

* Only one type of child panel is supported
* The child panel has to be a fixed height
* Horizontal scrolling can still be used, but virtualisation is only done vertically

## Tips

### Pre-Allocate More Items

The list handles dynamic resizing, but if you know the max height your content 
view can ever be, and that's likely to be bigger than the default size, you can
avoid some reallocation by specifying that height in the "Pre Alloc Height"
property on the list view component.

## License (MIT)

Copyright (c) 2019 Steve Streeting

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
