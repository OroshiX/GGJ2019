using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leechlife : MonoBehaviour
{

    [SerializeField]
    private Character character;

    [SerializeField]
    private GameObject room;

    [SerializeField]
    private Sanity sanity;

    // quand il est présent, baisse de vie x2
    //au contact, baisse fixe + ralentissement
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.ROOM))
        {
            room = other.gameObject;
        }
    }
}
