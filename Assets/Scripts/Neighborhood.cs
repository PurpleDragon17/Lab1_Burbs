using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neighborhood : MonoBehaviour
{
    //While thsi was copped for the Flock script, nothing here is edited. This scrpit was working well
    //This script has the anti collsion prepamisterss that I used for the Boids in reation to the obstaicals 
    [Header("Set Dynamically")]
    public List<Boid>       neighbors;
    private SphereCollider  coll;
  

    void Start()
    {
        neighbors = new List<Boid>();
        coll = GetComponent<SphereCollider>();
        coll.radius = Spawner.S.neighborDist / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (coll.radius != Spawner.S.neighborDist/2)
        {
            coll.radius = Spawner.S.neighborDist/2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Boid b = other.GetComponent<Boid>();
        if (b != null)
        {
            if (neighbors.IndexOf(b) == -1)
            { neighbors.Add(b);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        Boid b = other.GetComponent<Boid>();
        if (b != null)
        {
            if (neighbors.IndexOf(b) != -1)
            {
                neighbors.Remove(b);
            }
        }
    }


    public Vector3 avgPos
    {
        get
        {
            Vector3 avg = Vector3.zero;
            if (neighbors.Count == 0) return avg;

            for (int i=0; i<neighbors.Count; i++)
            {
                avg += neighbors[i].pos;
            }
            avg /= neighbors.Count;

            return avg;

        }
    }

    public Vector3 avgVel
    {
        get
        {
            Vector3 avg = Vector3.zero;
            if (neighbors.Count == 0) return avg;

            for (int i = 0; i < neighbors.Count; i++)
            {
                avg += neighbors[i].rigid.velocity;
            }
            avg /= neighbors.Count;

            return avg;
        }
    }

    public Vector3 avgClosePos
        //Scrip that's casuing avoidance 
    {
        get
        {
            Vector3 avg = Vector3.zero;
            Vector3 delta;
            // 
            int nearCount = 0;
            for (int i = 0; i < neighbors.Count; i++)
            {
                delta = neighbors[i].pos - transform.position;
                //difference between teh distance of a neighboor and itself 
                if (delta.magnitude <= Spawner.S.collDist)
                    //collDist = collsion distnabce, set in the spawner
                {
                    avg += neighbors[i].pos;
                    nearCount++;
                }
            }

            // If there were no neighbors too close, return Vector3.zero
            if (nearCount == 0) return avg;

            // Otherwise, averge their locations
            avg /= nearCount;
            return avg;
        }
    }
}
