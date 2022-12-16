using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    public GameObject _sphere;
    public GameObject _capsule;
    public bool _onoff;
    void Update()
    {
        if (_onoff == true)
        {
            StartCoroutine(_SpawnObject());
        }

    }
    IEnumerator _SpawnObject()
    {
        _Selectitem();
        _onoff = false;
        yield return new WaitForSeconds(2);
        _onoff = true;
    }

    void _Selectitem()
    {
        int _Select = Random.Range(1, 3);

        if (_Select == 1)
        {
            Instantiate(_sphere, transform.position, transform.rotation);
        }
        else if (_Select == 2)
        {
            Instantiate(_capsule, transform.position, transform.rotation);
        }

        
    }
}
