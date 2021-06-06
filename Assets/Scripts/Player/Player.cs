using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] private XRController rightTeleportRay;                 //������ ��Ż����
    [SerializeField] private XRRayInteractor leftInteractorRay;             //�޼� ����
    [SerializeField] private XRRayInteractor rightInteractorRay;            //������ ����
    [SerializeField] private XRNode playerMoveDevice;                       //��� ���� �̵����� ���ϴ� ����
    [SerializeField] private float speed;                                   //�÷��̾� �ӵ�
    [SerializeField] private InputHelpers.Button teleportActivationButton;  //�ڷ���Ʈ ��ư

    private CharacterController characterController;                        //VR Rig�� ĳ���� ��Ʈ�ѷ�
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

    public float mass = 1f;                                     //����޴� �߷�ũ��
    public float additionalHeight = 0.2f;                       //�߰����� �Ӹ� ũ��
    public float activationThreshold = 0.1f;                    //������ ���� ��ư�� �������ϴ� �ð�
    public bool moveImpossible = false;                         //�÷��̾� �̵��� ����


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
    /// ����� ���̰��� ���� PlayerController height�� ����
    /// </summary>
    void CapsuleFollowHeadset()
    {
        characterController.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        characterController.center = new Vector3(capsuleCenter.x, characterController.height / 2 + characterController.skinWidth + additionalHeight, capsuleCenter.z);
    }


    //�Է¹��� ���̽�ƽ������ �̵�
    void StartMove()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        characterController.Move(direction * Time.fixedDeltaTime * speed);
    }

    //�߷� ����
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

