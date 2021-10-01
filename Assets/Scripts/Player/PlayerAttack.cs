using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    CharacterStats playerStats;
    Collider weaponArea;
    [SerializeField] Image targetCrosshair;
    [SerializeField] Transform weaponRaycastTransformPosition;
    [SerializeField] Animator playerAnimator;
    RaycastHit hitTarget;
    bool hitCast;
    Color crosshairColor;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponentInParent<CharacterStats>();
        crosshairColor = targetCrosshair.color;
    }

    private void Update()
    {
        GetPlayerInput();
        CheckEnemyInRange();
    }

    private void GetPlayerInput()
    {
        if (Input.GetMouseButtonDown(0) && !playerAnimator.GetBool("swordSwing"))
        {
            StartCoroutine(WeaponSwing());

            if (Physics.BoxCast(weaponRaycastTransformPosition.position, new Vector3(0.5f, 0.5f, 0.5f), weaponRaycastTransformPosition.rotation * Vector3.forward, out hitTarget, Quaternion.identity, 2f))
            {
                if (hitTarget.transform.CompareTag("Enemy"))
                {

                    hitTarget.transform.GetComponentInParent<EnemyThang>().TakeDamage(playerStats.GetPlayerStrength());
                }
            }
        }
    }

    void CheckEnemyInRange()
    {
        hitCast = Physics.BoxCast(weaponRaycastTransformPosition.position, new Vector3(0.5f, 0.5f, 0.5f), weaponRaycastTransformPosition.rotation * Vector3.forward, out hitTarget, Quaternion.identity, 2f);
        if (hitCast)
        {
            if (hitTarget.transform.CompareTag("Enemy"))
            {
                targetCrosshair.color = Color.red;
            }
            else
            {
                targetCrosshair.color = crosshairColor;
            }
        }
        else
        {
            targetCrosshair.color = crosshairColor;
        }
    }
    IEnumerator WeaponSwing()
    {
        playerAnimator.SetBool("swordSwing", true);
        yield return new WaitForSeconds(1f);
        playerAnimator.SetBool("swordSwing", false);
        StopCoroutine(WeaponSwing());
    }
    public void SetSwordSwingComplete()
    {
        playerAnimator.SetBool("swordSwing", false);
    }
    public void SetStartSuperSlash()
    {
        playerAnimator.SetBool("superSlash", true);
    }
    public void SetSuperSlashComplete()
    {
        playerAnimator.SetBool("superSlash", false);
    }
    public bool GetSuperSlashStatus()
    {
        return playerAnimator.GetBool("superSlash");
    }
}
