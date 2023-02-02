using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateButton : MonoBehaviour
{
    public void rate(){
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.EasyStreet.TapTapRun");
    }
}
