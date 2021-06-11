using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.XR;

/// <summary>
/// 그리드 시스템을 이용한 건물 건설 및 건설 가능 여부 체크
/// </summary>
public class GridBuildingSystem3D : MonoBehaviour
{
    public static GridBuildingSystem3D instance;

    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList = null;
    [SerializeField] private XRNode ChangeDevice;
    [SerializeField] private bool debugGrid = false;
    [SerializeField] private Vector3Int startPoint;

    private InputDevice device;
    private BuildingGhost visualBuilding;
    private GridXZ<GridObject> grid;
    public PlacedObjectTypeSO currentPlacedObjectTypeSO;
    private Dir currentPlacedObjectTypeSODir;

    public int gridWidth;
    public int gridHeight;
    public float cellSize;
    private bool canBuild = false;
    private bool oneClick = false;
    private bool oneClickRotation = false;


    public bool debuing = false;
    public bool CanBuild
    {
        get { return canBuild; }
        set { canBuild = value; visualBuilding.ChangeMaterial(canBuild); }
    }

    private void Awake()
    {
        instance = this;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, startPoint, (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y), debugGrid);
        visualBuilding = GetComponent<BuildingGhost>();
    }

    public class GridObject
    {

        private GridXZ<GridObject> grid;
        private int x;
        private int y;
        private bool defaultObject = false;
        public PlacedObject_Done placedObject;

        public GridObject(GridXZ<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            placedObject = null;
        }

        public override string ToString()
        {
            return x + ", " + y + "\n" + placedObject;
        }

        public void SetPlacedObject(PlacedObject_Done placedObject)
        {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, y);
        }

        public void SetDefaultObject()
        {
            defaultObject = true;
        }

        public void ClearPlacedObject()
        {
            placedObject = null;
            grid.TriggerGridObjectChanged(x, y);
        }

        public PlacedObject_Done GetPlacedObject()
        {
            return placedObject;
        }

        public bool CanBuild()
        {
            return (placedObject == null && defaultObject == false);
        }

    }

    private void FixedUpdate()
    {
        device = InputDevices.GetDeviceAtXRNode(ChangeDevice);
        if (currentPlacedObjectTypeSO != null)
        {
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out bool isOn) && isOn)
            {
                if (oneClickRotation)
                {
                    currentPlacedObjectTypeSODir = PlacedObjectTypeSO.GetNextDir(currentPlacedObjectTypeSODir);
                    oneClickRotation = false;
                }
            }
            else
            {
                oneClickRotation = true;
            }

            Vector3 mousePosition = Player.instance.SetCurrentRayPos();
            grid.GetXZ(mousePosition, out int x, out int z);

            Vector2Int placedObjectOrigin = new Vector2Int(x, z);
            placedObjectOrigin = grid.ValidateGridPosition(placedObjectOrigin);

            List<Vector2Int> gridPositionList = currentPlacedObjectTypeSO.GetGridPositionList(placedObjectOrigin, currentPlacedObjectTypeSODir);
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if ((gridPosition.x + 1 > gridWidth || gridPosition.y + 1 > gridHeight) || !grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild())
                {
                    CanBuild = false;
                    break;
                }
                else
                {
                    CanBuild = true;
                }
            }
            device.TryGetFeatureValue(CommonUsages.triggerButton, out bool building);
            print(building);
            if (building)
            {
                if (CanBuild)
                {
                    if (oneClick)
                    {
                        Vector2Int rotationOffset = currentPlacedObjectTypeSO.GetRotationOffset(currentPlacedObjectTypeSODir);
                        Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                        PlacedObject_Done placedObject = PlacedObject_Done.Create(placedObjectWorldPosition, placedObjectOrigin, currentPlacedObjectTypeSODir, currentPlacedObjectTypeSO);
                        foreach (Vector2Int gridPosition in gridPositionList)
                        {
                            grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObject(placedObject);
                        }

                        DeselectObjectType();
                        oneClick = false;
                    }
                    else
                    {
                        oneClick = true;
                    }
                }
                else
                {
                    UtilsClass.CreateWorldTextPopup("지을 수 없습니다!", mousePosition);
                }
            }
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
        //    if (grid.GetGridObject(mousePosition) != null)
        //    {
        //        // Valid Grid Position
        //        PlacedObject_Done placedObject = grid.GetGridObject(mousePosition).GetPlacedObject();
        //        if (placedObject != null)
        //        {
        //            // Demolish
        //            placedObject.DestroySelf();

        //            List<Vector2Int> gridPositionList = placedObject.GetGridPositionList();
        //            foreach (Vector2Int gridPosition in gridPositionList)
        //            {
        //                grid.GetGridObject(gridPosition.x, gridPosition.y).ClearPlacedObject();
        //            }
        //        }
        //    }
        //}
    }


    private void DeselectObjectType()
    {
        currentPlacedObjectTypeSO = null; RefreshSelectedObjectType();
    }

    private void RefreshSelectedObjectType()
    {
        visualBuilding.RefreshVisual();
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        grid.GetXZ(worldPosition, out int x, out int z);
        return new Vector2Int(x, z);
    }

    public void SetDefaultGridPosition(List<Vector2Int> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            grid.GetGridObject(points[i].x, points[i].y).SetDefaultObject();
        }
    }

    public Vector3 GetMouseWorldSnappedPosition()
    {
        Vector3 mousePosition = Player.instance.SetCurrentRayPos();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (currentPlacedObjectTypeSO != null)
        {
            Vector2Int rotationOffset = currentPlacedObjectTypeSO.GetRotationOffset(currentPlacedObjectTypeSODir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z) + new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();
            return placedObjectWorldPosition;
        }
        else
        {
            return mousePosition;
        }
    }

    public Quaternion GetPlacedObjectRotation()
    {
        if (currentPlacedObjectTypeSO != null)
        {
            return Quaternion.Euler(0, currentPlacedObjectTypeSO.GetRotationAngle(currentPlacedObjectTypeSODir), 0);
        }
        else
        {
            return Quaternion.identity;
        }
    }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO()
    {
        return currentPlacedObjectTypeSO;
    }

    public void SetPlacedObjectTypeSO(PlacedObjectTypeSO place)
    {
        currentPlacedObjectTypeSO = place; RefreshSelectedObjectType();
    }

}
