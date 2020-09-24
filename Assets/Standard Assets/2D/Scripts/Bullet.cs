using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{

    public class Bullet : MonoBehaviour
    {

        public GameObject source;
        Rigidbody2D rb;
        // Start is called before the first frame update
        void Start()
        {
            float ForceAmount = 400f;
            rb = GetComponent<Rigidbody2D>();
            
            //rb.AddForce((source.transform.position - transform.position) * ForceAmount, ForceMode2D.Force);
            rb.AddForce((transform.right) * ForceAmount, ForceMode2D.Force);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}
