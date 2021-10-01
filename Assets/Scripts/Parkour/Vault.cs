using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vault : MonoBehaviour
{
    [SerializeField] float vaultSpeed = 100f;
    [SerializeField] GameObject playerBody;
    PlayerMovement ourPlayer;
    Rigidbody ourPlayerRigidBody;
    Transform entryTransform;
    [SerializeField] Camera firstPersonCam;
    [SerializeField] Camera thirdPersonCam;
    bool startedVault = false;
    private void Start()
    {
        ourPlayer = FindObjectOfType<PlayerMovement>();
        ourPlayerRigidBody = ourPlayer.GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Body"))
        {         
            if (!startedVault)
            {
                thirdPersonCam.gameObject.SetActive(true);
                firstPersonCam.gameObject.SetActive(false);
                startedVault = true;
                entryTransform = ourPlayer.transform;
                ourPlayer.GetComponent<Rigidbody>().AddForce(Vector3.forward * vaultSpeed, ForceMode.Impulse);
                playerBody.transform.rotation = Quaternion.Euler(-75, ourPlayer.transform.rotation.eulerAngles.y, ourPlayer.transform.rotation.eulerAngles.z);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.transform.CompareTag("Body"))
            ourPlayerRigidBody.velocity = new Vector3(ourPlayerRigidBody.velocity.x, ourPlayerRigidBody.velocity.y, 25f);
        //ourPlayer.transform.rotation = Quaternion.Euler(-75, ourPlayer.transform.rotation.eulerAngles.y, ourPlayer.transform.rotation.eulerAngles.z);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Body"))
        {
            startedVault = false;
            playerBody.transform.rotation = Quaternion.Euler(0, ourPlayer.transform.rotation.eulerAngles.y, ourPlayer.transform.rotation.eulerAngles.z);
            firstPersonCam.gameObject.SetActive(true);
            thirdPersonCam.gameObject.SetActive(false);
        }
    }
}
