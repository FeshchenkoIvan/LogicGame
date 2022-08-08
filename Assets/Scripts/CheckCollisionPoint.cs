using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionPoint : MonoBehaviour
{
    [SerializeField] private MeshRenderer cube;
    public bool collision=false;

    private void OnTriggerEnter(Collider other)
    {
        collision = true;
        cube.enabled = false;
    }
    private void OnTriggerExit(Collider other)
    {
        collision = false;
        cube.enabled = true;
    }
    private void OnTriggerStay(Collider other)
    {
        collision = true;
        cube.enabled = false;
    }
}
