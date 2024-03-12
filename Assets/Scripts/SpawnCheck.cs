using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCheck : MonoBehaviour
{
    public bool usedSpawn = false;


    private void OnTriggerStay(Collider enemy)
    {
        usedSpawn = true;
    }
}
