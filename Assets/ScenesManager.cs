using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider loadingSlider;
    IEnumerator loadAsync(int id)
    {
        AsyncOperation operation = Application.LoadLevelAsync(id);
        loadingPanel.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            yield return null;

        }
    }

    
    public void openScene(int id){
        
        //PlayerUI.addCnt++;
        Time.timeScale=1;
        PlayerUI.canMove = true;
        StartCoroutine(loadAsync(id));
    }






    
}
