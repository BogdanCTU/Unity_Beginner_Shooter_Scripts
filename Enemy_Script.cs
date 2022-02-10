using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    #region Variables

    // Enemy attributes
    [SerializeField]
    private float _speed = 2f;
    [SerializeField]
    private short _enemy_life = 2;   // life = life - laser_damage
    public short enemy_damage = 1;    // player_life = player_life - enemy_damage

    [SerializeField]   //for seeing it in the inspector
    private GameObject _laser_prefab;
    [SerializeField]   //for seeing it in the inspector
    private GameObject _player;

    #endregion

    #region Methods 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Enemy_Direction();
        Enemy_Destroy();
        Enemy_Respawn();
    }

    private void Enemy_Direction()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
    private void Enemy_Destroy()
    {
        //Destroy(gameObject);

    }

    // Re-use object if player does not destroy it
    // Respawn on top at a new random x position
    private void Enemy_Respawn()
    {
        if (transform.position.y <= -3f)
        {
            Enemy_Spawn();
        }
    }

    public void Enemy_Spawn()
    {
        transform.position = new Vector3(Random.Range(-3f, 3f), 13f, 0);
    }

    // Course 46 - Colliders
    private void OnTriggerEnter(Collider other)
    {
        // View who collided with the enemy Object
        Debug.Log("Enemy collides with : " + other.transform.name);   //other returns the object, transform return the root of the object

        // If enemy collides with player, damage player and destroy enemy
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            Player_Script player = other.transform.GetComponent<Player_Script>();   // to avoid null reference
            if (player != null)   // player exist
            {
                player.Damage_Player(enemy_damage);
            }
        }
        // If enemy collides with laser, destroy laser, damage enemy
        else if (other.tag == "Laser_Tag")
        {
            Destroy(other.gameObject);
            //_enemy_life = _enemy_life - _laser_prefab.
            //if(_enemy_life <= 0) 
                Destroy(this.gameObject);
        }
    }

    #endregion

    // EOF - End Of File
}
