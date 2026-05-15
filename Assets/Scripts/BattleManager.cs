using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Player;
using System.Collections.Generic;
using System.Collections;
using TMPro;


public class BattleManager : MonoBehaviour
{

    //audio variables
    public AudioSource soundEffects;
    public AudioClip[] sounds; // Public variable to access the Audio Source component

    //Animation variables
    Animator anim;
    public bool Catidle;
    public bool Catattck;
    public bool Catdrink;
    public bool Companionattck;
    public bool Enemyidle;
    public bool EAttack;

    public TextMeshPro existsIn3DSpaceText; //the TextMeshPro object exists in scene space, NOT canvas or screenspace
    public TextMeshProUGUI existsInScreenSpace; //any canvas based textMeshPro objects you add will be this data type


    [SerializeField] BattleDialogueBox dialogueBox;
    public TextMeshProUGUI milktext;
    public TextMeshProUGUI attacktext;
    public TextMeshProUGUI itemtext;

    public TextMeshProUGUI Playerhealth;
    public TextMeshProUGUI Enemyhealth;


    public enum Battlestates
    {
        Start,
        PlayerAction,
        PlayerActionCat,
        PlayerActionCompanion,
        PlayerActionItem,
        EnemyMove,
        Busy,

    }

    [Header("Scripts Ref:")]
    public Playerstates State;
    public float armor;
    public float SP;
    public float attack;
    public float spell;
    public int HP;
    public float EXP;
    public float EXPfinal;
    public float GP;
    public float GPfinal;
    public float Level;
    public float armoradd;
    public float SPadd;
    public float attckadd;
    public float splladd;

    public float eneSP;
    public float eneattck;
    public float eneDef;
    public float eneHP;
    public float eneEXP;
    public float eneHPFinal;

    public float Battleorder;
    public float Lvlattack;

    public int Milk;
    public int Treat;

    static int milkheal;




    Battlestates state;
    int CurrentActionBattle;
    int CurrentMoveCat;
    int CurrentMoveCompanion;
    int CurrentMoveItem;


    // Start is called before the first frame update
    private void Start()
    {


        soundEffects = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableMoveSelectorCat(false);
        dialogueBox.EnableMoveSelectorCat(false);

   


          itemtext.enabled = false;
        milktext.enabled = false;
        attacktext.enabled = false;
        SetupBattle();



        armor = 10;
        SP = 10;
        attack = 10;
        spell = 1;
        HP = 10;
        EXP = 0;
        GP = 0;
        Level = 1;

        eneSP = 5;
        eneattck = 10;
        eneDef = 5;
        eneHP = 4;
        eneEXP = 10;


        armor = Level * 10;
        attack = (Level * Lvlattack) + 10;
        spell = (Level * 15) + 15;

        Milk = 5;
        Treat = 5;

        milkheal = 3;




        StartCoroutine(SetupBattle());

    }


