using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyHpBar : MonoBehaviour
{
    private Camera uiCamera; //UI 카메라를 담을 변수
    private Canvas canvas; //캔버스를 담을 변수
    private RectTransform rectParent; //부모의 rectTransform 변수를 저장할 변수

    public RectTransform rectHpEmpty; //자신의 rectTransform 저장할 변수
    public RectTransform rectHp; //자신의 rectTransform 저장할 변수

    
    public Vector3 offset = Vector3.zero; //HpBar 위치 조절용, offset은 어디에 HpBar를 위치 출력할지
    public Transform enemyTr; //적 캐릭터의 위치

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>(); //부모가 가지고있는 canvas 가져오기, Enemy HpBar canva

        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        //rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    //LateUpdate는 update 이후 실행함, 적의 움직임은 Update에서 실행되니 움직임 이후에 HpBar를 출력함
    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(enemyTr.position + offset); //월드좌표(3D)를 스크린좌표(2D)로 변경, offset은 오브젝트 머리 위치
        var localPos = Vector2.zero;
        Debug.Log("1" + screenPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos); //스크린좌표에서 캔버스에서 사용할 수 있는 좌표로 변경?
        
       rectHp.localPosition = screenPos; //그 좌표를 localPos에 저장, 거기에 hpbar를 출력
        rectHpEmpty.localPosition = screenPos; //그 좌표를 localPos에 저장, 거기에 hpbar를 출력
    }
}
