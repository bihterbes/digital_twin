using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{

    //merhva
    [SerializeField] GameObject dirLight;
    [SerializeField] GameObject pointLight;
    [SerializeField] float timeScale = 30f;
    [SerializeField] float lightRotationScale = 30f;

    Vector3 startRot;
    float ingameSec;
    float ingameMin;
    float ingameHour;
    private void Start()
    {
        ingameSec = 0f;
        startRot = dirLight.transform.eulerAngles;
    }
    private void Update()
    {
        ingameSec += Time.deltaTime * timeScale;
        dirLight.transform.rotation *= Quaternion.Euler(Time.deltaTime * lightRotationScale, 0f, 0f);
        if (ingameSec > 59f)
        { // one min
            ingameMin += 1f;
            ingameSec = 0f;
        }
        if(ingameMin > 59f)
        {
            ingameHour += 1f;
            ingameMin = 0f;
            ingameSec = 0f;
        }
        if (ingameHour > 23f)
        {
            ingameHour = 0f;
            ingameMin = 0f;
            ingameSec = 0f;
            dirLight.transform.rotation = Quaternion.Euler(startRot);
        }

         Debug.Log(dirLight.transform.rotation.eulerAngles.x);

        if(dirLight.transform.rotation.eulerAngles.x <360f && dirLight.transform.rotation.eulerAngles.x > 269f )
        {
            pointLight.gameObject.SetActive(true);
            ConnectionArdunio.instance.SetLight(true);
        }
        else
        {
            Debug.Log("HERERERERERE");
            pointLight.gameObject.SetActive(false);
            ConnectionArdunio.instance.SetLight(false);
        }
    }
}