    // Update is called once per frame
    void Update()
    {

        if (state == Battlestates.PlayerAction)
        {

            HandleActionSelection();

        }
        else if (state == Battlestates.PlayerActionCat)
        {

            CatAction();

        }


        if (Input.GetKeyDown(KeyCode.L))
        {

            eneHP = -1;
        }

        anim.SetBool("attack", Catattck);
        anim.SetBool("battleidle", Catidle);
        anim.SetBool("drink", Catdrink);

        anim.SetBool("EnemyAttack", EAttack);

        milktext.text = "Milk......" + Milk.ToString();
        attacktext.text = "attacks the enemy".ToString();

        Playerhealth.text = "HP:" + HP.ToString();
        Enemyhealth.text = "HP:" + eneHP.ToString();


    }
    void HandleActionSelection()
    {
        Catidle = true;
        Catattck = false;
        Catdrink = false;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentActionBattle < 1)
                ++CurrentActionBattle;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentActionBattle > 0)
                --CurrentActionBattle;

        }

        dialogueBox.UpdateActionSelection(CurrentActionBattle);


        if (Input.GetKeyDown(KeyCode.E))
        {

            if (CurrentActionBattle == 0)
            {

                //Fight
                CatAction();
            }

            if (CurrentActionBattle == 1)
            {

                //Run
                StartCoroutine(BattleFlee());
            }



        }

        

    }




    void CatAction()
    {
        EAttack = false;
        Debug.Log("CatAction");
        state = Battlestates.PlayerActionCat;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableMoveSelectorCat(true);
        HandleMoveSelectionCat();


    }

    void HandleMoveSelectionCat()
    {

        Catidle = true;
        Catattck = false;
        Catdrink = false;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentMoveCat < 1)
                ++CurrentMoveCat;
            milktext.enabled = true;
            attacktext.enabled = false;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentMoveCat > 0)
                --CurrentMoveCat;
            milktext.enabled = false;
            attacktext.enabled = true;

        }

        dialogueBox.UpdateMoveSelectionCat(CurrentMoveCat);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (CurrentMoveCat == 0)
            {
                Debug.Log("CatMove");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorCat(false);
                state = Battlestates.Busy;
                StartCoroutine(CatAttack());


            }


            if (CurrentMoveCat == 1)
            {
                Debug.Log("Catdrink");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorCat(false);
                state = Battlestates.Busy;
                StartCoroutine(CatItem());

            }


        }

    }

    void ItemSelection()
    {

        Debug.Log("Items");
        dialogueBox.EnableDialogueText(true);
        dialogueBox.EnableMoveSelectorCat(false);
        milktext.enabled = true;

        if (Input.GetKeyDown(KeyCode.F))
        {

            Milk--;

            HP += milkheal;

            milktext.enabled = false;

            EnemyAction();

        }


        if (Input.GetKeyDown(KeyCode.R))
        {

            HandleMoveSelectionCat();


        }
    }








    public IEnumerator CatAttack()
    {
        Catidle = false;
        Catattck = true;
        Catdrink = false;
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You Punched! ...It's rather weak"));
        yield return new WaitForSeconds(5f);
        {

            --eneHP;

            if (eneHP < 1)
            {

                enemydeath();

            }

            EnemyAction();
        }
    }


    public IEnumerator CatItem()
    {
        Catidle = false;
        Catattck = false;
        Catdrink = true;
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You drank some milk! It's rather refreshing"));
        yield return new WaitForSeconds(5f);
        {
            Catidle = false;
            Catattck = true;

            Milk--;

            HP += milkheal;

            EnemyAction();
        }
    }





    void EnemyAction()
    {
        state = Battlestates.EnemyMove;
        dialogueBox.EnableDialogueText(true);

        StartCoroutine(EnemyAttack());

        Catidle = true;
        Catattck = false;
        Catdrink = false;

    }

    public IEnumerator EnemyAttack()
    {

        EAttack = true;
        for (int i = 0; i < 1; i++)
        {

            yield return StartCoroutine(dialogueBox.TypeDialogue($"The Enemy Attacked"));
            yield return new WaitForSeconds(5f);


            HP--;

        }

        CatAction();

    }

    public void enemydeath()
    {

        StartCoroutine(BattleEndWin());

    }


    public IEnumerator SetupBattle()
    {


        yield return StartCoroutine(dialogueBox.TypeDialogue($"Alley Cats"));
        yield return new WaitForSeconds(1f);

        Playeraction();
    }

    void Playeraction()
    {

        state = Battlestates.PlayerAction;
        StartCoroutine(dialogueBox.TypeDialogue("Choose an action"));
        dialogueBox.EnableActionSelector(true);
    }

    public IEnumerator BattleFlee()
    {
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You fled"));
        {
            //yield return new WaitForSeconds(1);
            SceneManager.LoadScene(8);
            State = Playerstates.Overworld;
            EXPfinal = EXP + 43;
            GPfinal = GP + 15;
        }
    }

    public IEnumerator BattleEndWin()
    {
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You Won! Gained 50exp and 15gp!"));
        yield return new WaitForSeconds(1f);
        {
            //yield return new WaitForSeconds(1);
            SceneManager.LoadScene(8);
            State = Playerstates.Overworld;
            EXPfinal = EXP + 50;
            GPfinal = GP + 15;
        }


    }

    public IEnumerator BattleEndLose()
    {
        yield return StartCoroutine(dialogueBox.TypeDialogue($"You Lost"));
        yield return new WaitForSeconds(1f);
        {
            //yield return new WaitForSeconds(1);
            SceneManager.LoadScene(8);
            State = Playerstates.Overworld;
            EXPfinal = EXP + 50;
            GPfinal = GP + 15;
        }


    }


}
