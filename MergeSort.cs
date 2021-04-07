using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MergeSort : MonoBehaviour {
    public GameManager gm;
    public GameObject[] nodeArray;
    public float[] heightArray;
    public Color[] colorArray;
    public int n;

    int indexA = 0;
    int indexB = 1;
    int arrayIndex = 0;
    int arraySize = 1;

    int k = 0;


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

        if (arraySize < n) {
            if (arrayIndex < n - arraySize) {
                if (indexA >= arraySize) {
                    GameObject nodeB = nodeArray[indexB + arrayIndex];
                    indexB++;
                    heightArray[k] = nodeB.transform.localScale.y;
                    colorArray[k] = nodeB.GetComponent<SpriteRenderer>().color;
                }
                else if (indexB >= Mathf.Min(2 * arraySize, n - arrayIndex)) {
                    GameObject nodeA = nodeArray[indexA + arrayIndex];
                    indexA++;
                    heightArray[k] = nodeA.transform.localScale.y;
                    colorArray[k] = nodeA.GetComponent<SpriteRenderer>().color;
                } else {
                    GameObject nodeA = nodeArray[indexA + arrayIndex];
                    GameObject nodeB = nodeArray[indexB + arrayIndex];
                    if (gm.Compare(nodeB, nodeA)) {
                        indexA++;
                        heightArray[k] = nodeA.transform.localScale.y;
                        colorArray[k] = nodeA.GetComponent<SpriteRenderer>().color;
                    } else {
                        indexB++;
                        heightArray[k] = nodeB.transform.localScale.y;
                        colorArray[k] = nodeB.GetComponent<SpriteRenderer>().color;
                    }
                }
                k++;
                
                if (indexA >= arraySize && indexB >= Mathf.Min(2 * arraySize, n - arrayIndex)) {
                    arrayIndex += 2 * arraySize;
                    indexA = 0;
                    indexB = arraySize;
                }
            } else {
                arrayIndex = 0;
                arraySize *= 2; 
                indexA = 0;
                indexB = arraySize;
                switch (gm.compareType) {
                    case "Height":
                        for(int i = 0; i<k; i++) {
                            nodeArray[i].transform.localScale = new Vector3(nodeArray[i].transform.localScale.x, heightArray[i], 1);
                        }
                        heightArray = new float[n];
                        break;
                    case "Color":
                        for (int i = 0; i < k; i++) {
                            nodeArray[i].GetComponent<SpriteRenderer>().color = colorArray[i];
                        }
                        colorArray = new Color[n];
                        break;
                    default:
                        break;
                }
                k = 0;
            }
        }
    }
}
