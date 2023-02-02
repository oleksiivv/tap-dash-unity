using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement playerMovement;
    private Vector3 offset;

    public bool changeYPos=false;

    void Start(){
        offset=player.transform.position-transform.position;
        playerMovement=player.GetComponent<PlayerMovement>();
    }

    void Update(){
     
        Vector3 relativePos = player.transform.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,rotation,22.5f);

        //transform.localRotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.eulerAngles.x,player.transform.eulerAngles.y,transform.eulerAngles.z), 2);

        if(!changeYPos){
            transform.position=Vector3.MoveTowards(transform.position,player.transform.position-offset,0.1f);
        }
    }

    IEnumerator goBack(){
        int startX=(int)player.transform.position.x;
        transform.position=player.transform.position-offset;
        changeYPos=false;
        while(true){

            if(playerMovement.sumDir==-1){
                gameObject.transform.position=Vector3.MoveTowards(transform.position,
                                                            new Vector3(player.transform.position.x, 12,player.transform.position.z+5), 0.4f);
            }
            else if(playerMovement.sumDir==0){
                transform.position=Vector3.MoveTowards(transform.position,player.transform.position-offset,0.1f);
            }
            else{
                transform.position=Vector3.MoveTowards(transform.position,
                                                            new Vector3(player.transform.position.x-5, 12,player.transform.position.z),0.1f);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
