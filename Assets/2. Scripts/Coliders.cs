using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coliders : MonoBehaviour
{
    // Start is called before the first frame update

        private void Awake()
        {
            Collider[] collidrs = GetComponentsInChildren<Collider>();
            foreach (Collider col in collidrs)
            {
                col.isTrigger = true;
            }
        }


    // Update is called once per frame
    void Update()
    {
        
    }
}
