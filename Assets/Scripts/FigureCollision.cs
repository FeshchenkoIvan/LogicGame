using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureCollision : MonoBehaviour
{
    public bool collision;
    public bool active;
    Vector3 directionCollision;
    Vector3 directionCorrect;
    public SwipeControl swipeControl;
    // Start is called before the first frame update
    void Start()
    {
        //TriggerCollision = transform.GetComponentInParent<ÑenteringObject>().Collision;
        active=transform.GetComponentInParent<ActivateFigure>().active;
        collision = false;
        directionCorrect = new Vector3(0, 0, 0);
        swipeControl = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<SwipeControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (swipeControl.collision && !collision)
        {
            swipeControl.collision = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //transform.GetComponentInParent<ÑenteringObject>().Collision = true;
        Debug.Log("Ñòîëêíóëñÿ ñ "+ other.transform.root.name);
        collision = true;
        swipeControl.collision = true;
        directionCollision = transform.position - other.transform.position;
        Debug.Log("directionCollision= " + directionCollision);
        if (System.Math.Abs(Vector3.Dot(directionCollision, Vector3.right)) > System.Math.Abs(Vector3.Dot(directionCollision, Vector3.forward)))
        {
            if (Vector3.Dot(directionCollision, Vector3.right) > 0)
            {
                directionCorrect.x = 0.1f;
                //directionCorrect.x = System.Math.Abs(directionCollision.z);
            }

            else
            {
                directionCorrect.x = -0.1f;
                //directionCorrect.x = -System.Math.Abs(directionCollision.z);
            }
        }
        else
        {
            if (Vector3.Dot(directionCollision, Vector3.forward) > 0)
                directionCorrect.z = 0.1f;
                //directionCorrect.z = System.Math.Abs(directionCollision.x);
            else
                directionCorrect.z = -0.1f;
                //directionCorrect.z = -System.Math.Abs(directionCollision.x);
        }
        //directionCorrect.y = transform.root.position.y;
        Debug.Log("directionCorrect=" + directionCorrect);
        Debug.Log("other.transform.position=" + other.transform.position);
    }
    void OnTriggerExit(Collider other)
    {
        swipeControl.collision = false;
        //transform.GetComponentInParent<ÑenteringObject>().Collision = false;
        Debug.Log("Âûøåë èç " + other.transform.root.name);
        collision = false;
        directionCorrect = new Vector3(0, 0, 0);
    }
    private void OnTriggerStay(Collider other)
    {
        collision = true;
        active = transform.GetComponentInParent<ActivateFigure>().active;
        if (active)
        {
            transform.root.position += directionCorrect;
        }
    }
}
