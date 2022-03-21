using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public GameObject bullet;

    private Rigidbody2D myBody;
    private Animator character;

    public float speed;
    [SerializeField]
    private int health;

    private Vector2 moveVelocity;

    private bool hit = true;

    void Awake () {
        myBody = GetComponent<Rigidbody2D>();
        character = transform.GetChild(0).GetComponent<Animator>();

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
    
        if(moveVelocity == Vector2.zero)
            character.SetBool("Moving", false);
        else
            character.SetBool("Moving", true);
         

    }

    IEnumerator HitBoxOff()
    {
        hit = false;
        yield return new WaitForSeconds(1.5f);
        hit = true;
    }

   void OnTriggerEnter2D(Collider2D target) 
    {
        if(target.tag == "Enemy")
        {
            if(hit) {
                StartCoroutine(HitBoxOff());
                health--;
            }
        }    
    }
}
