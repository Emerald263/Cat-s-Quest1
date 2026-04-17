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
    public bool Companionidle;
    public float Companionattck;
    public float Enemyidle;
    public float Enemyattck;

    public TextMeshPro existsIn3DSpaceText; //the TextMeshPro object exists in scene space, NOT canvas or screenspace
    public TextMeshProUGUI existsInScreenSpace; //any canvas based textMeshPro objects you add will be this data type


    [SerializeField] BattleDialogueBox dialogueBox;


    public enum Battlestates
    {
        Start,
        PlayerAction,
        PlayerActionCat,
        PlayerActionCompanion,
        EnemyMove,
        Busy,

    }

    [Header("Scripts Ref:")]
    public Playerstates State;
    public float armor;
    public float SP;
    public float attack;
    public float spell;
    public float HP;
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




    Battlestates state;
    int CurrentActionBattle;
    int CurrentMoveCat;
    int CurrentMoveCompanion;


    // Start is called before the first frame update
    private void Start()
    {

        soundEffects = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableMoveSelectorCat(false);
        dialogueBox.EnableMoveSelectorCompanion(false);
        SetupBattle();



        armor = 10;
        SP = 10;
        attack = 10;
        spell = 1;
        HP = 50;
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
        HP = (Level * 10) + 40;
        spell = (Level * 15) + 15;




        StartCoroutine(SetupBattle());

    }


    // Update is called once per frame
    private void Update()
    {

        if (state == Battlestates.PlayerAction)
        {

            HandleActionSelection();

        }
        else if (state == Battlestates.PlayerActionCat)
        {

            CatAction();

        }
        else if (state == Battlestates.PlayerActionCompanion)
        {

            CompanionAction();

        }

        if (Input.GetKeyDown(KeyCode.L))
        {

            eneHP = -1;
        }

        anim.SetBool("catattck", Catattck);
        anim.SetBool("catidl", Catidle);

    }
    void HandleActionSelection()
    {

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
        Debug.Log("Cat");
        state = Battlestates.PlayerActionCat;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableMoveSelectorCat(true);
        HandleMoveSelectionCat();
        Catidle = true;

    }

    void HandleMoveSelectionCat()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentMoveCat < 1)
                ++CurrentMoveCat;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentMoveCat > 0)
                --CurrentMoveCat;

        }

        dialogueBox.UpdateMoveSelectionCat(CurrentMoveCat);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (CurrentMoveCat == 0)
            {
                Debug.Log("Cat");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorCat(false);
                state = Battlestates.Busy;
                StartCoroutine(CatAttack());


            }

            if (CurrentMoveCat == 1)
            {

                Debug.Log("Cat");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorCat(false);
                state = Battlestates.Busy;
                StartCoroutine(CatSpecial());

            }



        }

    }

    public IEnumerator CatAttack()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Cat"));
        yield return new WaitForSeconds(5f);
        {
            Catidle = false;
            Catattck = true;
            --eneHP;

            if (eneHP < 1)
            {

                enemydeath();

            }

            CompanionAction();
        }
    }

    public IEnumerator CatSpecial()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Cat"));
        yield return new WaitForSeconds(5f);
        {
            Catidle = false;
            Catattck = true;
            --eneHP;

            if (eneHP < 1)
            {

                enemydeath();

            }

            CompanionAction();
        }
    }

    void HandleMoveSelectionCompanion()
    {

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CurrentMoveCompanion < 1)
                ++CurrentMoveCompanion;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CurrentMoveCompanion > 0)
                --CurrentMoveCompanion;

        }

        dialogueBox.UpdateMoveSelectionCompanion(CurrentMoveCompanion);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (CurrentMoveCompanion == 0)
            {
                Debug.Log("Companion");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorCompanion(false);
                state = Battlestates.Busy;
                StartCoroutine(CompanionAttack());


            }

            if (CurrentMoveCompanion == 1)
            {

                Debug.Log("Companion");
                dialogueBox.EnableDialogueText(true);
                dialogueBox.EnableMoveSelectorCompanion(false);
                state = Battlestates.Busy;
                StartCoroutine(CompanionSpecial());

            }



        }


    }

    public IEnumerator CompanionAttack()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Companion"));
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

    public IEnumerator CompanionSpecial()
    {

        yield return StartCoroutine(dialogueBox.TypeDialogue($"Companion"));
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

    void CompanionAction()
    {
        Catidle = false;
        Catattck = false;
        Debug.Log("Companion");
        state = Battlestates.PlayerActionCompanion;
        dialogueBox.EnableActionSelector(false);
        dialogueBox.EnableDialogueText(false);
        dialogueBox.EnableMoveSelectorCompanion(true);
        HandleMoveSelectionCompanion();
        Companionidle = true;

        if (eneHP < 0)
        {

            enemydeath();

        }
    }


    void EnemyAction()
    {
        state = Battlestates.EnemyMove;
        dialogueBox.EnableDialogueText(true);

        StartCoroutine(EnemyAttack());



    }

    public IEnumerator EnemyAttack()
    {
        for (int i = 0; i < 3; i++)
        {

            yield return StartCoroutine(dialogueBox.TypeDialogue($"The Enemy Attacked"));
            yield return new WaitForSeconds(5f);


        }

        CatAction();

    }

    public void enemydeath()
    {

        StartCoroutine(BattleEndWin());

    }


    public IEnumerator SetupBattle()
    {


        yield return StartCoroutine(dialogueBox.TypeDialogue($"Wild Tree Crawlers appeared"));
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
            SceneManager.LoadScene(0);
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
            SceneManager.LoadScene(0);
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
            SceneManager.LoadScene(0);
            State = Playerstates.Overworld;
            EXPfinal = EXP + 50;
            GPfinal = GP + 15;
        }


    }


}
