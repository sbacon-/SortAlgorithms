using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
    public GameObject nodePrefab;
    int n = 20;
    float max = 0f, min =1f;
    GameObject[] gArray;
    GameObject nodes;
    public string compareType = "Height";
    public string sortType = "BubbleSort";

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel(n);
    }

    private void GenerateLevel(int n) {
        nodes = new GameObject("Nodes");
        gArray = new GameObject[n];
        for(int i = 0; i<n; i++) {
            float rHeight = Random.Range(0.2f,1);
            float width = 16f / n; 
            if (rHeight > max) max = rHeight;
            if (rHeight < min) min = rHeight;
            gArray[i] = Instantiate(nodePrefab);

            gArray[i].transform.position = Vector3.right * (-8f + (width / 2) + (width * i));
            gArray[i].transform.localScale = new Vector3(width, rHeight*9, 1);

            gArray[i].GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
            
            gArray[i].name = "Node - " + i;

            gArray[i].transform.parent = nodes.transform;

            
        }
        foreach(GameObject g in gArray) g.transform.localScale = g.transform.localScale + Vector3.up * (9 - (max * 9));

        switch (sortType) {
            case "BubbleSort":
                BubbleSort bubbleSort = nodes.AddComponent<BubbleSort>();
                bubbleSort.gm = this;
                bubbleSort.nodeArray = gArray;
                bubbleSort.n = n;
                bubbleSort.lastIndex = n - 1;
                break;
            case "SelectionSort":
                SelectionSort selectionSort = nodes.AddComponent<SelectionSort>();
                selectionSort.gm = this;
                selectionSort.nodeArray = gArray;
                selectionSort.n = n;
                break;
            case "InsertionSort":
                InsertionSort insertionSort = nodes.AddComponent<InsertionSort>();
                insertionSort.gm = this;
                insertionSort.nodeArray = gArray;
                insertionSort.n = n;
                break;
            case "MergeSort":
                MergeSort mergeSort = nodes.AddComponent<MergeSort>();
                mergeSort.gm = this;
                mergeSort.nodeArray = gArray;
                mergeSort.n = n;
                mergeSort.heightArray = new float[n];
                mergeSort.colorArray = new Color[n];
                break;
            case "QuickSort":
                QuickSort quickSort = nodes.AddComponent<QuickSort>();
                quickSort.gm = this;
                quickSort.nodeArray = gArray;
                quickSort.n = n;
                quickSort.pivot = gArray[Random.Range(0, n)];
                quickSort.sortBool = new bool[n];
                quickSort.lIndex = 0;
                quickSort.uIndex = n - 1;
                for (int i = 0; i < n; i++) quickSort.sortBool[i] = false;
                break;
            case "HeapSort":
                HeapSort heapSort = nodes.AddComponent<HeapSort>();
                heapSort.gm = this;
                heapSort.nodeArray = gArray;
                heapSort.n = n;
                break;
            default:
                break;
        }
    }

    public bool Compare(GameObject currentNode, GameObject nextNode, bool equal) {
        SpriteRenderer currentColor = currentNode.GetComponent<SpriteRenderer>();
        SpriteRenderer nextColor = nextNode.GetComponent<SpriteRenderer>();
        switch (compareType) {
            case "Height":
                currentColor.color = Color.gray;
                nextColor.color = Color.gray;
                return equal? 
                    currentNode.transform.localScale.y >= nextNode.transform.localScale.y:
                    currentNode.transform.localScale.y > nextNode.transform.localScale.y;
            case "Color":
                currentNode.transform.localScale = new Vector3(currentNode.transform.localScale.x, 10, 1);
                nextNode.transform.localScale = new Vector3(nextNode.transform.localScale.x, 10, 1);
                float hue1,hue2;
                Color currentRGB = currentColor.color;
                Color nextRGB = nextColor.color;
                Color.RGBToHSV(currentRGB, out hue1, out _, out _);
                Color.RGBToHSV(nextRGB, out hue2, out _, out _);
                return equal ?
                    hue1 >= hue2:
                    hue1 > hue2;
            default:
                return false;
        }
    }
    public bool Compare(GameObject cN, GameObject nN) {
        return Compare(cN, nN, false);
    }
    public void UpdateN() {
        n = (int)GameObject.Find("Slider").GetComponent<Slider>().value;
        GameObject.Find("nText").GetComponent<TextMeshProUGUI>().text = "n=" + n;
    }
    public void UpdateSort() {
        sortType = GameObject.Find("SortType").GetComponent<Text>().text;
        GenButton();
    }
    public void UpdateCompareToggle() {
        compareType = GameObject.Find("CompareToggle").GetComponent<Toggle>().isOn ? "Color" : "Height";
        GenButton();
    }

    public void GenButton() {
        GameObject.Destroy(nodes);
        GenerateLevel(n);
    }

}

