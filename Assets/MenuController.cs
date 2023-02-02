using UnityEngine;
using UnityEngine.UI;

public class MenuController : ScenesManager
{
    public Text level;
    public Text coins;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("level",1)<23){
            level.text=PlayerPrefs.GetInt("level",1).ToString();
        }
        else{
            level.text="23";
        }

        coins.text=PlayerPrefs.GetInt("coins").ToString();
    }

    public void startGame(){
        if(PlayerPrefs.GetInt("level",1)+2<Application.levelCount){
            openScene(PlayerPrefs.GetInt("level",1)+2);
        }
        else{
            openScene(3);
        }
    }

    
}
