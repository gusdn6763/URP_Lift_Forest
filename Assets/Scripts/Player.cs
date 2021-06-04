using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public static Player instance;

    public XRController rightTeleportRay;
    public XRRayInteractor leftInteractorRay;
    public XRRayInteractor rightInteractorRay;
    [SerializeField] private XRNode playerMoveDevice;                //어떠한 기기로 이동할지 정하는 변수
    [SerializeField] protected float speed;         //플레이어 속도
    public InputHelpers.Button teleportActivationButton;

    private CharacterController characterController;     //VR Rig의 캐릭터 컨트롤러
    private XRRig rig;

    [HideInInspector] public XRController climbingHand;
    private Vector2 inputAxis;
    public float mass = 1f;                                     //영향받는 중력크기
    public float additionalHeight = 0.2f;                       //추가적인 머리 크기
    public float activationThreshold = 0.1f;
    public bool moveImpossible = false;                         //플레이어 이동을 금지

    public bool EnableRightTeleport { get; set; } = true;

    Vector3 pos;
    Vector3 norm;
    int index;
    bool validTarget;

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
            rightTeleportRay.gameObject.SetActive(EnableRightTeleport && CheckIfActivated(rightTeleportRay) && !isRightInteractorRayHovering);
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

}

