using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    Rigidbody player;
    bool yeetFromWall = false;
    float wallRunTime = 1f;
    float timeSinceStart;
    float intialHeight;
    CharacterStats characterStats;
    // Start is called before the first frame update
    void Start()
    {
        characterStats = FindObjectOfType<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(yeetFromWall)
        {
            WallRunEffect();
        }
    }
    
    void WallRunEffect()
    {
        timeSinceStart += Time.deltaTime;
        if(timeSinceStart < wallRunTime)
        {
            //player.transform.position = new Vector3(player.transform.position.x, intialHeight, player.transform.position.z);
            player.useGravity = false;
            characterStats.SetThirdPersonCam();
        }
        else
        {
            player.useGravity = true;
            timeSinceStart = 0;
            yeetFromWall = false;
            characterStats.SetFirstPersonCam();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Body"))
        {
            Debug.Log("Yeetus Deeletus");
            yeetFromWall = true;
            player = other.GetComponentInParent<Rigidbody>();
        }
       
    }
    
}
