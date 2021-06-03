using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmemyHpBar : MonoBehaviour
{
    private Camera uiCamera; //UI ī�޶� ���� ����
    private Canvas canvas; //ĵ������ ���� ����
    private RectTransform rectParent; //�θ��� rectTransform ������ ������ ����

    public RectTransform rectHpEmpty; //�ڽ��� rectTransform ������ ����
    public RectTransform rectHp; //�ڽ��� rectTransform ������ ����

    
    public Vector3 offset = Vector3.zero; //HpBar ��ġ ������, offset�� ��� HpBar�� ��ġ �������
    public Transform enemyTr; //�� ĳ������ ��ġ

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInParent<Canvas>(); //�θ� �������ִ� canvas ��������, Enemy HpBar canva

        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        //rectHp = this.gameObject.GetComponent<RectTransform>();
    }

    //LateUpdate�� update ���� ������, ���� �������� Update���� ����Ǵ� ������ ���Ŀ� HpBar�� �����
    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(enemyTr.position + offset); //������ǥ(3D)�� ��ũ����ǥ(2D)�� ����, offset�� ������Ʈ �Ӹ� ��ġ
        var localPos = Vector2.zero;
        Debug.Log("1" + screenPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, uiCamera, out localPos); //��ũ����ǥ���� ĵ�������� ����� �� �ִ� ��ǥ�� ����?
        
       rectHp.localPosition = screenPos; //�� ��ǥ�� localPos�� ����, �ű⿡ hpbar�� ���
        rectHpEmpty.localPosition = screenPos; //�� ��ǥ�� localPos�� ����, �ű⿡ hpbar�� ���
    }
}
