using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressCleaner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("progress_cleaned", 0) == 0){
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("progress_cleaned", 1);
        }
    }
}
