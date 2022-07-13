using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    // 캔버스 - 조이패드 생성할 위치 크기만큼 투명한 이미지 - 조이패드 배경 - 조이스틱
    // 스크립트는 캔버스에, 조이패드 배경에 조이스틱 들어있는 프리팹, 조이스틱

    public enum EventHandle { Click, Drag }
    public EventHandle ePrevEvent;

    //private RectTransform m_BackGround;

    public GameObject joyStickBackGround;
    public GameObject joyStick;

    private RectTransform transJoyStickBackGround;
    private RectTransform transJoyStick;

    public Vector2 vecJoystickValue { get; private set; }
    public Vector3 vecJoyRotValue { get; private set; }

    public float Horizontal { get { return vecJoystickValue.x; } }
    public float Vertical { get { return vecJoystickValue.y; } }

    private float fRadius;

    public enum PlayerState { Idle, Attack, Move, End }
    public PlayerState ePlayerState { get; private set; }


    private void Awake()
    {
        Init();
    }

    #region event
    public void OnPointerClick(PointerEventData eventData)
    {
        SetPlayerState(PlayerState.Idle);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CallJoyStick(eventData);
        SetHandleState(EventHandle.Click);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joyStickBackGround.SetActive(false);

        if (ePrevEvent == EventHandle.Drag)
            return;

        SetPlayerState(PlayerState.Attack);
        SetHandleState(EventHandle.Click);
    }

    public void OnDrag(PointerEventData eventData)
    {
        JoyStickMove(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        JoyStickMoveEnd(eventData);
    }
    #endregion

    private void Init()
    {
        transJoyStickBackGround = joyStickBackGround.GetComponent<RectTransform>();
        transJoyStick = joyStick.GetComponent<RectTransform>();
        fRadius = transJoyStickBackGround.rect.width * 0.5f; //조이스틱의 행동반경 계산

        joyStick.SetActive(true);
        joyStickBackGround.SetActive(false);
    }

    private void JoyStickMoveEnd(PointerEventData eventData)
    {
        vecJoystickValue = Vector3.zero;

        transJoyStick.position = eventData.position;
        joyStickBackGround.SetActive(false);

        SetHandleState(EventHandle.Click);
        SetPlayerState(PlayerState.Idle);
    }

    private void CallJoyStick(PointerEventData eventData)
    {
        joyStickBackGround.transform.position = eventData.position;
        joyStick.transform.position = eventData.position;
        joyStickBackGround.SetActive(true);
    }

    private void JoyStickMove(PointerEventData eventData)
    {
        vecJoystickValue = eventData.position - (Vector2)transJoyStickBackGround.position;

        vecJoystickValue = Vector2.ClampMagnitude(vecJoystickValue / 32 /*드래그 이동량*/, fRadius);
        transJoyStick.localPosition = vecJoystickValue /* / 2 조이스틱 최대 행동반경*/;

        vecJoyRotValue = new Vector3(transJoyStick.localPosition.x, 0f, transJoyStick.localPosition.y);

        SetHandleState(EventHandle.Drag);
        SetPlayerState(PlayerState.Move);
    }

    private void SetHandleState(EventHandle _handle)
    {
        ePrevEvent = _handle;
    }

    private void SetPlayerState(PlayerState _state)
    {
        ePlayerState = _state;
    }
}