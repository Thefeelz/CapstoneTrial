using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUp : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Image overChargeBar;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar.fillAmount < 1)
            healthBar.fillAmount += (Time.deltaTime / 4);
        else if (overChargeBar.fillAmount < 1)
            overChargeBar.fillAmount += (Time.deltaTime / 4);
    }
}
