using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Android;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class LocationManager : MonoBehaviour
{
    public TMP_Text textDisplay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLocationTracking());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartLocationTracking()
    {
        //print(LocationService.status);

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
            print("no");
        }
        else
        {
            Debug.Log ("Latitude : " + Input.location.lastData.latitude);
            Debug.Log ("Longitude : " + Input.location.lastData.longitude);
            
            textDisplay.text = "Latitude : " + Input.location.lastData.latitude +
                "\n" + "Longitude : " + Input.location.lastData.longitude;
        }
        
        //print(LocationService.status);
        
        Input.location.Start();
        
        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        ////

        // check if user allowed location tracking
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
        }

        // check if status is initializing?
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            yield return new WaitForSeconds(1);
        }

        // if location check fails
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        
        Debug.Log ("Latitude : " + Input.location.lastData.latitude);
        Debug.Log ("Longitude : " + Input.location.lastData.longitude);
        Debug.Log("Altitude : " + Input.location.lastData.altitude);

    }
}
