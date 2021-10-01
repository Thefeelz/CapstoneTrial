using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThang : MonoBehaviour
{
    [SerializeField] int enemyMaxHealth = 20;
    [SerializeField] int enemyCurrentHealth;
    [SerializeField] float movementSpeed = 1f;

    [SerializeField] float rateOfFire;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] int enemyAttackPower;

    [SerializeField] float amountToRestore;
    [SerializeField] int progressTowardsKillCount = 1;

    [SerializeField] ParticleSystem deathEffect;

    CharacterStats player;
    MasterLevel masterLevel;


    void Start()
    {
        enemyCurrentHealth = enemyMaxHealth;
        player = FindObjectOfType<CharacterStats>();
        masterLevel = FindObjectOfType<MasterLevel>();
        StartCoroutine(FireWeapon());
    }

    void Update()
    {
        MoveToPlayer();
    }
    public void TakeDamage(int damage)
    {
        enemyCurrentHealth -= damage;
        if(enemyCurrentHealth <= 0)
        {
            player.ReplenishHealth(amountToRestore);
            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponentInChildren<Collider>().enabled = false;
            masterLevel.AddToKillCount(progressTowardsKillCount);
            StartCoroutine(StartDeathScene());
            //Destroy(this.gameObject);
        }
    }
    void MoveToPlayer()
    {
        if(Vector3.Distance(transform.position, player.transform.position) > 1)
            transform.position = (Vector3.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime));
    }
    IEnumerator StartDeathScene()
    {
        Instantiate(deathEffect, transform);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    IEnumerator FireWeapon()
    {
        while (enemyCurrentHealth > 0)
        {
            yield return new WaitForSeconds(Random.Range(rateOfFire, (float)(rateOfFire * 1.5)));
            if (enemyCurrentHealth > 0)
            {
                GameObject newItem = Instantiate(projectile, firePoint.position, Quaternion.FromToRotation(transform.position, player.transform.position));
                newItem.GetComponent<ProjectileOne>().SetDamage(enemyAttackPower);
            }
        }
    }
}
