using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int[] dir;
    public int currDir=0;

    public int sumDir=0;

    public CameraMovement cam;

    public static int speed=1;
    private bool started;
    public PlayerAnimator animator;
    private bool win;

    public PlayerUI ui;

    public ParticleSystem gemGet;
    private int totalCollsNumber=0;
    public Rigidbody rigidbody;

    public bool reverse=false;
    public int dx=-1;
    private int[] tempDir;
    public Text coins;

    void Start(){
        Application.targetFrameRate=50;

        speed=1;

        win=false;
        started=false;
        animator.sit();

        rigidbody=GetComponent<Rigidbody>();

        

        if(reverse){
            tempDir=new int[dir.Length+1];
            for( int i = 0; i < dir.Length; i++ )
            {
                tempDir[dir.Length - 1 - i ] = dir[ i ]*dx;
            }
            tempDir[dir.Length]=-1;
            dir=tempDir;
        }   

        coins.text=PlayerPrefs.GetInt("coins").ToString();

    }
    void Update(){
        if(started)transform.Translate(Vector3.forward/15*speed*Time.timeScale);

        if(Input.GetMouseButtonUp(0) && Time.timeScale==1){
            changeDirection();
        }

    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag=="Finish"){
            stopMove();
            animator.idle();

            win=true;

            ui.showWinPanel();
            PlayerPrefs.SetInt("level",Application.loadedLevel-1);
        }
        totalCollsNumber++;
    }
    void OnCollisionExit(Collision other){
        totalCollsNumber--;
        // if(totalCollsNumber==0){
        //     GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward*200);
        // }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag=="Respawn"){
            stopMove();
            animator.fall();

            ui.showDeathPanel();
        }
        if(other.gameObject.tag=="gem"){
            PlayerPrefs.SetInt("Coin@"+other.gameObject.name+"@"+Application.loadedLevelName,1);
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins")+5);
            coins.text=PlayerPrefs.GetInt("coins").ToString();

            gemGet.Play();
        }
    }

    void stopMove(){
        speed=0;
    }

    public void changeDirection(){
        
        if(!PlayerUI.canMove)return;

        if(!win){
            if(!started){
                started=true;
                animator.run();
            }
            else{
                if(dir[currDir]==2 || dir[currDir]==-2){
                    rigidbody.AddForce(Vector3.up*100, ForceMode.Impulse);
                    currDir+=2;
                }
                else if(dir[currDir]==3 || dir[currDir]==-3){
                    rigidbody.AddForce(Vector3.up*100, ForceMode.Impulse);
                    currDir+=1;
                }
                else{
                    animator.run();
                    // /StartCoroutine(changeCamPos());
                    transform.Rotate(0, dir[currDir]*90,0);
                    sumDir+=dir[currDir];

                    if(sumDir!=0){
                        cam.changeYPos=true;
                    }
                    else{
                        cam.changeYPos=false;
                    }
                    currDir++;
                }
            }
        }
    }


    IEnumerator changeCamPos(){
        while(cam.gameObject.transform.position.z!=gameObject.transform.position.z){
            cam.gameObject.transform.position=Vector3.MoveTowards(cam.gameObject.transform.position,
                                                        new Vector3(cam.gameObject.transform.position.x,
                                                                    cam.gameObject.transform.position.y,
                                                                    gameObject.transform.position.z), 0.5f);
            yield return new WaitForEndOfFrame();
        }
    }
}
