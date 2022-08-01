using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class InfoPoseAR : MonoBehaviour
{
    public Text TextCamera;
    public Text TextARFound;
    public GameObject GOARFound;
    public GameObject GOCamera;
    //public Camera ARCamera2;
    //public ARSessionOrigin ARsessionOrigin;
    // Start is called before the first frame update
    void Start()
    {
        //ARsessionOrigin.camera = ARCamera2;
    }

    // Update is called once per frame
    void Update()
    {
        TextCamera.text = "Позиция Y:"+ GOCamera.transform.position.y.ToString()+
            "\nARmod="+ DataHolder.ARmod.ToString();
        //TextARFound.text = "Позиция Y:" + GOARFound.transform.position.y.ToString() +
        //    "\nЛ.Позиция Y:" + GOARFound.transform.localPosition.y.ToString();
    }
}
