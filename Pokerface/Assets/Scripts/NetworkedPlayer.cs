using UnityEngine;
using System.Collections;

public class NetworkedPlayer : Photon.MonoBehaviour {

	// Use this for initialization
    
    public GameObject avatar;
    public Transform playerGlobal;
    public Transform playerLocal;
	
    void Start () {
	
        Debug.Log("I'm instantiated!");
        
        
        
	}
    // 
    void Update(){
                
 
        
        if (photonView.isMine)
        {
        Debug.Log("player is mine");


        
       // avatar.SetActive(false);
        
        }
      
    }
	
	// Update is called once per frame
	void onPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
        
        if(stream.isWriting)
        {
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);
        }	
        else {
            
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
            
        }
	}
}
