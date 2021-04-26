using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            gameObject.transform.position = new Vector3(1.8f, 0, 1.6f);
        }
    }
}
