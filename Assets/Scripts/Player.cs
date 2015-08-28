using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 10;  //public variable to modify the speed of the character

	Rigidbody2D rb;
	Vector2 velocity;

	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update(){
		velocity = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized; 
		//get input from the player, normalizing so they dont go faster when pressing two keys down at once
		velocity *= speed; //controls how fast the character is moving
	}

	void FixedUpdate(){
		rb.MovePosition (rb.position + velocity * Time.fixedDeltaTime);  
		//updating the characters position based on input collected during update and updating regardless of frame rate
	}
}
