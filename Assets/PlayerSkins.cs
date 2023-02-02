using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkins : MonoBehaviour
{
    public GameObject[] skins;

    public PlayerAnimator animator;

    void Start(){
        foreach(var skin in skins)skin.SetActive(false);

        skins[PlayerPrefs.GetInt("current",0)].SetActive(true);
        animator.anim=skins[PlayerPrefs.GetInt("current",0)].GetComponent<Animator>();
    }
}
