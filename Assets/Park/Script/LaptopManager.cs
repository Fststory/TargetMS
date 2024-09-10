using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopManager : MonoBehaviour
{
    public float plusAnnualIncome;
    public float instanceIncome;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpecUp()
    {
        plusAnnualIncome += 500f; //annualIncome에 더해져서 영원히 올라야 하는값
        Debug.Log(plusAnnualIncome);
    }

    public void TwoJob()
    {
        instanceIncome = 1000f; //단한번 saving에 더해지는값
        Debug.Log(instanceIncome);
    }
}
