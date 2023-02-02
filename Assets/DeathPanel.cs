using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPanel : MonoBehaviour
{
    public GameObject[] elements;
    void Start(){
        Invoke(nameof(show),1f);
    }
    void show(){
        foreach(var el in elements)el.SetActive(true);
    }
}
