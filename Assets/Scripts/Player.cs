using UnityEngine;
using static BattleManager;
using static Inventory;
using UnityEngine.SceneManagement;

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
        Inv1 = 6,
        Inv2 = 7,
        Inv3 = 8,
        Start = 9,
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

    public static Player instance;

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

        if (instance != null) //if another instance of the player is in the scene
        {
            Destroy(gameObject); //then destroy it
        }

        instance = this; //reassign the instance to the current player
        GameObject.DontDestroyOnLoad(this.gameObject);


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

            if(Input.GetKeyDown(KeyCode.KeypadEnter))
            {

                State = Playerstates.Inv1;
                UpdateInventorySelection1();
            }

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

    void UpdateInventorySelection1()
    {
        invenpage1.SetActive(true);

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentInvenItem < 2)
                ++CurrentInvenItem;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentInvenItem > 0)
                --CurrentInvenItem;

        }

        Inventory.UpdateInventorySelection(CurrentInvenItem);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("HomeStairsTop"))
        {

            SceneManager.LoadScene(4);


        }

        if (collision.gameObject.tag.Equals("HomeStairsBottom"))
        {

            SceneManager.LoadScene(3);


        }

        if (collision.gameObject.tag.Equals("DoorOutside"))
        {

            SceneManager.LoadScene(6);


        }

        if (collision.gameObject.tag.Equals("DoorInside"))
        {

            SceneManager.LoadScene(4);


        }

        if (collision.gameObject.tag.Equals("OutsidetoGrounds"))
        {

            SceneManager.LoadScene(5);


        }

        if (collision.gameObject.tag.Equals("OutsidetoHome"))
        {

            SceneManager.LoadScene(6);


        }

        if (collision.gameObject.tag.Equals("OutsidetoTown"))
        {

            SceneManager.LoadScene(2);


        }

        if (collision.gameObject.tag.Equals("TowntoOutside"))
        {

            SceneManager.LoadScene(5);


        }


    }

}
