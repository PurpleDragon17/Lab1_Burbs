using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour

{
    //This is a coppied neigborhood scprit coppied and ajusted for the Burbs
    [Header("Set Dynamically")]
    public List<Burb> neighbors;
    //Biggest change is the new list
    private SphereCollider coll;

    void Start()
    {
        neighbors = new List<Burb>();
        coll = GetComponent<SphereCollider>();
        coll.radius = Spawner.S.neighborDist / 2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (coll.radius != Spawner.S.neighborDist / 2)
        {
            coll.radius = Spawner.S.neighborDist / 2;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Burb b = other.GetComponent<Burb>();
        if (b != null)
        {
            if (neighbors.IndexOf(b) == -1)
            {
                neighbors.Add(b);
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        Burb b = other.GetComponent<Burb>();
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

            for (int i = 0; i < neighbors.Count; i++)
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
    {
        get
        {
            Vector3 avg = Vector3.zero;
            Vector3 delta;
            int nearCount = 0;
            for (int i = 0; i < neighbors.Count; i++)
            {
                delta = neighbors[i].pos - transform.position;
                if (delta.magnitude <= Spawner.S.collDist)
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

