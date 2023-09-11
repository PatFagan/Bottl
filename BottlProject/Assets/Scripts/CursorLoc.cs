using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLoc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Vector3 rayCastStart = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            bool nearEnemy = Physics.Raycast(rayCastStart, -Vector3.forward, out hit, Mathf.Infinity);
            if (nearEnemy)
            { 
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                print(hit);
            }
            else 
            {
                print("no");
            }
        }
    }
}
