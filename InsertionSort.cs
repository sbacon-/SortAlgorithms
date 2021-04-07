using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InsertionSort : MonoBehaviour {
    public GameManager gm;
    public GameObject[] nodeArray;
    public int n;
    int currentIndex = 1;
    int compareIndex = 1;

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
        GameObject compareNode = nodeArray[compareIndex - 1];

        if(gm.Compare(compareNode,currentNode)) {
            nodeArray[compareIndex] = compareNode;
            nodeArray[compareIndex-1] = currentNode;
            compareIndex--;
        } else {
            compareIndex = 0;
        }
        if (compareIndex <= 0) {
            currentIndex++;
            compareIndex = currentIndex;
        }
    }
}
