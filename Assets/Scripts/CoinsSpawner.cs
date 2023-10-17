using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    public GameObject theCoin;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(0f, 1f, 0f, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            Vector3 randomPosition = new Vector3(Random.Range(-5, 5), 3, Random.Range(-5, 5));
            Instantiate(theCoin, randomPosition, Quaternion.identity);
        }
    }
}
