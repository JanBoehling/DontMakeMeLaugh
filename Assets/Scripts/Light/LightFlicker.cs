using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light flickeringLight;
    [SerializeField]private float minIntensity = 20000f;
    [SerializeField]private float maxIntensity = 40000f;
    [SerializeField]private float flickerInterval = 0.5f;
    [SerializeField] private float maxFlickerIntervall;
    [SerializeField] private float doupleTheIntervall;

    private float nextFlickerTime;
    private float waitForNextFlicker;


    void Start()
    {
        nextFlickerTime = Time.deltaTime + Random.Range(0, flickerInterval);
    }

    void Update()
    {
        nextFlickerTime += Time.deltaTime;

        if (nextFlickerTime <= maxFlickerIntervall)
        {
            flickeringLight.intensity = Random.Range(minIntensity, maxIntensity);
        }
        else
        {
            flickeringLight.intensity = 0;
            if (nextFlickerTime >= doupleTheIntervall)
            {
                nextFlickerTime = 0;
            }

        }
       
    }
    
}
