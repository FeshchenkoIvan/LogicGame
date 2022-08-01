using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeControl : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    private GameObject camera;
    //[SerializeField]
    //private ButtonCameraRotation buttonCameraRotation;
    [SerializeField]
    private ButtonDeleteFigure buttonDeleteFigure;
    [SerializeField]
    private ButtonObjectRotation buttonObjectRotation;

    private Camera MainCameraConfig;
    public GameObject panelFigures;
    public GameObject figureSelectedButton;
    public bool ARmod;
    public int speedMove=10;
    public int lengthSwipe = 200;
    public float X = 0;
    public float Y = 0;
    public float swipeX = 0;
    public float swipeY = 0;
    public float OffsetCentreX = 0;//эксперимент
    public float OffsetCentreZ = 0;//эксперимент
    float timePressOnFigure;
    public bool collision;
    public bool CameraRotation;
    public bool ObjectRotation;
    public bool ObjectMove;
    public bool StartTime = false;
    public bool TimerDoubleClick = false;
    public bool touchPanelFigures = false;
    public bool scrollPanelFigures = false;
    public bool DragingFigures = false;
    public bool checkAddFigure = false;
    public string CommandRotation;
    public string figureName="";
    public string ActiveFigureName;
    public bool PhaseBeganFigure;
    private int lyerMask;
    void Start()
    {
        figureSelectedButton = null;
        MainCameraConfig = Camera.main;
        ObjectRotation = false;
        ObjectMove = false;
        CommandRotation = "";
        ActiveFigureName = "";
        //lyerMask = (1 << LayerMask.NameToLayer("Ground"));
        lyerMask = (1 << LayerMask.NameToLayer("Ground"))| (1 << LayerMask.NameToLayer("Figure"));
        if (ARmod)
            CameraRotation = false;
        else
            CameraRotation = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //X += eventData.delta.x;
        //Y += eventData.delta.y;
        Debug.Log("OnBeginDrag");
    }

    public void Update()
    {
        if (TimerDoubleClick)
        {
            timePressOnFigure -= Time.deltaTime;
            if (timePressOnFigure < 0) TimerDoubleClick = false;
        }
        
        foreach (Touch touch in Input.touches)
        {
            if (touch.fingerId == 0)
            {
                RaycastHit hit;
                Ray ray = camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(touch.position.x / MainCameraConfig.pixelWidth, touch.position.y / MainCameraConfig.pixelHeight, 0));
                Physics.Raycast(ray, out hit);
                Debug.DrawLine(ray.origin, hit.point, Color.green, 10f);

                PickActiveFigure(hit);//выбор фигуры для управления

                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (touch.position.y<320) //если палец попал на панель фигур
                    {
                        touchPanelFigures = true;
                    }
                    else
                        if (ObjectMove)
                        {
                            if (hit.collider != null)
                            {
                                if (hit.collider.gameObject.tag == "Figure")
                                {
                                    if (hit.collider.gameObject.name == ActiveFigureName)
                                    {
                                        PhaseBeganFigure = true;
                                        TimerDoubleClick = false;
                                        var PositionFigure = hit.transform.root.position;
                                        OffsetCentreX = hit.point.x-PositionFigure.x;//смещение центра фигуры от точки перемещения фигуры пальцем
                                        OffsetCentreZ = hit.point.z-PositionFigure.z;
                                        //Debug.Log("OffsetCentreX=" + OffsetCentreX + ", OffsetCentreZ=" + OffsetCentreZ);
                                    }
                                    else CheckDoubleClick();
                                }
                                else CheckDoubleClick();
                            }
                            else CheckDoubleClick();
                        }
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if (ActiveFigureName != "")//останавливаем фигуру
                        GameObject.Find(ActiveFigureName).GetComponent<Rigidbody>().velocity = Vector3.zero;

                    PhaseBeganFigure = false;
                    touchPanelFigures = false;
                    scrollPanelFigures = false;
                    DragingFigures = false;
                    OffsetCentreX = 0;
                    OffsetCentreZ = 0;
                    if (ObjectRotation) {swipeX = 0;swipeY = 0;}
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (touchPanelFigures)//если попали по панели фигур
                    {
                        if (scrollPanelFigures || DragingFigures)
                        {
                            if (scrollPanelFigures)
                                ScrollPanelFigures(touch.deltaPosition.x);
                            else
                                if (PhaseBeganFigure==true)//продолжение передвижение фигуры по плоскости после перенесения с панели фигур
                                    MovingFigure();
                                else
                                    addFigure(touch); 
                        }
                        else
                            if (Mathf.Abs(touch.deltaPosition.x) > Mathf.Abs(touch.deltaPosition.y))//либо скролим панель либо перетаскиваем фигуру
                                scrollPanelFigures = true;
                            else { DragingFigures = true; }
                    }
                }
                if (Input.GetTouch(0).phase == TouchPhase.Stationary)//пересмотреть  для AR!!!*
                {
                    if (!CameraRotation)
                        MovingFigure();
                }
            }
        }
    }
    public void addFigure(Touch touch)
    {
        if (touch.position.y > 320)
        {
            if (figureSelectedButton != null)
            {
                RaycastHit hit;
                Ray ray = camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(touch.position.x / MainCameraConfig.pixelWidth, touch.position.y / MainCameraConfig.pixelHeight, 0));
                Physics.Raycast(ray, out hit);
                if (System.Math.Abs(hit.point.x)<=8.5 && System.Math.Abs(hit.point.z) <= 8.5) //Проверка на попадание на Ground//для AR подумать!!!
                {
                    if (Physics.Raycast(ray, out hit, 150, lyerMask))
                    {
                        Vector3 NewPosition = new Vector3(hit.point.x, 1, hit.point.z);
                        figureSelectedButton.transform.position = NewPosition;

                        ObjectMove = true;
                        checkAddFigure = true;
                        PhaseBeganFigure = true;

                        if (ActiveFigureName != figureSelectedButton.name) //Установка настроек для активной фигуры и сброс для не активной
                        {
                            if (ActiveFigureName != "") 
                            {
                                SelectActiveFigure(ActiveFigureName, false);
                            }
                            ActiveFigureName = figureSelectedButton.name;
                            SelectActiveFigure(ActiveFigureName, true);
                        }
                    }
                }
            }
        }
    }
    public void ScrollPanelFigures(float deltaX)
    {
        if (panelFigures.transform.position.x + deltaX < 0 && panelFigures.transform.position.x + deltaX > -1000)//-720)
        {
            Vector3 NewPosition = panelFigures.transform.position;
            NewPosition.x += deltaX;
            panelFigures.transform.position = NewPosition;
        }
    }
    public void PickActiveFigure(RaycastHit hit) //выбор фигуры для управления
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Debug.DrawLine(ray.origin, hit.point, Color.red, 10f);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag=="Figure")
                {
                    //ActiveFigureName = hit.collider.gameObject.transform.parent.name;
                    figureName = hit.collider.gameObject.name;
                    StartTime = true;
                    timePressOnFigure = 0.8f;
                }
            }
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            if (StartTime)
                StartTime = false;
        }

        if (Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            if (StartTime)
            {
                if (timePressOnFigure > 0)
                {
                    timePressOnFigure -= Time.deltaTime;
                }
                else
                {
                    StartTime = false;
                    ObjectMove = true;
                    if (ActiveFigureName != "")
                    {
                        SelectActiveFigure(ActiveFigureName, false);
                    }
                    ActiveFigureName = figureName;
                    SelectActiveFigure(ActiveFigureName, true);
                }
            }
        }
        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            if (StartTime == true)
            {
                //timePressOnFigure = 0.8f;
                StartTime = false;
                ObjectMove = false;
                //ActiveFigureName = "";
                figureName = "";
            }
        }
    }
    private void CheckDoubleClick()
    {
        if (TimerDoubleClick && timePressOnFigure > 0)
        {
            TimerDoubleClick = false;
            SelectActiveFigure(ActiveFigureName, false);
            ActiveFigureName = "";
            ObjectMove = false;
        }
        if (!TimerDoubleClick)
        {
            timePressOnFigure = 0.3f;
            TimerDoubleClick = true;
        }
    }
    public void SelectActiveFigure(string NameFigure, bool b)
    {
        GameObject.Find(ActiveFigureName).GetComponent<Rotation>().figureActive = b;
        if (GameObject.Find(ActiveFigureName).GetComponent<ActivateFigure>() != null)
        {
            GameObject.Find(ActiveFigureName).GetComponent<ActivateFigure>().activateFigure(b);
        }

        buttonObjectRotation.ActiveButtonObjRotation(b);
        buttonDeleteFigure.ActiveButtonDeleteFigure(b);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!CameraRotation) //подумать для АR
            MovingFigure();

        if (ObjectRotation && CommandRotation == "")
        {
            swipeX += eventData.delta.x;
            swipeY += eventData.delta.y;

            if ((System.Math.Abs(swipeX) > lengthSwipe) || (System.Math.Abs(swipeY) > lengthSwipe))
            {
                if (System.Math.Abs(swipeX) > System.Math.Abs(swipeY))
                {
                    if (swipeX > 0)
                    {
                        CommandRotation = "right";
                        Debug.Log("right");
                    }
                    else
                    {
                        CommandRotation = "left";
                        Debug.Log("left");
                    }
                }
                else
                    if (swipeY > 0)
                {
                    CommandRotation = "up";
                    Debug.Log("up");
                }
                else
                {
                    CommandRotation = "down";
                    Debug.Log("down");
                }
                swipeX = 0;
                swipeY = 0;
            }
        }
    }

    public void MovingFigure()
    {
        if (ObjectMove && PhaseBeganFigure && !ObjectRotation)
        {
            Touch[] touch = Input.touches;
            RaycastHit hit;
            Ray ray = camera.GetComponent<Camera>().ViewportPointToRay(new Vector3(touch[0].position.x / MainCameraConfig.pixelWidth, touch[0].position.y / MainCameraConfig.pixelHeight, 0));
            //Physics.Raycast(ray, out hit);
            //Debug.DrawLine(ray.origin, hit.point, Color.black, 10f);

            if (Physics.Raycast(ray, out hit, 50, lyerMask))
            {
                Vector3 figurePosition = GameObject.Find(ActiveFigureName).transform.position;

                figurePosition.x += OffsetCentreX;
                figurePosition.z += OffsetCentreZ;

                if ((hit.point.y < 0.98))
                {
                    if (figurePosition.y <= 1)
                    {
                        GameObject.Find(ActiveFigureName).GetComponent<СenteringObject>().CalibrationHeight2(1);
                        figurePosition.y = 0;
                    }
                    else
                    {
                        figurePosition.y = 1;
                    }
                }
                else
                {
                    if ((hit.point.y >= 0.98) && (hit.point.y < 1.98))
                    {
                        if (figurePosition.y < 2)
                            GameObject.Find(ActiveFigureName).GetComponent<СenteringObject>().CalibrationHeight2(2);
                    }
                    if ((hit.point.y >= 1.98) && (hit.point.y < 2.98))
                    {
                        if (figurePosition.y < 3)
                            GameObject.Find(ActiveFigureName).GetComponent<СenteringObject>().CalibrationHeight2(3);
                    }
                    if ((hit.point.y >= 2.98))
                    {
                        if (figurePosition.y < 4)

                            GameObject.Find(ActiveFigureName).GetComponent<СenteringObject>().CalibrationHeight2(4);
                    }
                    figurePosition.y = 0;
                }
                
                //Debug.Log("hitPoint.y="+hit.point.y);

                Vector3 direction = new Vector3(hit.point.x, 0, hit.point.z) - figurePosition;

                if (Vector3.Magnitude(direction) > 3)
                {
                    GameObject.Find(ActiveFigureName).GetComponent<Rigidbody>().velocity = direction * speedMove;
                }
                else
                {
                    if (Vector3.Magnitude(direction) < 1f)
                    {
                        GameObject.Find(ActiveFigureName).GetComponent<Rigidbody>().velocity = direction * Vector3.Magnitude(direction) * speedMove;
                    }
                    else
                        GameObject.Find(ActiveFigureName).GetComponent<Rigidbody>().velocity = direction * (3 / Vector3.Magnitude(direction)) * speedMove;
                }
                //Debug.Log("магнитуда="+  Vector3.Magnitude(direction));
                //Debug.DrawLine(ray.origin, hit.point, Color.green, 10f);
            }
        }
    }
}

