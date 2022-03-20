using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public GameObject bullet;

    private Rigidbody2D myBody;

    public float speed;

    private Vector2 moveVelocity;

    void Awake () {
        myBody = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
            Rotation();
            //Shoot Function
            if(Input.GetMouseButtonDown(0))
            Instantiate(bullet, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

   void Rotation() {
       Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10 * Time.deltaTime);
    }

    void Movement()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        myBody.MovePosition(myBody.position + moveVelocity * Time.fixedDeltaTime);
    }
}
