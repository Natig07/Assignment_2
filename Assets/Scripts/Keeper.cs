using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keeper : MonoBehaviour
{

    // Update is called once per frame
    private Vector3 _startPosition;
    [SerializeField] float keepspeed = 1.1f;
    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        transform.position = _startPosition + new Vector3(Mathf.Sin(Time.time*keepspeed)*2.1f, 0.0f, 0.0f);
    }
}

