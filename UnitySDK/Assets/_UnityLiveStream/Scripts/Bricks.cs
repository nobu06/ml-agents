using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bricks : MonoBehaviour {

    public GameObject brickParticle;

	private void OnCollisionEnter(Collision collision)
	{
        Instantiate(brickParticle, transform.position, Quaternion.identity);
        //GameManager_Tutorial.instance.DesroyBrick();
        Destroy(gameObject);
	}
}