//if ((hit.point.y < 0.98))
//{
//    if (figurePosition.y <= 1)
//    {
//        //figurePosition.y = 1;
//        //GameObject.Find(ActiveFigureName).transform.position = figurePosition;
//        GameObject.Find(ActiveFigureName).GetComponent<СenteringObject>().CalibrationHeight2(1);
//        figurePosition.y = 0;
//    }
//    else
//    {
//        figurePosition.y = 1;
//    }
//}
//if ((hit.point.y >= 0.98) && (hit.point.y < 1.98))
//{
//    if (figurePosition.y < 2)
//    {
//        //figurePosition.y = 2;
//        //GameObject.Find(ActiveFigureName).transform.position = figurePosition;
//        GameObject.Find(ActiveFigureName).GetComponent<СenteringObject>().CalibrationHeight2(2);
//        figurePosition.y = 0;
//    }
//    else
//    {
//        figurePosition.y = 0;
//    }
//}
//if ((hit.point.y >= 1.98) && (hit.point.y < 2.98))
//{
//    if (figurePosition.y < 3)
//    {
//        //figurePosition.y = 3;
//        //GameObject.Find(ActiveFigureName).transform.position = figurePosition;
//        GameObject.Find(ActiveFigureName).GetComponent<СenteringObject>().CalibrationHeight2(3);
//        figurePosition.y = 0;
//    }
//    else
//    {
//        figurePosition.y = 0;
//    }
//}
//if ((hit.point.y >= 2.98))
//{
//    if (figurePosition.y < 4)
//    {
//        //figurePosition.y = 4;
//        //GameObject.Find(ActiveFigureName).transform.position = figurePosition;
//        GameObject.Find(ActiveFigureName).GetComponent<СenteringObject>().CalibrationHeight2(4);
//        figurePosition.y = 0;
//    }
//    else
//    {
//        figurePosition.y = 0;
//    }
//}