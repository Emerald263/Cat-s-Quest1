using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static BattleManager;
using static Inventory;

public class Player : MonoBehaviour
{

    //audio variables
    public AudioSource soundEffects;
    public AudioClip[] sounds; // Public variable to access the Audio Source component

    public int Milk;

    public float speed;
    public float IdleTimer;
    public float timeincrease;
    public GameObject inventory; //inventory UI
    public GameObject invenpage1;
    public GameObject invenpage2;
    public GameObject Startscreen;
    public int item1;
    public int item2;
    public int item3;
    public int gold;

    public TextMeshProUGUI milktext;
    public TextMeshProUGUI keyitem1;
    public TextMeshProUGUI keyitem2;
    public TextMeshProUGUI keyitem3;

    public TextMeshProUGUI EXPtext;
    public TextMeshProUGUI HPtext;
    public TextMeshProUGUI DEFtext;
    public TextMeshProUGUI GOLDtext;

    public float dead;

    public GameObject Textdialoguebox; //textbox UI
    TextBox targetBox;

    public GameObject player; 

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
        Text = 10,
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
        Textdialoguebox.SetActive(false);
        Startscreen.SetActive(true);
        targetBox = Textdialoguebox.GetComponent<TextBox>();

        dead = 0;

        gold = 500;

        speed = 0.2f;

        State = Playerstates.Start;

        IdleTimer = 0;
        timeincrease = 1;

        drink = false;

        if (instance != null) //if another instance of the player is in the scene
        {
            Destroy(gameObject); //then destroy it
        }

        instance = this; //reassign the instance to the current player
        GameObject.DontDestroyOnLoad(this.gameObject);


