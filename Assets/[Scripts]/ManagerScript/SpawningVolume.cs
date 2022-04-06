using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawningVolume : MonoBehaviour
{
    private BoxCollider boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //why do I have to put this in Awake???
        boxCollider = GetComponent<BoxCollider>();
    }
    

    public Vector3 GetPositionInBounds()
    {
        Bounds boxBounds = boxCollider.bounds;
        return new Vector3(Random.Range(boxBounds.min.x, boxBounds.max.x), transform.position.y,
            Random.Range(boxBounds.min.z, boxBounds.max.z));
    }
}
