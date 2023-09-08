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

    IEnumerator StartLocationTracking()
    {
        #if UNITY_EDITOR
        //Wait until Unity connects to the Unity Remote
        while (!UnityEditor.EditorApplication.isRemoteConnected)
        {
            yield return null;
        }
        #endif

        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
        {
            print("location tracking not enabled");
            yield break;
        }

        // Starts the location service.
        Input.location.Start();
        Input.location.Stop();
        yield return new WaitForSeconds(3f);
        Input.location.Start();

        TryAgain();

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }

    void TryAgain()
    {
        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            //yield break;
        }
        if (Input.location.status == LocationServiceStatus.Running)
        {
            print("yuh");
            // if the connection succeeded, display loc
            textDisplay.text = "Latitude : " + Input.location.lastData.latitude +
                "\n" + "Longitude : " + Input.location.lastData.longitude;
        }
        if (Input.location.status == LocationServiceStatus.Initializing)
        {
            print("initing");
        }
        if (Input.location.status == LocationServiceStatus.Stopped)
        {
            print("why stop");
        }
    }
}

// excess
/*
    // Waits until the location service initializes
    int maxWait = 20;
    while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
    {
        yield return new WaitForSeconds(1);
        maxWait--;
        print("tick");
    }

    // If the service didn't initialize in 20 seconds this cancels location service use.
    if (maxWait < 1)
    {
        print("Timed out");
        yield break;
    }
*/