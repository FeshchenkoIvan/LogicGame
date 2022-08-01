using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    ARRaycastManager ARRaycastManager;
    [SerializeField] private ARSessionOrigin sessionOrigin;
    private Vector2 touchPosition;
    public GameObject ScenePrefab;
    public GameObject CameraAR;
    public GameObject WorldCenter;
    static List<ARRaycastHit> Hits = new List<ARRaycastHit>();
    public Text ConsolInfo;

    private bool PostedPrefab = false;
    private TrackableId trackableIdPlane;
    private ARPlaneManager ARPlaneManager;
    void Awake()
    {
        ARRaycastManager = GetComponent<ARRaycastManager>();
        ScenePrefab.SetActive(false);
    }
    void Update()
    {
        if (!PostedPrefab)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                if (ARRaycastManager.Raycast(touchPosition, Hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = Hits[0].pose;
                    ConsolInfo.text = "hitPose.Y=" + hitPose.position.y.ToString();
                   
                    trackableIdPlane = Hits[0].trackableId;
                    ClearTrackablesObjects();

                    //GameObject PlaneHit = Hits[0].trackable.gameObject;
                    sessionOrigin.MakeContentAppearAt(WorldCenter.transform, hitPose.position);
                    ScenePrefab.SetActive(true);
                    PostedPrefab = true;
                }
            }
        }
    }
    void ClearTrackablesObjects()
    {
        ARPlaneManager = gameObject.GetComponent<ARPlaneManager>();
        var ARPointManager = gameObject.GetComponent<ARPointCloudManager>();

        foreach (var point in ARPointManager.trackables)
        {
            point.gameObject.SetActive(false);
        }

        foreach (var plane in ARPlaneManager.trackables)
        {
            //if (trackableIdPlane != plane.trackableId)
            //{
            //    plane.gameObject.SetActive(false);
            //}
            plane.gameObject.SetActive(false);
        }

        ARPlaneManager.enabled = false;
        ARPointManager.enabled = false;
    }
    void LookAtPlayer(Transform scene) //LookAtPlayer(ScenePrefab.transform);
    {
        var lookDirection = Camera.main.transform.position - scene.position;
        lookDirection.y = 0;
        scene.rotation = Quaternion.LookRotation(lookDirection);
    }

    public void UpCamera()
    {
        sessionOrigin.MakeContentAppearAt(CameraAR.transform, CameraAR.transform.position + new Vector3(0,1,0));
    }
    public void DownCamera()
    {
        sessionOrigin.MakeContentAppearAt(CameraAR.transform, CameraAR.transform.position + new Vector3(0, -1, 0));
    }
}
