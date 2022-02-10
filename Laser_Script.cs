using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Script : MonoBehaviour
{
    #region Variables

    [SerializeField]
    private float _speed = 10f;

    public short laser_damage = 1;

    #endregion

    #region Methods 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Laser_Direction();
        Destroy_Laser();
    }

    private void Laser_Direction()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    private void Destroy_Laser()
    {
        if (transform.position.y >= 10f)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    // EOF - End Of File
}
