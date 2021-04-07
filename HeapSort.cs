using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapSort : MonoBehaviour {
    public GameManager gm;
    public GameObject[] nodeArray;
    public int n;
    public int currentIndex=0;
    public int tempIndex=0;
    bool added = false;
    bool heaped = false;

    // Update is called once per frame
    void Update() {
        //Make each node uniform before applying step
        for (int i = 0; i < nodeArray.Length; i++) {
            GameObject node = nodeArray[i];
            float width = 16f / n;
            node.transform.position = Vector3.right * (-8f + (width / 2) + (width * i));
            if (gm.compareType == "Height") node.GetComponent<SpriteRenderer>().color = Color.white;
            if (gm.compareType == "Color") node.transform.localScale = new Vector3(node.transform.localScale.x, 9, 1);
        }
        if (!heaped) {
            if (ToBranch(currentIndex)) {
                int newIndex = getParent(currentIndex);
                Swap(currentIndex, getParent(currentIndex));
                currentIndex = newIndex;
            } else {
                tempIndex++;
                currentIndex = tempIndex;
            }
            if (tempIndex == n) {
                heaped = true;
                tempIndex--;
                currentIndex = 0;
            }
        } else if(added){
            if (ToLeaf(currentIndex)) {
                int newIndex = GetChild(currentIndex);
                Swap(currentIndex, newIndex);
                currentIndex = newIndex;
            } else {
                added = false;
                currentIndex = 0;
            }
        } else {
            if (tempIndex == -1) return;
            Swap(currentIndex, tempIndex);
            tempIndex--;
            added = true;
        }
    }

    void Swap(int index1, int index2) {
        GameObject temp = nodeArray[index1];
        nodeArray[index1] = nodeArray[index2];
        nodeArray[index2] = temp;
    }

    int getParent(int index) {
        return (index - 1) / 2;
    }
    int GetChild(int index) {
        if (tempIndex < (index * 2) + 2) return (2 * index) + 1;
        return gm.Compare(nodeArray[(2 * index) + 1], nodeArray[(2 * index) + 2]) ?
            (2 * index) + 1 :
            (2 * index) + 2;
    }
    bool ToLeaf(int index) {
        if (tempIndex < (index * 2) + 1) return false;
        bool testChild = gm.Compare(nodeArray[GetChild(index)],nodeArray[index]);
        return testChild;
    }
    bool ToBranch(int index) {
        bool testChild = gm.Compare(nodeArray[index], nodeArray[getParent(index)]); 
        return testChild;
    }

}