        Milk = 5;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("x") && (State == Playerstates.Start))
        {


            State = Playerstates.Overworld;
            Startscreen.SetActive(false);


        }

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

        if (Input.GetKey("q"))
        {
            State = Playerstates.Text;
            Textdialoguebox.SetActive(true);
            StartCoroutine(HandleTextBox());


        }




        if (Input.GetKey("e"))
        {
            State = Playerstates.Inventory;
            soundEffects.PlayOneShot(sounds[2], .7f);


        }

        if (State == Playerstates.Inventory)
        {
            Debug.Log("Inventory");
            inventory.SetActive(true);
            invenpage1.SetActive(true);
            invenpage2.SetActive(false);


            HandleCurrentInventoryPage();


        }

        if (State == Playerstates.Shop)
        {
            if (Input.GetKey("x"))
            {

                State = Playerstates.Overworld;

                Textdialoguebox.SetActive(false);

            }

            if (Input.GetKey("t"))
            {


                StartCoroutine(SellAll());


            }

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

        if (dead == 1)
        {

            StartCoroutine(Surrender());

        }



        myAnim.SetBool("Idle", idle);


        myAnim.SetFloat("Up", dir.y);
        myAnim.SetFloat("Strafe", dir.x);


        transform.position = newPosition;

        IdleTimer = Time.deltaTime + timeincrease;

        milktext.text = "Milk......" + Milk.ToString();
        keyitem1.text = "Yarn....." + item1.ToString();
        keyitem2.text = "Treats....." + item2.ToString();
        keyitem3.text = "Toys." + item3.ToString();


        EXPtext.text = "EXP" + 0.ToString();
        HPtext.text = "HP" + 50.ToString();
        DEFtext.text = "DEF" + 10.ToString();
        GOLDtext.text = "GOLD" + gold.ToString();
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


    #region TextBox
    IEnumerator HandleTextBox()
    {
        Debug.Log("Handle TextBox");
        yield return StartCoroutine(targetBox.Typecharacterdialogue($"testing"));

        yield return new WaitForSeconds(1f);




    }

#endregion

    #region Inventory
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
        

        }

        if (Currentinvenpage == 1)
        {

            Debug.Log("Cat");
            invenpage1.SetActive(false);
            invenpage2.SetActive(true);
      
        }





    }

    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("HomeStairsTop"))
        {
            inventory.SetActive(false);
            Textdialoguebox.SetActive(false);
            SceneManager.LoadScene(4);


        }

        if (collision.gameObject.tag.Equals("HomeStairsBottom"))
        {
            inventory.SetActive(false);
            Textdialoguebox.SetActive(false);
            SceneManager.LoadScene(3);


        }

        if (collision.gameObject.tag.Equals("DoorOutside"))
        {
            soundEffects.PlayOneShot(sounds[0], .7f);
            inventory.SetActive(false);
            Textdialoguebox.SetActive(false);
            SceneManager.LoadScene(6);


        }

        if (collision.gameObject.tag.Equals("DoorInside"))
        {
            soundEffects.PlayOneShot(sounds[0], .7f);
            inventory.SetActive(false);
            Textdialoguebox.SetActive(false);
            SceneManager.LoadScene(4);


        }

        if (collision.gameObject.tag.Equals("OutsidetoGrounds"))
        {
            inventory.SetActive(false);
            Textdialoguebox.SetActive(false);
            SceneManager.LoadScene(5);


        }

        if (collision.gameObject.tag.Equals("OutsidetoHome"))
        {
            inventory.SetActive(false);
            Textdialoguebox.SetActive(false);
            SceneManager.LoadScene(6);


        }

        if (collision.gameObject.tag.Equals("OutsidetoTown"))
        {
            inventory.SetActive(false);
            Textdialoguebox.SetActive(false);
            SceneManager.LoadScene(2);


        }

        if (collision.gameObject.tag.Equals("TowntoOutside"))
        {

            inventory.SetActive(false);
            Textdialoguebox.SetActive(false);

            SceneManager.LoadScene(5);


        }

        if (collision.gameObject.tag.Equals("ShopDoor"))
        {
            State = Playerstates.Overworld;

            SceneManager.LoadScene(8);


        }

        if (collision.gameObject.tag.Equals("Enemy"))
        {


            StartCoroutine(SetupBattle());


        }

        if (collision.gameObject.tag.Equals("ShopNPC"))
        {


            StartCoroutine(SetupShop());


        }

        if (collision.gameObject.tag.Equals("Milk"))
        {


            StartCoroutine(MilkGet());


        }

        if (collision.gameObject.tag.Equals("Item1"))
        {


            StartCoroutine(Item1Get());


        }

        if (collision.gameObject.tag.Equals("Item2"))
        {


            StartCoroutine(Item2Get());


        }

        if (collision.gameObject.tag.Equals("Item3"))
        {


            StartCoroutine(Item3Get());


        }


    }

    #endregion

    public IEnumerator SetupBattle()
    {
        State = Playerstates.Battle;
        Textdialoguebox.SetActive(true);

        yield return StartCoroutine(targetBox.Typecharacterdialogue($"So, you don't wanna mind your buisness? Well then, I'll show you a lesson!"));

        yield return new WaitForSeconds(1f);
        {



        }
        Textdialoguebox.SetActive(false);
        Debug.Log("BattleStart");

        SceneManager.LoadScene(1);

        player.SetActive(false);

        dead = 1; 
    }

    public IEnumerator SetupShop()
    {
        State = Playerstates.Shop;
        Textdialoguebox.SetActive(true);

        yield return StartCoroutine(targetBox.Typecharacterdialogue($"Wanna sell your items? You can here!"));

        yield return StartCoroutine(targetBox.Typecharacterdialogue($"Sell your items? [T]"));

        yield return new WaitForSeconds(1f);
        {



        }



    }


    public IEnumerator SellAll()
    {

        yield return StartCoroutine(targetBox.Typecharacterdialogue($"Everything Sold! Thank you for your buisness!"));
        yield return new WaitForSeconds(1f);
        {

            while (item1 > 0)
            {

                item1--;
                gold += 25;

            }

            while (item2 > 0)
            {

                item2--;
                gold += 25;

            }

            while (item3 > 0)
            {

                item3--;
                gold += 25;

            }

        }

       

        Textdialoguebox.SetActive(false);
        State = Playerstates.Overworld;

    }

    public IEnumerator Surrender()
    {
        State = Playerstates.Overworld;

        player.SetActive(true);

        Textdialoguebox.SetActive(true);

        yield return StartCoroutine(targetBox.Typecharacterdialogue($"Okay, Okay! You Win! Just please spare me!"));

        yield return new WaitForSeconds(1f);
        {



        }
        Textdialoguebox.SetActive(false);

    }

    public IEnumerator MilkGet()
    {

        Textdialoguebox.SetActive(true);

        soundEffects.PlayOneShot(sounds[1], .7f);

        Milk++;
        yield return StartCoroutine(targetBox.Typecharacterdialogue($"Milk Obtained!"));
        yield return new WaitForSeconds(1f);
        {



        }

        Textdialoguebox.SetActive(false);
    }

    public IEnumerator Item1Get()
    {

        Textdialoguebox.SetActive(true);

        soundEffects.PlayOneShot(sounds[1], .7f);

        item1++;
        yield return StartCoroutine(targetBox.Typecharacterdialogue($"Item Obtained!"));
        yield return new WaitForSeconds(1f);
        {



        }
        Textdialoguebox.SetActive(false);

    }

    public IEnumerator Item2Get()
    {

        Textdialoguebox.SetActive(true);

        soundEffects.PlayOneShot(sounds[1], .7f);

        item2++;
        yield return StartCoroutine(targetBox.Typecharacterdialogue($"Item Obtained!"));
        yield return new WaitForSeconds(1f);
        {



        }
        Textdialoguebox.SetActive(false);

    }

    public IEnumerator Item3Get()
    {

        Textdialoguebox.SetActive(true);

        soundEffects.PlayOneShot(sounds[1], .7f);

        item3++;
        yield return StartCoroutine(targetBox.Typecharacterdialogue($"Item Obtained!"));
        yield return new WaitForSeconds(1f);
        {



        }
        Textdialoguebox.SetActive(false);

    }

}
