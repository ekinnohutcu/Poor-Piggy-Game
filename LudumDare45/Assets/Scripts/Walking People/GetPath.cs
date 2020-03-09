using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPath : MonoBehaviour
{
    public GameObject[] allPaths;
    void Awake()
    {
        int num = Random.Range(0, allPaths.Length);
        transform.position = allPaths[num].transform.position;
        MoveOnEditorPath yourPath = GetComponent<MoveOnEditorPath>();
        yourPath.pathName = allPaths[num].name;
    }
}
