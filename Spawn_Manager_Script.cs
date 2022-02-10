using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager_Script : MonoBehaviour
{

    #region Variables

    [SerializeField]
    private GameObject _enemy_prefab;
    private Vector3 _spawn_position;

    // Course 54 - Clean up Hierarchy with Containers
    [SerializeField]
    private GameObject _enemy_container;

    private bool player_alive = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn_Routine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn Game Objects every 5 seconds
    // create a coroutine of type IEnumerator --> using yield events

    // Course 54 - Coroutine
    private IEnumerator Spawn_Routine()
    {
        // while loop (infinite)
            // Instantiee enemy prefab
            // yield wait for 5 seconds
        while (player_alive)   // will spawn while player is alive
        {
            Debug.Log("Enemy spawned;");
            _spawn_position = new Vector3(Random.Range(-3f, 3f), 13f, 0);
            //Instantiate(_enemy_prefab, _spawn_position, Quaternion.identity);   //Quaternion.identity means default rotation

            // Course 54 - Refference for the instantiated object
            GameObject new_enemy = Instantiate(_enemy_prefab, _spawn_position, Quaternion.identity);   //Quaternion.identity means default rotation
            new_enemy.transform.parent = _enemy_container.transform;    //for hierarchy container

            yield return new WaitForSeconds(3f);   // wait specified time and then execute next code
            // execute code here after time passed
        }
    }

    public void Player_Death()
    {
        player_alive = false;
    }
}
