using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    GameObject goThis;
    GameObject goPoint;

    /*
    객체의 생성은 종전의 방식과 다르다
    유니티에서는 클래스(Origin)와 객체(Inst)변수 2개를 생성한다
    1. 먼저 스크립트에서 public GameObject로 변수를 2개 생성한다(cannonOrigin, cannonInst)
    2. 유니티 툴에서 Inspector에서 scrip 부분에서 확인이 가능하다면 Origin쪽에다 생성한 그래픽 객체를 연결한다
    3. 스크립트로 돌아와서 cannonInst = Instantiate(cannonOrigin) as GameObject;와 같은 식으로 Origin으로
       인스턴스(Inst)를 생성하여 작동시킨다
    */
    public GameObject cannonOrigin; //원본
    public bool ___________________________; // 그래픽 객체를 담은 클래스와 실제 생성되는 인스턴스를 구분하기 위하여 임시로 하나의 변수를 생성한다
    public GameObject cannonInst; //인스턴스(객체) = null
    private bool isAimingMode;


    /// <summary>
    /// 프로그램 실행될 때 최초에 한번만 실행
    /// </summary>
    private void Awake()
    {
        Transform point = transform.Find("point");
        goPoint = point.gameObject;
        goPoint.SetActive(false);
    }

    /// <summary>
    /// 창이 활성화 될 때마다 실행된다. 즉 다른프로그램 클릭했다가 창이 활성화 되면
    /// 다시 실행된다
    /// </summary>
    private void Start()
    {
        //goThis = this.gameObject;
        //goPoint = goThis.transform.Find("point").gameObject;
        //// 위와 동일 Transform point = transform.Find("point");
        //goPoint.SetActive(false);

        

    }

    private void OnMouseEnter()
    {
        goPoint.SetActive(true);

    }

    private void OnMouseExit()
    {
        goPoint.SetActive(false);

    }


    /*
    OnMouseDown()이벤트와 OnMouseUp()이벤트의 경우에는
    객에 위에서만 이벤트가 동작한다

    하지만, Update()이벤튼 내에서 
    if (Input.GetMouseButtonUp(0)) { } 메소드를 사용한다면
    화면 어디에 있던지 이벤트가 동작한다
    */
    private void OnMouseDown()
    {
        /*
        유니티에서 클래스의 생성은 직접 코딩으로 생서하는 것이 아니다.
        먼저 유니티 툴로 그래픽 객체를 그린다.
        그 뒤 그것을 Assets폴더로 옮기면 클래스가 생성되는 것이다.
        그 뒤 다시 유니티 툴의 Hierarchy에서 삭제를 하는 방식(안지워도 됨)으로 생성한다

        이렇게 생성된 클래스를 스크립트에서
        Instantiate(클래스명) 이런 식으로 객체화 하여 사용한다
        */

        isAimingMode = true;

        // 탄환 생성
        cannonInst = Instantiate(cannonOrigin) as GameObject;
        cannonInst.transform.position = goPoint.transform.position;
    }

    private void OnMouseUp()
    {
        
    }

    /// <summary>
    /// 시스템 사양에 따라 업데이트 되는 속도가 다르다
    /// </summary>
    private void Update()
    {
        if (!isAimingMode)
            return;

        // 2D mouse 좌표 ->  3D
        Vector3 mousePos = Input.mousePosition; // 2D position z축은?
        mousePos.z = -Camera.main.transform.position.z;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // 탄환 이동(당겨짐)
        Vector3 delta = mousePos - goPoint.transform.position; //delta = 마우스 위치에서 goPoint객체까지의 거리
                                                               //delta.magnitude; //delta.magnitude : 델타값의 절대값

        float maxMaginitude = this.GetComponent<SphereCollider>().radius;

        if (delta.magnitude > maxMaginitude)
        {
            delta.Normalize();
            delta *= maxMaginitude * 3;
        }

        cannonInst.transform.position = goPoint.transform.position + delta;

        //print("update");
        if (Input.GetMouseButtonUp(0)) //버튼을 눌렀다 떼면 // 매개변수[0 : 왼쪽 버튼, 1 : 오른쪽 버튼, 2 : 가운데 버튼]
        {

        }
    }

    /// <summary>
    /// 모든 시스템에서 동일한 속도로 업데이트 된다
    /// </summary>
    //private void FixedUpdate()
    //{
    //    print("fixedupdate");

    //}
}
