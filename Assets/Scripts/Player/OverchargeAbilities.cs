using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverchargeAbilities : MonoBehaviour
{
    Rigidbody myRigidbody;
    [SerializeField] int airDashPower;
    [SerializeField] int airDashCost;

    [SerializeField] int superSlashDamage;
    [SerializeField] float superSlashDamageMultiplier;
    [SerializeField] int superSlashCost;
    [SerializeField] int superSlashSpeed;
    [SerializeField] GameObject superSlashObject;
    CharacterStats playerStats;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        playerStats = GetComponent<CharacterStats>();
    }

    public void OverchargeAbility_AirDash()
    {
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.AddRelativeForce(new Vector3(0, 5, airDashPower), ForceMode.VelocityChange);
    }
    public int Get_OverchargeAbility_AirDash_Cost()
    {
        return airDashCost;
    }
    public void OverchargeAbility_SuperSlash()
    {
        GameObject swordSlash = Instantiate(superSlashObject, transform.position, transform.rotation);
        swordSlash.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * superSlashSpeed, ForceMode.VelocityChange);
        //swordSlash.GetComponent<PlayerProjectile>().SetProjectileDamage(superSlashDamage + );
        Destroy(swordSlash, 5f);
    }
    public int Get_OverchargeAbility_SuperSlash_Cost()
    {
        return superSlashCost;
    }
    float CalculateDamageMultiplier(float currentOvercharge, int abilityCost, float damageMultiplier)
    {
        if (currentOvercharge > abilityCost)
        {
            return Mathf.Floor(damageMultiplier);
        }
        else if (currentOvercharge > abilityCost * 2)
        {
            return (float)(damageMultiplier * 2.5);
        }
        else if (currentOvercharge > abilityCost * 3)
        {
            return (float)(damageMultiplier * 4);
        }
        else
        {
            return 0;
        }
    }
}
