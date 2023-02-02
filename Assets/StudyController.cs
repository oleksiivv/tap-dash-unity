using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyController : MonoBehaviour
{
    public GameObject studyPanel;


    void Start(){
        if(PlayerPrefs.GetInt("studied",-1)==-1){
            studyPanel.SetActive(true);
        }
        else{
            studyPanel.SetActive(false);
        }
    }

    void Update(){
        if(Input.GetMouseButtonUp(0) && PlayerPrefs.GetInt("studied",-1)==-1){
            PlayerPrefs.SetInt("studied",1);
            studyPanel.SetActive(false);
        }
    }
}
