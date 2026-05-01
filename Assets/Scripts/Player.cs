using UnityEngine;
using static BattleManager;
using static Inventory;

public class Player : MonoBehaviour
{

    public float speed;
    public float IdleTimer;
    public float timeincrease;
    public GameObject inventory; //inventory UI
    public GameObject invenpage1;
    public GameObject invenpage2;
    public GameObject invenpage3;


    public Playerstates State;
    public enum Playerstates
    {

        Overworld = 1,
        Battle = 2,
        Inventory = 3,
        Shop = 4,
        Rest = 5,
    }

        int Currentinvenpage;


    

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
        inventory.SetActive(false);

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
            invenpage1.SetActive(true);
            invenpage2.SetActive(false);
            invenpage3.SetActive(false);

            HandleCurrentInventoryPage();


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

    void HandleCurrentInventoryPage()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Currentinvenpage < 2)
                ++Currentinvenpage;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Currentinvenpage > 0)
                --Currentinvenpage;

        }


        if (Currentinvenpage == 0)
        {
            Debug.Log("Cat");
            invenpage1.SetActive(true);
            invenpage2.SetActive(false);
            invenpage3.SetActive(false);


        }

        if (Currentinvenpage == 1)
        {

            Debug.Log("Cat");
            invenpage1.SetActive(false);
            invenpage2.SetActive(true);
            invenpage3.SetActive(false);
        }

        if (Currentinvenpage == 2)
        {

            Debug.Log("Items");
            invenpage1.SetActive(false);
            invenpage2.SetActive(false);
            invenpage3.SetActive(true);

        }



    }

}
