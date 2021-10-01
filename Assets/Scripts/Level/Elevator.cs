using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform topOfShaft;
    [SerializeField] Transform bottomOfShaft;
    [SerializeField] GameObject elevatorFloor;
    [SerializeField] float elevatorRideTime;
    [SerializeField] Door elevatorDoor;
    float timeSinceStart = 0f;

    bool startDecent = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startDecent)
        {
            StartElevatorDecent();
        }
    }

    void StartElevatorDecent()
    {
        timeSinceStart += Time.deltaTime;
        if (timeSinceStart < elevatorRideTime)
        {
            elevatorFloor.transform.position = Vector3.Lerp(topOfShaft.position, bottomOfShaft.position, timeSinceStart / elevatorRideTime);
        }
        else
        {
            elevatorDoor.SetOpenDoor();
            timeSinceStart = 0;
            startDecent = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Body"))
        {
            startDecent = true;
            // other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Body"))
        {
            // other.transform.SetParent(null);
        }
    }
}
