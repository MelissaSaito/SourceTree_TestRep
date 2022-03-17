using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
	private Rigidbody rigidBody;
	private Vector3 velocity;
	private Vector3 input;

	[SerializeField] private LayerMask groundLayers;
	[SerializeField] private float walkSpeed = 4f;
	[SerializeField] private bool isGrounded;
	[SerializeField] private Vector3 groundPositionOffset = new Vector3(0f, 0.02f, 0f);
	[SerializeField] private float groundColliderRadius = 0.29f;

	void Start()
	{
		rigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		CheckGround();

		input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		var clampedInput = Vector3.ClampMagnitude(input, 1f);
		velocity = clampedInput * walkSpeed;
		transform.LookAt(rigidBody.position + input);

		if (isGrounded)
		{
			Debug.Log("Grounded");
		}

		rigidBody.AddForce(rigidBody.mass * velocity / Time.fixedDeltaTime, ForceMode.Force);
	}

	private void CheckGround()
	{
		if (Physics.CheckSphere(rigidBody.position + groundPositionOffset, groundColliderRadius, groundLayers))
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
		
	}
}
