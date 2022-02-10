using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// namespaces / libraries 

// : means extends a class, in this case Player_S extends MonoBehaviour
public class Player_Script : MonoBehaviour
{

    #region Variables
    //    Variables
    // if variables are public, they can be accessed in editor
    // if variables are private, they can not be accesed in editor
    private float _speed = 7f;   // generic example course 21
    
    // Course 23 - User Input
    //public float horizontal_Input;   
    //public float vertical_Input;   

    // Course 24 - Optimise User Input Movements
    public Vector3 direction;

    // Course 32 - Instantie Prefabs
    [SerializeField]   //for seeing it in the inspector
    private GameObject _laserPrefab;

    // Course 37 - Offset laser spawn above player
    Vector3 _laserPosition = new Vector3(0, 1f, 0);

    // Course 39 - Firerate cooldows
    private float _laserFirerate = 0.5f;   // 0,5 sec
    private float _canFire = -1f;   // determines if I can fire

    // Course 46 - Colliders
    public short player_life = 3;

    // Course 55
    Spawn_Manager_Script spawn_manager;

    #endregion

    // Start is called before the first frame update
    void Start() {
        // set start position for the caracter, using new Vector3(x,y,z)
        transform.position = new Vector3(0,0,0);
        //spawn_manager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager_Script>();   //or using tags
        spawn_manager = GameObject.FindGameObjectWithTag("Spawn_Manager_Tag").GetComponent<Spawn_Manager_Script>();

        if (spawn_manager == null)
        {
            Debug.LogError("Spawn_Manager is null in Player_Script");
        }
    }

    // Update is called once per frame
    void Update() {
        // User Movement --> using method
        Calculate_Movement();
        Shoot_Laser();
    }

    #region Methods
    // Course 27 - Methods 

    public void Calculate_Movement() {

        // Course 21 - Moving the character
        //transform.Translate(new Vector3(1, 0, 0));   //same thing as Vector3.right -> moving the object to right 1 value per frame
        //transform.Translate(Vector3.left * _speed * Time.deltaTime);   //moving object to left "_speed" units per second

        // Course 23 - Moving horizontally - x axe
        //horizontal_Input = Input.GetAxis("Horizontal");
        //transform.Translate(Vector3.right * horizontal_Input * _speed * Time.deltaTime);    //moving object to left "horizontal_Input * _speed" units per second

        // Moving vertically - y axe
        //vertical_Input = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.up * vertical_Input * _speed * Time.deltaTime);
        
        // Course 24 - Optimise upper moving code
        //transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * _speed * Time.deltaTime);
        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        //Course 25/26 - Limiting Player Moving Area
        // if player position is greater than 0 than y position = 0
        if(transform.position.y <= 0){
            // remember to use same current x position
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y >= 5f){
            transform.position = new Vector3(transform.position.x, 5f, 0);
        }

        //teleport player from left side to right side and viceversa
        if(transform.position.x >= 4){
            // remember to use same current y position
            // Right -> Left    --> disappear at 4f --> appear at -3.95f
            transform.position = new Vector3(-3.99f, transform.position.y, 0);
        }
        else if (transform.position.x <= -4f){
            // Left -> Right    --> disappear at -4f --> appear at 3.95f
            transform.position = new Vector3(3.99f, transform.position.y, 0);
        }
    }
    
    // Course 32 - Instantie objects
    public void Shoot_Laser()
    {
        // When "space" key is hitted, spawn game object (in this case laser prefab)
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _laserFirerate;   // resets a timer, determining if the cooldown is done or not

            Debug.Log("ACTION : Space key pressed");   // Debugger Unity
            Instantiate(_laserPrefab, transform.position + _laserPosition, Quaternion.identity);   //Quaternion.identity means default rotation
        }
        else
        {
            Debug.Log("ACTION : Cooldown firerate");   // Debugger Unity
        }
    }

    // Course 49 - Script Communication
    public void Damage_Player(short damage)
    {
        //player_life--;
        //player_life -= (short)1;
        player_life = (short)(player_life - damage);

        Debug.Log("Player damager : actual life : " + player_life);

        // Destroy player if life == 0
        if (player_life == 0)
        {
            spawn_manager.Player_Death();   // Communicating to Spawner Script that player is dead
            Destroy(this.gameObject);
        }
    }

    #endregion

    // EOF - End Of File
}
