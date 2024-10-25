using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{

    public Rigidbody2D theRB;
    public SpriteRenderer sr;
    public float initialMoveSpeed;
    public float moveSpeed;

    public Animator myAnim;

    public static PlayerController instance;

    public string areaTransitionName;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;
    public bool canMove = true;


    [Header("Collectibles Holder")]
    public List<Collectible> collectedCollectibles = new List<Collectible>();
    public List<Item> ownedItems = new List<Item>();

	// Use this for initialization
	void Start () 
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () 
    {
        if(moveSpeed != initialMoveSpeed + GameManager.instance.speedBuffValue)
        {
            moveSpeed = initialMoveSpeed + GameManager.instance.speedBuffValue;
        }
        if(GameManager.instance.playerStats[0].maximumCollectTrashAmt != 2 + GameManager.instance.carryCapacityBuffValue)
        {
             GameManager.instance.playerStats[0].maximumCollectTrashAmt =  2 + GameManager.instance.speedBuffValue;
        }

        if (canMove)
        {
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * moveSpeed;
        } else
        {
            theRB.velocity = Vector2.zero;
        }


        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }

        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
    }
}
