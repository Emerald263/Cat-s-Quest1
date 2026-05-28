using UnityEngine;

public class Enemychange : MonoBehaviour
{

    public GameObject enemyattack;
    public GameObject enemysurrender;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        enemyattack.SetActive(true);
        enemysurrender.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("Player"))
        {
            enemyattack.SetActive(false);
            enemysurrender.SetActive(true);


        }



    }
}
