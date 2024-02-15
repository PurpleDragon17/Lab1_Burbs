using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //This is a Singleton of the BoidSpawner. there is only one instance 
    // of BoidSpawner, so we can store it in a static variable named s.
    static public Spawner       S;
    static public List<Boid>    boids;
    static public List<Burb>    burbs;

    // These fields allow you to adjust the spawning behavior of the boids
    [Header("Set in Inspector: Spawning")]
    public GameObject           boidPrefab;
    public GameObject           burbPrefab;
    // burbs are the second group of boids 
    public Transform            boidAnchor;
    public int                  numBoids = 100;
    public int                  numBurbs = 100;
    // number of burbs and number of boids are indevepndly determined
    public float                spawnRadius = 100f;
    public float                spawnDelay = 0.1f;

    // These fields allow you to adjust the flocking behavior of the boids
    [Header("Set in Inspector: Boids")]
    public float                velocity = 30f;
    public float                neighborDist = 30f;
    public float                collDist = 4f;
    public float                velMatching = 0.25f;
    public float                flockCentering = 0.2f;
    public float                collAvoid = 2f;
    public float                attractPull = 2f;
    public float                attractPush = 2f;
    public float                attractPushDist = 5f;

    //These are the obsticals 
    public GameObject Ob1;
    public GameObject Ob2;
    
    void Awake()
    {
        //Set the Singleton S to be this instance of BoidSpawner
        S = this;
        //Start instantiation of the Boids
        boids = new List<Boid>();
        burbs = new List<Burb>();
        InstantiateBoid();
        InstantiateBurbs();
    }

    public void InstantiateBoid()
    {
        GameObject go = Instantiate(boidPrefab);
        Boid b = go.GetComponent<Boid>();
        b.transform.SetParent(boidAnchor);
        boids.Add(b);
        if (boids.Count < numBoids)
        {
            Invoke("InstantiateBoid", spawnDelay);
        }
 
    }

    public void InstantiateBurbs()
    {
        GameObject go = Instantiate(burbPrefab);
        Burb b = go.GetComponent<Burb>();
        //Telling this script to get the Burbs scrpit
        b.transform.SetParent(boidAnchor);
        burbs.Add(b);
        if (burbs.Count < numBurbs)
        {
            Invoke("InstantiateBurbs", spawnDelay);
        }
        //This is the same thing as the InstantiateBoid, but for Burbs
    }
}