using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;
    public LayerMask decorMask;
    public float height;

    Vector3 offset;
    GameSettings gs;

    private void OnEnable()
    {
        gs = FindObjectOfType<SettingsManager>().gameSettings;
    }


    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
        if (target.position.z < -35)
        {
            Vector3 A = transform.position;
            A.z = -42;
            transform.position = A;
        }
        if (target.position.z < transform.position.z)
        {
            Vector3 A = transform.position;
            A.z = target.position.z;
            transform.position = A;
        }
        transform.LookAt(target, Vector3.up);
    }
}