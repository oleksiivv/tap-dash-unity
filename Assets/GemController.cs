using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Coin@"+gameObject.name+"@"+Application.loadedLevelName,-1)==1){
            Destroy(gameObject);
        }    
    }

    
}
