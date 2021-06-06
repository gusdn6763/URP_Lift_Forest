using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] private XRController rightTeleportRay;                 //오른손 포탈레이
    [SerializeField] private XRRayInteractor leftInteractorRay;             //왼손 레이
    [SerializeField] private XRRayInteractor rightInteractorRay;            //오른손 레이
    [SerializeField] private XRNode playerMoveDevice;                       //어떠한 기기로 이동할지 정하는 변수
    [SerializeField] private float speed;                                   //플레이어 속도
    [SerializeField] private InputHelpers.Button teleportActivationButton;  //텔레포트 버튼

    private CharacterController characterController;                        //VR Rig의 캐릭터 컨트롤러
    private XRRig rig;          
    private XRController climbingHand;
    public XRController ClimbingHand { get { return climbingHand; } set { climbingHand = value; } }

    private Vector2 inputAxis;
    private bool enableRightTeleport = true;
    private Vector3 pos;
    private Vector3 norm;
    private int index;
    private bool validTarget;
    private int money;
    public int Money { get { return money; } set { money = value; } }

    public float mass = 1f;                                     //영향받는 중력크기
    public float additionalHeight = 0.2f;                       //추가적인 머리 크기
    public float activationThreshold = 0.1f;                    //텔포를 위해 버튼을 눌러야하는 시간
    public bool moveImpossible = false;                         //플레이어 이동을 금지


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        rig = GetComponent<XRRig>();
        characterController = GetComponent<CharacterController>();
    }



    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(playerMoveDevice);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        if (rightTeleportRay)
        {
            bool isRightInteractorRayHovering = rightInteractorRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
            rightTeleportRay.gameObject.SetActive(enableRightTeleport && CheckIfActivated(rightTeleportRay) && !isRightInteractorRayHovering);
        }
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        if (climbingHand)
        {
            Climb();
        }
        else if (!moveImpossible)
        {
            StartMove();
            ApplyGravity();
        }
        else
        {
            ApplyGravity();
        }
    }

    /// <summary>
    /// 변경된 높이값에 따른 PlayerController height값 변경
    /// </summary>
    void CapsuleFollowHeadset()
    {
        characterController.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        characterController.center = new Vector3(capsuleCenter.x, characterController.height / 2 + characterController.skinWidth + additionalHeight, capsuleCenter.z);
    }


    //입력받은 조이스틱값으로 이동
    void StartMove()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        characterController.Move(direction * Time.fixedDeltaTime * speed);
    }

    //중력 적용
    void ApplyGravity()
    {
        Vector3 gravity = new Vector3(0, Physics.gravity.y * mass, 0);
        gravity.y *= Time.deltaTime;
        characterController.Move(gravity * Time.deltaTime);
    }

    void Climb()
    {
        InputDevices.GetDeviceAtXRNode(climbingHand.controllerNode).TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 velocity);

        characterController.Move(transform.rotation * -velocity * Time.fixedDeltaTime);
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, teleportActivationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
    public Vector3 SetCurrentRayPos()
    {
        rightInteractorRay.TryGetHitInfo(out pos, out norm, out index, out validTarget);
        return pos;
    }
}

