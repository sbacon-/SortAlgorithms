using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionSort : MonoBehaviour {
    public GameManager gm;
    public GameObject[] nodeArray;
    public int n;
    int currentIndex = 0;
    int compareIndex = 0;
    int lowestIndex = 0;

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
        if (currentIndex == n) return;

        GameObject currentNode = nodeArray[compareIndex];
        GameObject nextNode = nodeArray[lowestIndex];
        if (gm.Compare(nextNode,currentNode)) {
            lowestIndex = compareIndex;
        }
        compareIndex++;
        if (compareIndex == n) {
            GameObject temp = nodeArray[currentIndex];
            nodeArray[currentIndex] = nodeArray[lowestIndex];
            nodeArray[lowestIndex] = temp;
            currentIndex++;
            compareIndex = currentIndex;
            lowestIndex = compareIndex;
        }
    }
}
