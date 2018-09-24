/* 
 * perhaps use object pooling, or some other more efficient system instead
 */ 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	private void OnCollisionEnter(Collision collision)
	{
        Destroy(gameObject);
	}

	//private void OnCollisionEnter(Collision collision)
	//{
	//       Debug.Log("collided with a ball");
	//       Debug.Log("tag is" + collision.gameObject.tag.ToString());

	//       if (collision.gameObject.tag == "Ball")
	//       {
	//           Destroy(gameObject);
	//       }
	//}

}
