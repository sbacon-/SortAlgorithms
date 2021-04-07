using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSort : MonoBehaviour
{
    public GameManager gm;
    public GameObject[] nodeArray;
    public int n;
    public int lastIndex;
    int currentIndex = 0;

    // Update is called once per frame
    void Update() {
        //Make each node uniform before applying step
        for (int i = 0; i < nodeArray.Length; i++) {
            GameObject node = nodeArray[i];
            float width = 16f / n;
            node.transform.position = Vector3.right * (-8f + (width / 2) + (width * i));
            if (gm.compareType == "Height") node.GetComponent<SpriteRenderer>().color = Color.white;
            if (gm.compareType == "Color") node.transform.localScale = new Vector3(node.transform.localScale.x, 9,1);
        }
        if (lastIndex == 0) return;

        GameObject currentNode = nodeArray[currentIndex];
        GameObject nextNode = nodeArray[currentIndex+1];
        if (gm.Compare(currentNode,nextNode)) {
            nodeArray[currentIndex] = nextNode;
            nodeArray[currentIndex + 1] = currentNode;
        }
        currentIndex++;

        if (currentIndex == lastIndex) {
            currentIndex = 0;
            lastIndex--;
        }
    }
}
