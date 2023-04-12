using UnityEngine;

// Include the namespace required to use Unity UI and Input System
using UnityEngine.InputSystem;
using TMPro;

//For my jump functionality, I copied https://stackoverflow.com/questions/58377170/how-to-jump-in-unity-3d, I tried adding a double jump functionality but then realized it already let me double jump.

public class PlayerController : MonoBehaviour //Taken from unity tutorial.
{

	// Create public variables for player speed, and for the Text UI game objects
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;

	//public bool candoublejump = true;
	public bool isGrounded = true;
	public Vector3 jump;
	public float jumpForce = 2.0f;



	// At the start of the game..
	void Start()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;

		SetCountText();

		// Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winTextObject.SetActive(false);
	}

	void OnCollisionStay()
	{
		isGrounded = true;
		//candoublejump = true;
	}

	void Update() //Putting this in FixedUpdate() made it a bit unresponsive.
    {
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //I had intended to add my own work to what I took when changing a single to doublejump, but it already let me double jump so unfortunately I couldn't add my own doublejump.
		{

			rb.AddForce(jump * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		}
		//else if (Input.GetKeyDown(KeyCode.Space) && candoublejump) Including this let me triple jump. Because when I did a grounded jump it still thought I was grounded for a frame?
		//{
		//	rb.AddForce(jump * jumpForce, ForceMode.Impulse);
		//	candoublejump = false;
		//}
	}

	void FixedUpdate()
	{
		// Create a Vector3 variable, and assign X and Z to feature the horizontal and vertical float variables above
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);


	}

	void OnTriggerEnter(Collider other)
	{
		// ..and if the GameObject you intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText();
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 12)
		{
			// Set the text value of your 'winText'
			winTextObject.SetActive(true);
		}
	}
}