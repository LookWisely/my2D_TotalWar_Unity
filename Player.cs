using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Assets;

public class Player : MonoBehaviour {

    private Vector2 dir_up = new Vector2(0f, 1f);
    private Vector2 dir_down = new Vector2(0f, -1f);
    private Vector2 dir_left = new Vector2(-1f, 0f);
    private Vector2 dir_right = new Vector2(1f, 0f);

    private Dictionary<Vector2, bool> direction_stack;
    public Rigidbody2D self;
    public Animator AnimController;



    public Camera theCamera;
    [Range(0.1f, 3f)]
    public float PlayerSpeed;
    [Range(0.1f, 3f)]
    public float PlayerAcceleration;


	// Use this for initialization
	void Start () {
        theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
        direction_stack = new Dictionary<Vector2, bool>();
    }
	
	// Update is called once per frame
	void Update () {
        theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
	    mapWASD();
	}


    void mapWASD()
    {
        direction_stack[dir_up] = false;
        direction_stack[dir_down] = false;
        direction_stack[dir_left] = false;
        direction_stack[dir_right] = false;

        if (Input.GetKey("w"))
            direction_stack[dir_up] = true;
        else
            direction_stack[dir_up] = false;
        if (Input.GetKey("s"))
            direction_stack[dir_down] = true;
        else
            direction_stack[dir_down] = false;
        if (Input.GetKey("a"))
            direction_stack[dir_left] = true;
        else
            direction_stack[dir_left] = false;
        if (Input.GetKey("d"))
            direction_stack[dir_right] = true;
        else
            direction_stack[dir_right] = false;


        //-- Setting Animator "Running" Parameter
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
            AnimController.SetBool("PlayerIsMoving", true);
        else
            AnimController.SetBool("PlayerIsMoving", false);

        //------------------------
        //Debug.Log("up    : " + direction_stack[dir_up]);
        //Debug.Log("down  : " + direction_stack[dir_down]);
        //Debug.Log("left  : " + direction_stack[dir_left]);
        //Debug.Log("right : " + direction_stack[dir_right]);
        //------------------------

        MovePlayerBody();

    }

    void MovePlayerBody()
    {
        var _sum_dir = new Vector2(0f, 0f);
        foreach (Vector2 _key in direction_stack.Keys)
            if (direction_stack[_key])
                _sum_dir += _key;

        var GoalVector = _sum_dir.normalized * PlayerSpeed;
        var acceleration_direction = GoalVector - self.velocity;

        acceleration_direction.Normalize();

        if (GAnalytics.EquifyDZ(GoalVector, self.velocity + (acceleration_direction*PlayerAcceleration), PlayerAcceleration) != self.velocity)
        {
            self.AddForce(acceleration_direction*PlayerSpeed);
        }
        else
            self.velocity = GoalVector;


        //-- Setting Animator "Moving Direction" Parameter
        if (GoalVector.x < -0.05)
            AnimController.SetInteger("PlayerMovementDirection", -1);
        else if (GoalVector.x > 0.05)
            AnimController.SetInteger("PlayerMovementDirection", 1);
        else
            AnimController.SetInteger("PlayerMovementDirection", 0);
        //-----------------------------------------------



 //       Debug.Log(GoalVector.x + "\t\t" + GoalVector.y);

    }
}
