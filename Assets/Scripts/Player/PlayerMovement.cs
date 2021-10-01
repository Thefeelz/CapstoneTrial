using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerAcceleration = 10f;
    [SerializeField] float playerDeceleration = 10f;
    [SerializeField] float maxPlayerspeed = 10f;
    [SerializeField] float playerJumpPower = 10f;
    [SerializeField] float playerRotateSpeed = 60;
    [SerializeField] float playerStrafeSpeed = 10f;

    public bool isGrounded = true;
    float distanceToGround;

    Rigidbody rb;
    OverchargeAbilities overchargeAbilities;
    CharacterStats playerStats;
    PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distanceToGround = GetComponentInChildren<Collider>().bounds.extents.y;
        playerStats = GetComponent<CharacterStats>();
        overchargeAbilities = GetComponent<OverchargeAbilities>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForGrounded();
        GetUserInput();
    }

    void CheckForGrounded()
    {
        if(Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.1f ))
        {
            isGrounded = true;
        } else
            isGrounded = false;
    }
    void GetUserInput()
    {
        // Testing Purposes to restore health
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerStats.ReplenishHealth(10f);
        }
        // Character Movement (Checks for them to be Grounded)
        if (isGrounded)
        {
            // Move Forward
            if (Input.GetKey(KeyCode.W))
            {
                MoveForward();
            }
            // Move Backwards
            else if (Input.GetKey(KeyCode.S))
            {
                MoveBackwards();
            }
            // Decelerate the Character
            else if (rb.velocity.z != 0)
            {
                Decelerate();
            }
            // Character Jump
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            // Strafe the Character
            if(Input.GetKeyDown(KeyCode.E))
            {
                rb.AddRelativeForce(Vector3.right * playerStrafeSpeed, ForceMode.Impulse);
            }
            else if (Input.GetKeyDown(KeyCode.Q))
            {
                rb.AddRelativeForce(Vector3.right * -playerStrafeSpeed, ForceMode.Impulse);
            }
        }

        // Rotate the Character
        if(Input.GetKey(KeyCode.D))
        {
            RotateCharacter(1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            RotateCharacter(-1);
        }

        // Ensures player is not moving faster than desired
        LockCharacterSpeed();

        // Overcharge Abilities (In Air Allowed Abilities)
        if(!isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift) && (playerStats.GetPlayerOvercharge() > overchargeAbilities.Get_OverchargeAbility_AirDash_Cost()))
            {
                playerStats.RemoveHealth(overchargeAbilities.Get_OverchargeAbility_AirDash_Cost());
                overchargeAbilities.OverchargeAbility_AirDash();
            }
        }
        else /// (On Ground Allowed Abilities)
        {
            if(Input.GetKeyDown(KeyCode.Alpha2) && (playerStats.GetPlayerOvercharge() > overchargeAbilities.Get_OverchargeAbility_SuperSlash_Cost()) && !playerAttack.GetSuperSlashStatus())
            {
                playerAttack.SetStartSuperSlash();
                // overchargeAbilities.OverchargeAbility_SuperSlash();
            }
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * playerJumpPower, ForceMode.VelocityChange);
    }

    private void Decelerate()
    {
        float currentX = rb.velocity.x;
        float currentZ = rb.velocity.z;
        if(currentX > 0)
        {
            currentX -= Mathf.Sqrt(currentX) * Time.deltaTime * playerDeceleration;
        }
        else if (currentX < 0)
        {
            currentX += Mathf.Sqrt(-currentX) * Time.deltaTime * playerDeceleration;
        }

        if (currentZ > 0)
        {
            currentZ -= Mathf.Sqrt(currentZ) * Time.deltaTime * playerDeceleration;
        }
        else if (currentZ < 0)
        {
            currentZ += Mathf.Sqrt(-currentZ) * Time.deltaTime * playerDeceleration;
        }
        rb.velocity = new Vector3(currentX, rb.velocity.y, currentZ);
    }
    private void RotateCharacter(int rotationDirection)
    {
        rb.rotation = rb.rotation * Quaternion.Euler(0, playerRotateSpeed * rotationDirection * Time.deltaTime, 0);
    }

    private void MoveForward()
    {
        rb.AddRelativeForce(Vector3.forward * playerAcceleration * Time.deltaTime, ForceMode.VelocityChange);
    }
    private void MoveBackwards()
    {
        rb.AddRelativeForce(Vector3.back * playerAcceleration * Time.deltaTime, ForceMode.VelocityChange);
    }
    private void LockCharacterSpeed()
    {
        if (rb.velocity.z > maxPlayerspeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxPlayerspeed);
        } 
        else if (-rb.velocity.z > maxPlayerspeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -maxPlayerspeed);
        }

        if (rb.velocity.x > maxPlayerspeed)
        {
            rb.velocity = new Vector3(maxPlayerspeed, rb.velocity.y, rb.velocity.z);
        }
        else if (-rb.velocity.x > maxPlayerspeed)
        {
            rb.velocity = new Vector3(-maxPlayerspeed, rb.velocity.y, rb.velocity.z);
        }

    }


}
