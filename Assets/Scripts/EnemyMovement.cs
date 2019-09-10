using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] List<Waypoint> path;
    //int cnt = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(PrintAllWayPoints());
        //print("back at start");
    }

    IEnumerator PrintAllWayPoints()
    {
        //print("Starting patrol ...");
        foreach (Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            //print("Visiting " + waypoint);
            yield return new WaitForSeconds(1f);
        }
        //print("Ending patrol ...");


    }

    // Update is called once per frame
    void Update()
    {

    }

    /*  my challenge solution
     
    //created global variable int cnt = 1;

    IEnumerator PrintAllWayPoints()
    {
        foreach (Waypoint waypoint in path)
        {          
            if(cnt == 1)
            {
                //print("starting patrol (block: " + (transform.position.x / 10) + ", " + (transform.position.z / 10) + "), cnt is: " + cnt +", path.Count is: "+path.Count);
                print("Starting patrol!");
                cnt++;
                transform.position = waypoint.transform.position;
            }
            else if(cnt > 0 && cnt < path.Count)
            {
                transform.position = waypoint.transform.position;
                //print("visiting block: " + (transform.position.x / 10) + ", " + (transform.position.z / 10)+ ", cnt is: " + cnt);
                print("visiting block: " + (transform.position.x / 10) + ", " + (transform.position.z / 10));
                cnt++;
            }
            else
            {
                transform.position = waypoint.transform.position;
                //print("ending patrol (block: " + (transform.position.x / 10) + ", " + (transform.position.z / 10) + "), cnt is: " + cnt);
                print("Ending patrol!");
            }

            yield return new WaitForSeconds(1f);
        }


    }



     */

}
