using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    public float IdleTimer;
    public float timeincrease;
    public GameObject inventory; //inventory UI


    public Playerstates State;
    public enum Playerstates
    {

        Overworld = 1,
        Battle = 2,
        Inventory = 3, 
        Shop = 4,
        Rest = 5,


    }

    private SpriteRenderer sr;

    public Sprite upSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite frontSprite;


    public Animator myAnim;
    bool idle;
    bool drink;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();

        speed = 0.2f;

        State = Playerstates.Overworld;

        IdleTimer = 0;
        timeincrease = 1;

        drink = false;
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 newPosition = transform.position;

        Vector3 dir = inputDirection();

        idle = false;
        switch (State)
        {
            case (Playerstates.Overworld):
                {
                    newPosition += dir * speed;
                    if (dir.y > 0) { sr.sprite = upSprite; }
                    else if (dir.y < 0) { sr.sprite = frontSprite; }
                    else if (dir.x > 0) { sr.sprite = rightSprite; }
                    else if (dir.x < 0) { sr.sprite = leftSprite; }
                    else { sr.sprite = frontSprite; idle = true; }
                    break;


                }
        
        }


        if (Input.GetKey("e"))
        {
            State = Playerstates.Inventory;



        }

        if (State == Playerstates.Inventory)
        {
            Debug.Log("Inventory");
            inventory.SetActive(true);

        }

        if (Input.GetKey("x") && (State == Playerstates.Inventory))
        {
            inventory.SetActive(false);
            Debug.Log("Hide Inventory");
            State = Playerstates.Overworld;

        }

        if (Input.GetKey("r") && (State == Playerstates.Shop))
        {


            State = Playerstates.Overworld;

        }





        myAnim.SetBool("Idle", idle);


        myAnim.SetFloat("Up", dir.y);
        myAnim.SetFloat("Strafe", dir.x);


        transform.position = newPosition;

        IdleTimer = Time.deltaTime + timeincrease;

    }



    Vector3 inputDirection()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector3.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += Vector3.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += Vector3.down;
        }
        return dir;
    }



}
