using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestList : MonoBehaviour
{
    List<float> formID = new List<float> { 81, 9, 21 };
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(formID[1]);
        Debug.Log(formID[2]);
        Debug.Log(formID[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
