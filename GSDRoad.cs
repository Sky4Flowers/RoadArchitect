#region "Imports"
using UnityEngine;
using System.Collections.Generic;
using GSD.Roads;
using GSD;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion


[ExecuteInEditMode]
public class GSDRoad : MonoBehaviour
{
    public GameObject MainMeshes;
    public GameObject MeshRoad;
    public GameObject MeshShoR;
    public GameObject MeshShoL;
    public GameObject MeshiLanes;
    public GameObject MeshiLanes0;
    public GameObject MeshiLanes1;
    public GameObject MeshiLanes2;
    public GameObject MeshiLanes3;
    public GameObject MeshiMainPlates;
    public GameObject MeshiMarkerPlates;

    [System.NonSerialized]
    public string editorTitleString = "";

    [UnityEngine.Serialization.FormerlySerializedAs("GSDSpline")]
    public GSDSplineC spline;

    public int MostRecentNodeCount = -1;
    //private bool bMostRecentCheck = false;
    public GameObject GSDSplineObj;
    public GSDRoadSystem GSDRS;
    public GSDSplineC[] PiggyBacks = null;
    [UnityEngine.Serialization.FormerlySerializedAs("bEditorProgressBar")]
    public bool isEditorProgressBar = false;
    //Unique ID
    public string UID;
#if UNITY_EDITOR
    [SerializeField]
    public List<GSDTerrainHistoryMaker> TerrainHistory;
    public string TerrainHistoryByteSize = "";
#endif
    [System.NonSerialized]
    public bool isUpdatingSpline = false;

    //Road editor options:
    [UnityEngine.Serialization.FormerlySerializedAs("opt_LaneWidth")]
    public float laneWidth = 5f;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bShouldersEnabled")]
    public bool isShouldersEnabled = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_ShoulderWidth")]
    public float shoulderWidth = 3f;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_Lanes")]
    public int laneAmount = 2;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_RoadDefinition")]
    public float roadDefinition = 5f;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_RoadCornerDefinition")]
    public bool roadCornerDefinition = false;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bRoadCuts")]
    public bool isRoadCutsEnabled = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bShoulderCuts")]
    public bool isShoulderCutsEnabled = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bDynamicCuts")]
    public bool isDynamicCutsEnabled = false;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bMaxGradeEnabled")]
    public bool isMaxGradeEnabled = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_MaxGrade")]
    public float maxGrade = 0.08f;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_UseDefaultMaterials")]
    public bool isUsingDefaultMaterials = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_AutoUpdateInEditor")]
    public bool isAutoUpdatingInEditor = true;

    [UnityEngine.Serialization.FormerlySerializedAs("opt_TerrainSubtract_Match")]
    public float matchTerrainSubtraction = 0.1f;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bGSDRoadRaise")]
    public bool isRaisingRoad = false;

    [UnityEngine.Serialization.FormerlySerializedAs("opt_MatchHeightsDistance")]
    public float matchHeightsDistance = 50f;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_ClearDetailsDistance")]
    public float clearDetailsDistance = 30f;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_ClearDetailsDistanceHeight")]
    public float clearDetailsDistanceHeight = 5f;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_ClearTreesDistance")]
    public float clearTreesDistance = 30f;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_ClearTreesDistanceHeight")]
    public float clearTreesDistanceHeight = 50f;

    [UnityEngine.Serialization.FormerlySerializedAs("opt_HeightModEnabled")]
    public bool isHeightModificationEnabled = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_DetailModEnabled")]
    public bool isDetailModificationEnabled = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_TreeModEnabled")]
    public bool isTreeModificationEnabled = true;

    [UnityEngine.Serialization.FormerlySerializedAs("opt_SaveTerrainHistoryOnDisk")]
    public bool isSavingTerrainHistoryOnDisk = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_MagnitudeThreshold")]
    public float magnitudeThreshold = 300f;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_GizmosEnabled")]
    public bool isGizmosEnabled = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bMultithreading")]
    public bool isUsingMultithreading = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bSaveMeshes")]
    public bool isSavingMeshes = false;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bUseMeshColliders")]
    public bool isUsingMeshColliders = true;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bIsStatic")]
    public bool isStatic = false;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_bIsLightmapped")]
    public bool isLightmapped = false;
    [UnityEngine.Serialization.FormerlySerializedAs("opt_desiredRampHeight")]
    public float desiredRampHeight = 0.35f;


    public enum RoadMaterialDropdownEnum
    {
        Asphalt,
        Dirt,
        Brick,
        Cobblestone
    };

    [UnityEngine.Serialization.FormerlySerializedAs("opt_tRoadMaterialDropdown")]
    public RoadMaterialDropdownEnum roadMaterialDropdown = RoadMaterialDropdownEnum.Asphalt;
    public RoadMaterialDropdownEnum tRoadMaterialDropdownOLD = RoadMaterialDropdownEnum.Asphalt;


    public Material RoadMaterial1;
    public Material RoadMaterial2;
    public Material RoadMaterial3;
    public Material RoadMaterial4;
    public Material RoadMaterialMarker1;
    public Material RoadMaterialMarker2;
    public Material RoadMaterialMarker3;
    public Material RoadMaterialMarker4;
    public Material ShoulderMaterial1;
    public Material ShoulderMaterial2;
    public Material ShoulderMaterial3;
    public Material ShoulderMaterial4;
    public Material ShoulderMaterialMarker1;
    public Material ShoulderMaterialMarker2;
    public Material ShoulderMaterialMarker3;
    public Material ShoulderMaterialMarker4;

    public PhysicMaterial RoadPhysicMaterial;
    public PhysicMaterial ShoulderPhysicMaterial;
#if UNITY_EDITOR
    #region "Road Construction"
    [System.NonSerialized]
    public GSD.Threaded.TerrainCalcs TerrainCalcsJob;
    [System.NonSerialized]
    public GSD.Threaded.RoadCalcs1 RoadCalcsJob1;
    [System.NonSerialized]
    public GSD.Threaded.RoadCalcs2 RoadCalcsJob2;
    [System.NonSerialized]
    public RoadConstructorBufferMaker RCS;

    [UnityEngine.Serialization.FormerlySerializedAs("tName")]
    public string roadName = "";
    [UnityEngine.Serialization.FormerlySerializedAs("bProfiling")]
    public bool isProfiling = false;
    [UnityEngine.Serialization.FormerlySerializedAs("bSkipStore")]
    public bool isSkippingStore = true;
    [System.NonSerialized]
    public float EditorConstructionStartTime = 0f;


    private void CleanRunTime()
    {
        //Make sure unused items are not using memory space in runtime:
        TerrainHistory = null;
        RCS = null;
    }


    [UnityEngine.Serialization.FormerlySerializedAs("bEditorError")]
    public bool isEditorError = false;
    [UnityEngine.Serialization.FormerlySerializedAs("tError")]
    public System.Exception exceptionError = null;


    private void OnEnable()
    {
        if (!Application.isEditor)
        {
            return;
        }
        //if(Application.isEditor && !UnityEditor.EditorApplication.isPlaying){
        isEditorConstructing = false;
        UnityEditor.EditorApplication.update += delegate
        {
            EditorUpdate();
        };
#if UNITY_2018_1_OR_NEWER
        UnityEditor.EditorApplication.hierarchyChanged += delegate
        {
            hWindowChanged();
        };
#else
        UnityEditor.EditorApplication.hierarchyWindowChanged += delegate { hWindowChanged(); };
#endif
        //}
        if (spline == null || spline.nodes == null)
        {
            MostRecentNodeCount = 0;
        }
        else
        {
            MostRecentNodeCount = spline.GetNodeCount();
        }
        tRoadMaterialDropdownOLD = roadMaterialDropdown;
        CheckMats();
    }


    public void Awake()
    {
        if (spline == null || spline.nodes == null)
        {
            MostRecentNodeCount = 0;
        }
        else
        {
            MostRecentNodeCount = spline.GetNodeCount();
        }
    }


    [UnityEngine.Serialization.FormerlySerializedAs("EditorTimer")]
    private int editorTimer = 0;
    [UnityEngine.Serialization.FormerlySerializedAs("EditorTimerMax")]
    private int editorTimerMax = 0;
    [UnityEngine.Serialization.FormerlySerializedAs("EditorTimerSpline")]
    private int editorTimerSpline = 0;
    [UnityEngine.Serialization.FormerlySerializedAs("EditorTimerSplineMax")]
    private const int editorTimerSplineMax = 2;
    [System.NonSerialized]
    public int editorProgress = 0;
    [UnityEngine.Serialization.FormerlySerializedAs("GizmoNodeTimerMax")]
    private const int gizmoNodeTimerMax = 2;
    [UnityEngine.Serialization.FormerlySerializedAs("EditorUpdateMe")]
    public bool isUpdateRequired = false;
    [UnityEngine.Serialization.FormerlySerializedAs("bTriggerGC")]
    public bool isTriggeringGC = false;
    [UnityEngine.Serialization.FormerlySerializedAs("bTriggerGC_Happening")]
    private bool isTriggeredGCExecuting;
    [UnityEngine.Serialization.FormerlySerializedAs("TriggerGC_End")]
    private float triggerGCEnd = 0f;


    private void EditorUpdate()
    {
        if (!Application.isEditor)
        {
            UnityEditor.EditorApplication.update -= delegate
            {
                EditorUpdate();
            };
        }

        if (this == null)
        {
            UnityEditor.EditorApplication.update -= delegate
            {
                EditorUpdate();
            };
            isEditorConstructing = false;
            EditorUtility.ClearProgressBar();
            return;
        }

        //Custom garbage collection demands for editor:
        if (isTriggeringGC)
        {
            isTriggeringGC = false;
            triggerGCEnd = Time.realtimeSinceStartup + 1f;
            isTriggeredGCExecuting = true;
        }
        if (isTriggeredGCExecuting)
        {
            if (Time.realtimeSinceStartup > triggerGCEnd)
            {
                isTriggeredGCExecuting = false;
                GSDRootUtil.ForceCollection();
                triggerGCEnd = 200000f;
            }
        }

        if (isEditorConstructing)
        { // && !Application.isPlaying && !UnityEditor.EditorApplication.isPlaying){
            if (GSDRS != null)
            {
                if (GSDRS.isMultithreaded)
                {
                    editorTimer += 1;
                    if (editorTimer > editorTimerMax)
                    {
                        if ((Time.realtimeSinceStartup - EditorConstructionStartTime) > 180f)
                        {
                            isEditorConstructing = false;
                            EditorUtility.ClearProgressBar();
                            Debug.Log("Update shouldn't take longer than 180 seconds. Aborting update.");
                        }

                        editorTimer = 0;
                        if (isEditorError)
                        {
                            isEditorConstructing = false;
                            EditorUtility.ClearProgressBar();
                            isEditorError = false;
                            if (exceptionError != null)
                            {
                                Debug.LogError(exceptionError.StackTrace);
                                throw exceptionError;
                            }
                        }

                        if (TerrainCalcsJob != null && TerrainCalcsJob.Update())
                        {
                            ConstructRoad2();
                        }
                        else if (RoadCalcsJob1 != null && RoadCalcsJob1.Update())
                        {
                            ConstructRoad3();
                        }
                        else if (RoadCalcsJob2 != null && RoadCalcsJob2.Update())
                        {
                            ConstructRoad4();
                        }
                    }
                }
            }
        }
        else
        {
            if (isUpdateRequired && !isEditorConstructing)
            {
                isUpdateRequired = false;
                spline.TriggerSetup();
            }
        }

        if (isEditorConstructing)
        {
            RoadUpdateProgressBar();
        }
        else if (isEditorProgressBar)
        {
            RoadUpdateProgressBar();
        }

        if (!Application.isPlaying && isUpdatingSpline && !UnityEditor.EditorApplication.isPlaying)
        {
            editorTimerSpline += 1;
            if (editorTimerSpline > editorTimerSplineMax)
            {
                editorTimerSpline = 0;
                isUpdatingSpline = false;
                spline.TriggerSetup();
                MostRecentNodeCount = spline.nodes.Count;
            }
        }

        if (isEditorCameraMoving && EditorCameraNextMove < EditorApplication.timeSinceStartup)
        {
            EditorCameraNextMove = (float)EditorApplication.timeSinceStartup + EditorCameraTimeUpdateInterval;
            DoEditorCameraLoop();
        }
    }


    [System.NonSerialized]
    [UnityEngine.Serialization.FormerlySerializedAs("bEditorCameraMoving")]
    public bool isEditorCameraMoving = false;
    [System.NonSerialized]
    public float EditorCameraPos = 0f;
    //float EditorCameraPos_Full = 0f;
    private const float EditorCameraTimeUpdateInterval = 0.015f;
    private float EditorCameraNextMove = 0f;
    [UnityEngine.Serialization.FormerlySerializedAs("bEditorCameraSetup")]
    private bool isEditorCameraSetup = false;
    private float EditorCameraStartPos = 0f;
    private float EditorCameraEndPos = 1f;
    private float EditorCameraIncrementDistance = 0f;
    private float EditorCameraIncrementDistance_Full = 0f;
    public float EditorCameraMetersPerSecond = 60f;
    [UnityEngine.Serialization.FormerlySerializedAs("bEditorCameraRotate")]
    public bool isEditorCameraRotated = false;
    private Vector3 EditorCameraV1 = default(Vector3);
    private Vector3 EditorCameraV2 = default(Vector3);
    [System.NonSerialized]
    public Vector3 editorCameraOffset = new Vector3(0f, 5f, 0f);
    [System.NonSerialized]
    public Camera editorPlayCamera = null;
    [UnityEngine.Serialization.FormerlySerializedAs("EditorCameraBadVec")]
    private Vector3 editorCameraBadVec = default(Vector3);


    public void DoEditorCameraLoop()
    {
        if (!isEditorCameraSetup)
        {
            isEditorCameraSetup = true;
            if (spline.isSpecialEndControlNode)
            {   //If control node, start after the control node:
                EditorCameraEndPos = spline.nodes[spline.GetNodeCount() - 2].time;
            }
            if (spline.isSpecialStartControlNode)
            {   //If ends in control node, end construction before the control node:
                EditorCameraStartPos = spline.nodes[1].time;
            }
            //			EditorCameraPos_Full = 0f;
            ChangeEditorCameraMetersPerSec();
        }

        if (!Selection.Contains(this.transform.gameObject))
        {
            QuitEditorCamera();
            return;
        }

        //EditorCameraPos_Full+=EditorCameraIncrementDistance_Full;
        //if(EditorCameraPos_Full > GSDSpline.distance){ EditorCameraPos = EditorCameraStartPos; bEditorCameraMoving = false; bEditorCameraSetup = false; EditorCameraPos_Full = 0f; return; }
        //EditorCameraPos = GSDSpline.TranslateDistBasedToParam(EditorCameraPos_Full);

        EditorCameraPos += EditorCameraIncrementDistance;
        if (EditorCameraPos > EditorCameraEndPos)
        {
            QuitEditorCamera();
            return;
        }
        if (EditorCameraPos < EditorCameraStartPos)
        {
            EditorCameraPos = EditorCameraStartPos;
        }

        spline.GetSplineValueBoth(EditorCameraPos, out EditorCameraV1, out EditorCameraV2);

        if (EditorApplication.isPlaying)
        {
            if (editorPlayCamera != null)
            {
                editorPlayCamera.transform.position = EditorCameraV1;
                if (isEditorCameraRotated)
                {
                    editorPlayCamera.transform.position += editorCameraOffset;
                    if (EditorCameraV2 != editorCameraBadVec)
                    {
                        editorPlayCamera.transform.rotation = Quaternion.LookRotation(EditorCameraV2);
                    }
                }
            }
        }
        else
        {
            SceneView.lastActiveSceneView.pivot = EditorCameraV1;
            if (isEditorCameraRotated)
            {
                SceneView.lastActiveSceneView.pivot += editorCameraOffset;
                if (EditorCameraV2 != editorCameraBadVec)
                {
                    SceneView.lastActiveSceneView.rotation = Quaternion.LookRotation(EditorCameraV2);
                }
            }
            SceneView.lastActiveSceneView.Repaint();
        }
    }


    public void EditorCameraSetSingle()
    {
        if (editorPlayCamera == null)
        {
            Camera[] editorCameras = (Camera[])GameObject.FindObjectsOfType<Camera>();
            if (editorCameras != null && editorCameras.Length == 1)
            {
                editorPlayCamera = editorCameras[0];
            }
        }
    }


    public void QuitEditorCamera()
    {
        EditorCameraPos = EditorCameraStartPos;
        isEditorCameraMoving = false;
        isEditorCameraSetup = false;
        //EditorCameraPos_Full = 0f;
    }


    public void ChangeEditorCameraMetersPerSec()
    {
        EditorCameraIncrementDistance_Full = (EditorCameraMetersPerSecond / 60);
        EditorCameraIncrementDistance = (EditorCameraIncrementDistance_Full / spline.distance);
    }


    private void hWindowChanged()
    {
        if (!Application.isEditor)
        {
#if UNITY_2018_1_OR_NEWER
            UnityEditor.EditorApplication.hierarchyChanged -= delegate { hWindowChanged(); };
#else
            UnityEditor.EditorApplication.hierarchyWindowChanged -= delegate { hWindowChanged(); };
#endif
        }
        if (Application.isPlaying || !Application.isEditor)
        {
            return;
        }
        if (Application.isEditor && UnityEditor.EditorApplication.isPlaying)
        {
            return;
        }
        if (Application.isEditor && UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }

        int count = 0;
        if (spline != null && spline.nodes != null)
        {
            count = spline.GetNodeCountNonNull();
        }
        if (count != MostRecentNodeCount)
        {
            isUpdatingSpline = true;
        }
    }


    private void RoadUpdateProgressBar()
    {
        if (isEditorConstructing)
        {
            EditorUtility.DisplayProgressBar(
                "GSD Road Update",
                editorTitleString,
                ((float)editorProgress / 100f));
        }
        else if (isEditorProgressBar)
        {
            isEditorProgressBar = false;
            EditorUtility.ClearProgressBar();
        }
    }


    public void UpdateRoad(RoadUpdateTypeEnum _updateType = RoadUpdateTypeEnum.Full)
    {
        if (!GSDRS.isAllowingRoadUpdates)
        {
            spline.Setup();
            isEditorConstructing = false;
            return;
        }

        if (isEditorConstructing)
        {
            return;
        }

        SetupUniqueIdentifier();


        GSDRootUtil.StartProfiling(this, "UpdateRoadPrelim");
        roadDefinition = Mathf.Clamp(roadDefinition, 1f, 50f);
        laneWidth = Mathf.Clamp(laneWidth, 0.2f, 500f);
        EditorConstructionStartTime = Time.realtimeSinceStartup;
        editorTitleString = "Updating " + transform.name + "...";
        System.GC.Collect();

        if (isSavingTerrainHistoryOnDisk)
        {
            ConstructRoad_LoadTerrainHistory();
        }

        CheckMats();

        EditorUtility.ClearProgressBar();

        isProfiling = true;
        if (GSDRS.isMultithreaded)
        {
            isProfiling = false;
        }

        //Set all terrains to height 0:
        GSD.Roads.GSDTerraforming.CheckAllTerrainsHeight0();

        editorProgress = 20;
        isEditorProgressBar = true;
        if (isEditorConstructing)
        {
            if (TerrainCalcsJob != null)
            {
                TerrainCalcsJob.Abort();
                TerrainCalcsJob = null;
            }
            if (RoadCalcsJob1 != null)
            {
                RoadCalcsJob1.Abort();
                RoadCalcsJob1 = null;
            }
            if (RoadCalcsJob2 != null)
            {
                RoadCalcsJob2.Abort();
                RoadCalcsJob2 = null;
            }
            isEditorConstructing = false;
        }

        //		if(Application.isPlaying || !Application.isEditor){ return; }
        //		if(Application.isEditor && UnityEditor.EditorApplication.isPlaying){ return; }
        //		if(Application.isEditor && UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode){ return; }

        //In here for intersection patching purposes:
        int nodeCount = spline.GetNodeCount();
        GSDSplineN node = null;
        GSDSplineN node1 = null;
        GSDSplineN node2 = null;
        if (spline.CheckInvalidNodeCount())
        {
            spline.Setup();
            nodeCount = spline.GetNodeCount();
        }
        if (nodeCount > 1)
        {
            for (int i = 0; i < nodeCount; i++)
            {
                //try
                //{
                node = spline.nodes[i];
                //}
                //catch
                //{
                //  Editor_bIsConstructing = false;
                //	EditorUpdateMe = true;
                //	return;	
                //}

                //If node is intersection with an invalid GSDRI, mark it at non-intersection. Just-in-case.
                if (node.isIntersection && node.intersection == null)
                {
                    node.isIntersection = false;
                    node.intersectionOtherNodeID = -1;
                    node.intersectionOtherNode = null;
                }
                //If node is intersection, re-setup:
                if (node.isIntersection && node.intersection != null)
                {
                    node1 = node.intersection.node1;
                    node2 = node.intersection.node2;
                    node.intersection.Setup(node1, node2);
                    node.intersection.DeleteRelevantChildren(node, node.spline.road.transform.name);
                    //If primary node on intersection, do more re-setup:
                    if (node.intersection.node1 == node)
                    {
                        node.intersection.lanesAmount = laneAmount;
                        node.intersection.name = node.intersection.transform.name;
                    }
                    //Setup construction objects:
                    node.intersection.node1.intersectionConstruction = new GSD.Roads.GSDIntersections.iConstructionMaker();
                    node.intersection.node2.intersectionConstruction = new GSD.Roads.GSDIntersections.iConstructionMaker();
                }

                //Store materials and physical materials for road and or shoulder cuts on each node, if necessary:
                node.StoreCuts();
            }
        }
        name = transform.name;


        spline.RoadWidth = RoadWidth();
        //GSDRootUtil.StartProfiling(this, "SplineSetup");
        spline.Setup();
        //GSDRootUtil.EndProfiling(this);
        nodeCount = spline.GetNodeCount();

        if (spline == null || spline.nodes == null)
        {
            MostRecentNodeCount = 0;
        }
        else
        {
            MostRecentNodeCount = spline.GetNodeCount();
        }  


        if (isUsingDefaultMaterials)
        {
            SetDefaultMats();

            if (DetectInvalidDefaultMatsForUndo())
            {
                SetAllCutsToCurrentMaterials();
            }
        }
        
        //Hiding in hierarchy:
        for (int i = 0; i < nodeCount; i++)
        {
            node = spline.nodes[i];
            if (node != null)
            {
                if (node.isIntersection || node.isSpecialEndNode)
                {
                    node.ToggleHideFlags(true);
                }
                else
                {
                    node.ToggleHideFlags(false);
                }
            }
        }
        
        int childCount = transform.childCount;
        GameObject mainMeshes = null;
        List<GameObject> objects = new List<GameObject>();
        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).transform.name.ToLower().Contains("mainmeshes"))
            {
                mainMeshes = transform.GetChild(i).transform.gameObject;
                objects.Add(mainMeshes);
            }
        }
        for (int i = (objects.Count - 1); i >= 0; i--)
        {
            mainMeshes = objects[i];
            Object.DestroyImmediate(mainMeshes);
        }

        if (nodeCount < 2)
        {
            //Delete old objs and return:
            if (MainMeshes != null)
            {
                Object.DestroyImmediate(MainMeshes);
            }
            if (MeshRoad != null)
            {
                Object.DestroyImmediate(MeshRoad);
            }
            if (MeshShoR != null)
            {
                Object.DestroyImmediate(MeshShoR);
            }
            if (MeshShoL != null)
            {
                Object.DestroyImmediate(MeshShoL);
            }
            if (MeshiLanes != null)
            {
                Object.DestroyImmediate(MeshiLanes);
            }
            if (MeshiLanes0 != null)
            {
                Object.DestroyImmediate(MeshiLanes0);
            }
            if (MeshiLanes1 != null)
            {
                Object.DestroyImmediate(MeshiLanes1);
            }
            if (MeshiLanes2 != null)
            {
                Object.DestroyImmediate(MeshiLanes2);
            }
            if (MeshiLanes3 != null)
            {
                Object.DestroyImmediate(MeshiLanes3);
            }
            if (MeshiMainPlates != null)
            {
                Object.DestroyImmediate(MeshiMainPlates);
            }
            if (MeshiMarkerPlates != null)
            {
                Object.DestroyImmediate(MeshiMarkerPlates);
            }
            GSDRootUtil.EndProfiling(this);
            return;
        }
        
        spline.HeightHistory = new List<KeyValuePair<float, float>>();
        if (GSDRS == null)
        {
            GSDRS = transform.parent.GetComponent<GSDRoadSystem>();
        } //Compatibility update.

        if (GSDRS.isMultithreaded)
        {
            isEditorConstructing = true;
        }
        else
        {
            isEditorConstructing = false;
        }
        editorConstructionID = 0;


        //Check if road takes place on only 1 terrain:
        Terrain terrain = GSD.Roads.GSDRoadUtil.GetTerrain(spline.nodes[0].pos);
        bool isSameTerrain = true;
        for (int i = 1; i < nodeCount; i++)
        {
            if (terrain != GSD.Roads.GSDRoadUtil.GetTerrain(spline.nodes[0].pos))
            {
                isSameTerrain = false;
                break;
            }
        }

        RCS = new RoadConstructorBufferMaker(this, _updateType);

        if (isSameTerrain)
        {
            RCS.tTerrain = terrain;
        }
        else
        {
            RCS.tTerrain = null;
        }
        terrain = null;
        
        GSDRootUtil.EndProfiling(this);
        if (GSDRS.isMultithreaded)
        {
            if (RCS.isTerrainOn || TerrainHistory == null)
            {
                GSDTerraforming.ProcessRoadTerrainHook1(spline, this);
            }
            else
            {
                ConstructRoad2();
            }
        }
        else
        {
            UpdateRoad_NoMultiThreading();
        }
    }


    #region "Terrain history"
    public void ConstructRoad_StoreTerrainHistory(bool _isDiskOnly = false)
    {
        if (!_isDiskOnly)
        {
            GSDRoad road = this;
            GSDRoadUtil.ConstructRoadStoreTerrainHistory(ref road);
        }

        if (isSavingTerrainHistoryOnDisk && TerrainHistory != null && TerrainHistory.Count > 0)
        {
            GSDRootUtil.StartProfiling(this, "TerrainHistory_Save");
            GSDGeneralEditor.TerrainHistorySave(TerrainHistory, this);
            GSDRootUtil.EndProfiling(this);
            TerrainHistory.Clear();
            TerrainHistory = null;
        }
        else
        {
            if (TerrainHistory != null && TerrainHistory.Count > 0)
            {
                int terrainSize = 0;
                for (int i = 0; i < TerrainHistory.Count; i++)
                {
                    terrainSize += TerrainHistory[i].GetSize();
                }
                TerrainHistoryByteSize = (terrainSize * 0.001f).ToString("n0") + " kb";
            }
            else
            {
                TerrainHistoryByteSize = "0 bytes";
            }
        }
    }


    public void ConstructRoad_ResetTerrainHistory()
    {
        GSDRoad tRoad = this;
        if (isSavingTerrainHistoryOnDisk && TerrainHistory != null)
        {
            GSDGeneralEditor.TerrainHistoryDelete(this);
        }
        else
        {
            GSDRoadUtil.ConstructRoadResetTerrainHistory(ref tRoad);
        }
    }


    public void ConstructRoad_LoadTerrainHistory(bool _isForced = false)
    {
        if (isSavingTerrainHistoryOnDisk || _isForced)
        {
            if (TerrainHistory != null)
            {
                TerrainHistory.Clear();
                TerrainHistory = null;
            }
            TerrainHistory = GSDGeneralEditor.TerrainHistoryLoad(this);
        }
        if (_isForced)
        {
            GSDGeneralEditor.TerrainHistoryDelete(this);
        }
    }
    #endregion


    #region "Construction process"
    #region "No multithread"
    private void UpdateRoad_NoMultiThreading()
    {
        if (isHeightModificationEnabled || isDetailModificationEnabled || isTreeModificationEnabled)
        {
            GSDRootUtil.StartProfiling(this, "RoadCon_Terrain");
            if (RCS.isTerrainOn || TerrainHistory == null)
            {
                GSDTerraforming.ProcessRoadTerrainHook1(spline, this, false);
                GSDTerraforming.ProcessRoadTerrainHook2(spline, ref EditorTTDList);
                ConstructRoad_StoreTerrainHistory();//Store history.
                int editorTTDListCount = EditorTTDList.Count;
                for (int i = 0; i < editorTTDListCount; i++)
                {
                    EditorTTDList[i] = null;
                }
                EditorTTDList = null;
                System.GC.Collect();
            }
            GSDRootUtil.EndProfiling(this);
        }

        editorProgress = 50;
        GSDRoad road = this;
        GSDRootUtil.StartProfiling(this, "RoadCon_RoadPrelim");

        editorProgress = 80;
        GSD.Threaded.GSDRoadCreationT.RoadJobPrelim(ref road);
        GSDRootUtil.EndStartProfiling(this, "RoadCon_Road1");
        editorProgress = 90;
        GSD.Threaded.RoadCalcs1Static.RunMe(ref RCS);
        GSDRootUtil.EndStartProfiling(this, "MeshSetup1");
        editorProgress = 92;
        RCS.MeshSetup1();
        GSDRootUtil.EndStartProfiling(this, "RoadCon_Road2");
        editorProgress = 94;
        GSD.Threaded.RoadCalcs2Static.RunMe(ref RCS);
        GSDRootUtil.EndStartProfiling(this, "MeshSetup2");
        editorProgress = 96;
        RCS.MeshSetup2();
        GSDRootUtil.EndProfiling(this);
        Construction_Cleanup();
    }
    #endregion


    private void ConstructRoad2()
    {
        editorProgress = 40;
        if (RCS.isTerrainOn)
        {
            //Store history:
            GSDTerraforming.ProcessRoadTerrainHook2(spline, ref EditorTTDList);
            ConstructRoad_StoreTerrainHistory();
            int editorTTDListCount = EditorTTDList.Count;
            for (int i = 0; i < editorTTDListCount; i++)
            {
                EditorTTDList[i] = null;
            }
            EditorTTDList = null;
            System.GC.Collect();
        }
        editorProgress = 60;

        if (TerrainCalcsJob != null)
        {
            TerrainCalcsJob.Abort();
            TerrainCalcsJob = null;
        }
        GSDRoad road = this;
        editorProgress = 72;
        RoadCalcsJob1 = new GSD.Threaded.RoadCalcs1();
        RoadCalcsJob1.Setup(ref RCS, ref road);
        RoadCalcsJob1.Start();
    }


    private void ConstructRoad3()
    {
        editorProgress = 84;
        RCS.MeshSetup1();
        editorProgress = 96;
        if (RoadCalcsJob1 != null)
        {
            RoadCalcsJob1.Abort();
            RoadCalcsJob1 = null;
        }
        RoadCalcsJob2 = new GSD.Threaded.RoadCalcs2();
        RoadCalcsJob2.Setup(ref RCS);
        RoadCalcsJob2.Start();
        editorProgress = 98;
    }


    private void ConstructRoad4()
    {
        RCS.MeshSetup2();
        Construction_Cleanup();
    }
    #endregion


    private void Construction_Cleanup()
    {
        FixZ();

        if (TerrainCalcsJob != null)
        {
            TerrainCalcsJob.Abort();
            TerrainCalcsJob = null;
        }
        if (RoadCalcsJob1 != null)
        {
            RoadCalcsJob1.Abort();
            RoadCalcsJob1 = null;
        }
        if (RoadCalcsJob2 != null)
        {
            RoadCalcsJob2.Abort();
            RoadCalcsJob2 = null;
        }
        isEditorConstructing = false;
        int nodeCount = spline.GetNodeCount();
        GSDSplineN node;
        for (int i = 0; i < nodeCount; i++)
        {
            node = spline.nodes[i];
            if (node.isIntersection)
            {
                if (node.intersectionConstruction != null)
                {
                    node.intersectionConstruction.Nullify();
                    node.intersectionConstruction = null;
                }
            }
            node.SetupSplinationLimits();
            node.SetupEdgeObjects(false);
            node.SetupSplinatedMeshes(false);
        }
        if (spline.HeightHistory != null)
        {
            spline.HeightHistory.Clear();
            spline.HeightHistory = null;
        }
        if (RCS != null)
        {
            RCS.Nullify();
            RCS = null;
        }

        if (GSDRS.isSavingMeshes)
        {
            UnityEditor.AssetDatabase.SaveAssets();
        }
        isEditorProgressBar = false;
        EditorUtility.ClearProgressBar();
        //Make sure terrain history out of memory if necessary (redudant but keep):
        if (isSavingTerrainHistoryOnDisk && TerrainHistory != null)
        {
            TerrainHistory.Clear();
            TerrainHistory = null;
        }

        //Collect:
        isTriggeringGC = true;

        if (tRoadMaterialDropdownOLD != roadMaterialDropdown)
        {
            tRoadMaterialDropdownOLD = roadMaterialDropdown;
            SetAllCutsToCurrentMaterials();
        }

        if (PiggyBacks != null && PiggyBacks.Length > 0)
        {
            for (int i = 0; i < PiggyBacks.Length; i++)
            {
                if (PiggyBacks[i] == null)
                {
                    PiggyBacks = null;
                    break;
                }
            }

            if (PiggyBacks != null)
            {
                GSDSplineC tPiggy = PiggyBacks[0];
                GSDSplineC[] NewPiggys = null;

                PiggyBacks[0] = null;
                if (PiggyBacks.Length > 1)
                {
                    NewPiggys = new GSDSplineC[PiggyBacks.Length - 1];
                    for (int i = 1; i < PiggyBacks.Length; i++)
                    {
                        NewPiggys[i - 1] = PiggyBacks[i];
                    }
                }

                if (NewPiggys != null)
                {
                    tPiggy.road.PiggyBacks = NewPiggys;
                }
                NewPiggys = null;
                tPiggy.TriggerSetup();
            }
        }
    }


    public List<GSDTerraforming.TempTerrainData> EditorTTDList;


    public void EditorTerrainCalcs(ref List<GSDTerraforming.TempTerrainData> _tddList)
    {
        EditorTTDList = _tddList;
    }
    #endregion


    #region "Gizmos"
    [UnityEngine.Serialization.FormerlySerializedAs("Editor_bIsConstructing")]
    public bool isEditorConstructing = false;
    [UnityEngine.Serialization.FormerlySerializedAs("Editor_bConstructionID")]
    public int editorConstructionID = 0;
    [UnityEngine.Serialization.FormerlySerializedAs("Editor_bSelected")]
    public bool isEditorSelected = false;
    [UnityEngine.Serialization.FormerlySerializedAs("Editor_MouseTerrainHit")]
    public bool isEditorMouseHittingTerrain = false;
    [UnityEngine.Serialization.FormerlySerializedAs("Editor_MousePos")]
    public Vector3 editorMousePos = new Vector3(0f, 0f, 0f);
    [UnityEngine.Serialization.FormerlySerializedAs("Color_NodeDefaultColor")]
    public Color defaultNodeColor = new Color(0f, 1f, 1f, 0.75f);
    public readonly Color Color_NodeConnColor = new Color(0f, 1f, 0f, 0.75f);
    public readonly Color Color_NodeInter = new Color(0f, 1f, 0f, 0.75f);
    public Color selectedColor = Color.yellow;
    public Color newNodePreviewColor = Color.red;


    private void OnDrawGizmosSelected()
    {
        if (isEditorMouseHittingTerrain)
        {
            Gizmos.color = newNodePreviewColor;
            Gizmos.DrawCube(editorMousePos, new Vector3(10f, 4f, 10f));
        }
    }
    #endregion
#endif


    public float RoadWidth()
    {
        return (laneWidth * (float)laneAmount);
    }


#if UNITY_EDITOR
    public float EditorCameraTimer = 0f;
    private float EditorTestTimer = 0f;
    [UnityEngine.Serialization.FormerlySerializedAs("bEditorTestTimer")]
    private bool isEditorTestTimer = true;


    private void Update()
    {
        if (Application.isEditor && isEditorCameraMoving)
        {
            EditorCameraTimer += Time.deltaTime;
            if (EditorCameraTimer > EditorCameraTimeUpdateInterval)
            {
                EditorCameraTimer = 0f;
                DoEditorCameraLoop();
            }
        }

        if (isEditorTestTimer)
        {
            if (transform.name == "Road1")
            {
                EditorTestTimer += Time.deltaTime;
                if (EditorTestTimer > 2f)
                {
                    //UpdateRoad(RoadUpdateTypeEnum.Full);
                    isEditorTestTimer = false;
                }
            }
            else
            {
                isEditorTestTimer = false;
            }
        }
    }


    #region "Default materials retrieval"
    public bool DetectInvalidDefaultMatsForUndo()
    {
        string lowerName = "";
        int counter = 0;
        if (!MeshRoad)
        {
            return false;
        }

        MeshRenderer[] MRs = MeshRoad.GetComponentsInChildren<MeshRenderer>();
        Material tMat2Lanes = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble.mat");
        Material tMat4Lanes = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble-4L.mat");
        Material tMat6Lanes = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble-6L.mat");
        foreach (MeshRenderer MR in MRs)
        {
            lowerName = MR.transform.name.ToLower();
            if (lowerName.Contains("marker"))
            {
                if (laneAmount == 2)
                {
                    if (MR.sharedMaterials[0] == tMat4Lanes)
                    {
                        counter += 1;
                    }
                    else if (MR.sharedMaterials[0] == tMat6Lanes)
                    {
                        counter += 1;
                    }
                }
                else if (laneAmount == 4)
                {
                    if (MR.sharedMaterials[0] == tMat2Lanes)
                    {
                        counter += 1;
                    }
                    else if (MR.sharedMaterials[0] == tMat6Lanes)
                    {
                        counter += 1;
                    }
                }
                else if (laneAmount == 6)
                {
                    if (MR.sharedMaterials[0] == tMat2Lanes)
                    {
                        counter += 1;
                    }
                    else if (MR.sharedMaterials[0] == tMat4Lanes)
                    {
                        counter += 1;
                    }
                }
            }
            if (counter > 1)
            {
                return true;
            }
        }
        return false;
    }


    public void SetAllCutsToCurrentMaterials()
    {
        string lowerName = "";
        if (!MeshRoad)
        {
            return;
        }

        MeshRenderer[] MRs = MeshRoad.GetComponentsInChildren<MeshRenderer>();
        Material[] roadWorldMats = GetRoadWorldMaterials();
        Material[] roadMarkerMats = GetRoadMarkerMaterials();
        foreach (MeshRenderer MR in MRs)
        {
            lowerName = MR.transform.name.ToLower();
            if (lowerName.Contains("marker"))
            {
                if (roadMarkerMats != null)
                {
                    MR.sharedMaterials = roadMarkerMats;
                }
            }
            else if (lowerName.Contains("cut"))
            {
                if (roadWorldMats != null)
                {
                    MR.sharedMaterials = roadWorldMats;
                }
            }
        }

        if (isShouldersEnabled && MeshShoL != null)
        {
            MRs = MeshShoL.GetComponentsInChildren<MeshRenderer>();
            roadWorldMats = GetShoulderWorldMaterials();
            roadMarkerMats = GetShoulderMarkerMaterials();
            foreach (MeshRenderer MR in MRs)
            {
                lowerName = MR.transform.name.ToLower();
                if (lowerName.Contains("marker"))
                {
                    if (roadMarkerMats != null)
                    {
                        MR.sharedMaterials = roadMarkerMats;
                    }
                }
                else if (lowerName.Contains("cut"))
                {
                    if (roadWorldMats != null)
                    {
                        MR.sharedMaterials = roadWorldMats;
                    }
                }
            }
        }

        if (isShouldersEnabled && MeshShoR != null)
        {
            MRs = MeshShoR.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer MR in MRs)
            {
                lowerName = MR.transform.name.ToLower();
                if (lowerName.Contains("marker"))
                {
                    if (roadMarkerMats != null)
                    {
                        MR.sharedMaterials = roadMarkerMats;
                    }
                }
                else if (lowerName.Contains("cut"))
                {
                    if (roadWorldMats != null)
                    {
                        MR.sharedMaterials = roadWorldMats;
                    }
                }
            }
        }
    }


    public Material[] GetRoadWorldMaterials()
    {
        int counter = 0;
        if (RoadMaterial1 != null)
        {
            counter += 1;
            if (RoadMaterial2 != null)
            {
                counter += 1;
                if (RoadMaterial3 != null)
                {
                    counter += 1;
                    if (RoadMaterial4 != null)
                    {
                        counter += 1;
                    }
                }
            }
        }
        if (counter > 0)
        {
            Material[] materials = new Material[counter];
            if (RoadMaterial1 != null)
            {
                materials[0] = RoadMaterial1;
                if (RoadMaterial2 != null)
                {
                    materials[1] = RoadMaterial2;
                    if (RoadMaterial3 != null)
                    {
                        materials[2] = RoadMaterial3;
                        if (RoadMaterial4 != null)
                        {
                            materials[3] = RoadMaterial4;
                        }
                    }
                }
            }
            return materials;
        }
        else
        {
            return null;
        }
    }


    public Material[] GetRoadMarkerMaterials()
    {
        int counter = 0;
        if (RoadMaterialMarker1 != null)
        {
            counter += 1;
            if (RoadMaterialMarker2 != null)
            {
                counter += 1;
                if (RoadMaterialMarker3 != null)
                {
                    counter += 1;
                    if (RoadMaterialMarker4 != null)
                    {
                        counter += 1;
                    }
                }
            }
        }
        if (counter > 0)
        {
            Material[] materials = new Material[counter];
            if (RoadMaterialMarker1 != null)
            {
                materials[0] = RoadMaterialMarker1;
                if (RoadMaterialMarker2 != null)
                {
                    materials[1] = RoadMaterialMarker2;
                    if (RoadMaterialMarker3 != null)
                    {
                        materials[2] = RoadMaterialMarker3;
                        if (RoadMaterialMarker4 != null)
                        {
                            materials[3] = RoadMaterialMarker4;
                        }
                    }
                }
            }
            return materials;
        }
        else
        {
            return null;
        }
    }


    public Material[] GetShoulderWorldMaterials()
    {
        if (!isShouldersEnabled)
        {
            return null;
        }

        int counter = 0;
        if (ShoulderMaterial1 != null)
        {
            counter += 1;
            if (ShoulderMaterial2 != null)
            {
                counter += 1;
                if (ShoulderMaterial3 != null)
                {
                    counter += 1;
                    if (ShoulderMaterial4 != null)
                    {
                        counter += 1;
                    }
                }
            }
        }
        if (counter > 0)
        {
            Material[] materials = new Material[counter];
            if (ShoulderMaterial1 != null)
            {
                materials[0] = ShoulderMaterial1;
                if (ShoulderMaterial2 != null)
                {
                    materials[1] = ShoulderMaterial2;
                    if (ShoulderMaterial3 != null)
                    {
                        materials[2] = ShoulderMaterial3;
                        if (ShoulderMaterial4 != null)
                        {
                            materials[3] = ShoulderMaterial4;
                        }
                    }
                }
            }
            return materials;
        }
        else
        {
            return null;
        }
    }


    public Material[] GetShoulderMarkerMaterials()
    {
        if (!isShouldersEnabled)
        {
            return null;
        }

        int counter = 0;
        if (ShoulderMaterialMarker1 != null)
        {
            counter += 1;
            if (ShoulderMaterialMarker2 != null)
            {
                counter += 1;
                if (ShoulderMaterialMarker3 != null)
                {
                    counter += 1;
                    if (ShoulderMaterialMarker4 != null)
                    {
                        counter += 1;
                    }
                }
            }
        }
        if (counter > 0)
        {
            Material[] materials = new Material[counter];
            if (ShoulderMaterialMarker1 != null)
            {
                materials[0] = ShoulderMaterialMarker1;
                if (ShoulderMaterialMarker2 != null)
                {
                    materials[1] = ShoulderMaterialMarker2;
                    if (ShoulderMaterialMarker3 != null)
                    {
                        materials[2] = ShoulderMaterialMarker3;
                        if (ShoulderMaterialMarker4 != null)
                        {
                            materials[3] = ShoulderMaterialMarker4;
                        }
                    }
                }
            }
            return materials;
        }
        else
        {
            return null;
        }
    }
    #endregion


    #region "Materials"
    private void CheckMats()
    {
        if (!isUsingDefaultMaterials)
        {
            return;
        }

        if (!RoadMaterial1)
        {
            RoadMaterial1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/GSDRoad1.mat");
            RoadMaterial2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDRoadDetailOverlay1.mat");
        }
        if (!RoadMaterialMarker1)
        {
            if (laneAmount == 2)
            {
                RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble.mat");
            }
            else if (laneAmount == 4)
            {
                RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble-4L.mat");
            }
            else if (laneAmount == 6)
            {
                RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble-6L.mat");
            }
            else
            {
                RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble.mat");
            }

            if (laneAmount == 2)
            {
                RoadMaterialMarker2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDTireMarks.mat");
            }
            else if (laneAmount == 4)
            {
                RoadMaterialMarker2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDTireMarks-4L.mat");
            }
            else if (laneAmount == 6)
            {
                RoadMaterialMarker2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDTireMarks-6L.mat");
            }
            else
            {
                RoadMaterialMarker2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDTireMarks.mat");
            }
        }
        if (isShouldersEnabled && !ShoulderMaterial1)
        {
            ShoulderMaterial1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/GSDShoulder1.mat");
            ShoulderMaterial2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDRoadDetailOverlay1.mat");
        }

        if (isShouldersEnabled && !RoadPhysicMaterial)
        {
            RoadPhysicMaterial = GSD.Roads.GSDRoadUtilityEditor.LoadPhysicsMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Physics/GSDPavement.physicMaterial");
        }
        if (isShouldersEnabled && !ShoulderPhysicMaterial)
        {
            ShoulderPhysicMaterial = GSD.Roads.GSDRoadUtilityEditor.LoadPhysicsMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Physics/GSDDirt.physicMaterial");
        }
    }


    public void SetDefaultMats()
    {
        if (roadMaterialDropdown == RoadMaterialDropdownEnum.Asphalt)
        {
            RoadMaterial1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/GSDRoad1.mat");
            RoadMaterial2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDRoadDetailOverlay1.mat");

            if (laneAmount == 2)
            {
                RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble.mat");
            }
            else if (laneAmount == 4)
            {
                RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble-4L.mat");
            }
            else if (laneAmount == 6)
            {
                RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble-6L.mat");
            }
            else
            {
                RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDWhiteYellowDouble.mat");
            }

            if (laneAmount == 2)
            {
                RoadMaterialMarker2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDTireMarks.mat");
            }
            else if (laneAmount == 4)
            {
                RoadMaterialMarker2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDTireMarks-4L.mat");
            }
            else if (laneAmount == 6)
            {
                RoadMaterialMarker2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDTireMarks-6L.mat");
            }
            else
            {
                RoadMaterialMarker2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDTireMarks.mat");
            }

            ShoulderMaterial1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/GSDShoulder1.mat");
            ShoulderMaterial2 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/Markers/GSDRoadDetailOverlay1.mat");

            RoadPhysicMaterial = GSD.Roads.GSDRoadUtilityEditor.LoadPhysicsMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Physics/GSDPavement.physicMaterial");
            ShoulderPhysicMaterial = GSD.Roads.GSDRoadUtilityEditor.LoadPhysicsMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Physics/GSDDirt.physicMaterial");
        }
        else if (roadMaterialDropdown == RoadMaterialDropdownEnum.Dirt)
        {
            RoadMaterial1 = null;
            RoadMaterial2 = null;
            RoadMaterial3 = null;
            RoadMaterial4 = null;
            RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/GSDDirtRoad.mat");
            RoadMaterialMarker2 = null;
            RoadMaterialMarker3 = null;
            RoadMaterialMarker4 = null;
        }
        else if (roadMaterialDropdown == RoadMaterialDropdownEnum.Brick)
        {
            RoadMaterial1 = null;
            RoadMaterial2 = null;
            RoadMaterial3 = null;
            RoadMaterial4 = null;
            RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/GSDBrickRoad.mat");
            RoadMaterialMarker2 = null;
            RoadMaterialMarker3 = null;
            RoadMaterialMarker4 = null;
        }
        else if (roadMaterialDropdown == RoadMaterialDropdownEnum.Cobblestone)
        {
            RoadMaterial1 = null;
            RoadMaterial2 = null;
            RoadMaterial3 = null;
            RoadMaterial4 = null;
            RoadMaterialMarker1 = GSD.Roads.GSDRoadUtilityEditor.LoadMaterial(GSD.Roads.GSDRoadUtilityEditor.GetBasePath() + "/Materials/GSDCobblestoneRoad.mat");
            RoadMaterialMarker2 = null;
            RoadMaterialMarker3 = null;
            RoadMaterialMarker4 = null;
        }
        if (roadMaterialDropdown == RoadMaterialDropdownEnum.Brick
        || roadMaterialDropdown == RoadMaterialDropdownEnum.Cobblestone
        || roadMaterialDropdown == RoadMaterialDropdownEnum.Dirt)
        {
            if (laneAmount > 2)
            {
                RoadMaterialMarker1 = new Material(RoadMaterialMarker1);
                RoadMaterialMarker1.mainTextureScale *= new Vector2(laneAmount / 2, 1f);
            }
        }

        int nodeCount = spline.GetNodeCount();
        for (int i = 0; i < nodeCount; i++)
        {
            if (spline.nodes[i] && spline.nodes[i].isIntersection && spline.nodes[i].intersection != null && spline.nodes[i].intersection.isUsingDefaultMaterials)
            {
                spline.nodes[i].intersection.ResetMaterialsAll();
            }
        }
    }
    #endregion


    public void Wireframes_Toggle()
    {
        MeshRenderer[] MRs = transform.GetComponentsInChildren<MeshRenderer>();
        Wireframes_Toggle_Help(ref MRs);

        if (spline != null)
        {
            MRs = spline.transform.GetComponentsInChildren<MeshRenderer>();
            Wireframes_Toggle_Help(ref MRs);
        }
    }


    private void Wireframes_Toggle_Help(ref MeshRenderer[] _MRs)
    {
        int meshRenderersCount = _MRs.Length;
        for (int i = 0; i < meshRenderersCount; i++)
        {
            //EditorUtility.SetSelectedWireframeHidden(tMRs[i], !opt_GizmosEnabled);
            EditorUtility.SetSelectedRenderState(_MRs[i], isGizmosEnabled ? EditorSelectedRenderState.Wireframe : EditorSelectedRenderState.Hidden);
        }
    }

#endif


    private void Start()
    {
#if UNITY_EDITOR
        if (Application.isPlaying)
        {
            CleanRunTime();
        }
#else
        this.enabled = false;
#endif
    }


#if UNITY_EDITOR
    //For compliance on submission rules:
    public void UpdateGizmoOptions()
    {
        if (spline == null)
        {
            return;
        }
        GSDSplineN node = null;

        int nodeCount = spline.GetNodeCount();
        for (int i = 0; i < nodeCount; i++)
        {
            node = spline.nodes[i];
            if (node != null)
            {
                node.isGizmosEnabled = isGizmosEnabled;
            }
        }
    }


    public void SetupUniqueIdentifier()
    {
        if (UID == null || UID.Length < 4)
        {
            UID = System.Guid.NewGuid().ToString();
        }
    }


    public void DuplicateRoad()
    {
        GameObject roadObj = GSDRS.AddRoad();
        UnityEditor.Undo.RegisterCreatedObjectUndo(roadObj, "Duplicate");

        GSDRoad road = roadObj.GetComponent<GSDRoad>();
        if (road == null)
        {
            return;
        }

        //Road editor options: 
        road.laneWidth = laneWidth;
        road.isShouldersEnabled = isShouldersEnabled;	
        road.shoulderWidth = shoulderWidth;
        road.laneAmount = laneAmount;	
        road.roadDefinition = roadDefinition;
        road.roadCornerDefinition = roadCornerDefinition;
        road.isRoadCutsEnabled = isRoadCutsEnabled;
        road.isShoulderCutsEnabled = isShoulderCutsEnabled;
        road.isDynamicCutsEnabled = isDynamicCutsEnabled;
        road.isMaxGradeEnabled = isMaxGradeEnabled;
        road.maxGrade = maxGrade;
        road.isUsingDefaultMaterials = isUsingDefaultMaterials;
        road.isAutoUpdatingInEditor = isAutoUpdatingInEditor;

        road.matchTerrainSubtraction = matchTerrainSubtraction;
        road.isRaisingRoad = isRaisingRoad;

        road.matchHeightsDistance = matchHeightsDistance;
        road.clearDetailsDistance = clearDetailsDistance;
        road.clearDetailsDistanceHeight = clearDetailsDistanceHeight;
        road.clearTreesDistance = clearTreesDistance;
        road.clearTreesDistanceHeight = clearTreesDistanceHeight;

        road.isHeightModificationEnabled = isHeightModificationEnabled;
        road.isDetailModificationEnabled = isDetailModificationEnabled;
        road.isTreeModificationEnabled = isTreeModificationEnabled;

        road.isSavingTerrainHistoryOnDisk = isSavingTerrainHistoryOnDisk;
        road.magnitudeThreshold = magnitudeThreshold;
        road.isGizmosEnabled = isGizmosEnabled;
        road.isUsingMultithreading = isUsingMultithreading;
        road.isSavingMeshes = isSavingMeshes;
        road.isUsingMeshColliders = isUsingMeshColliders;

        road.roadMaterialDropdown = roadMaterialDropdown;
        road.tRoadMaterialDropdownOLD = tRoadMaterialDropdownOLD;

        road.RoadMaterial1 = RoadMaterial1;
        road.RoadMaterial2 = RoadMaterial2;
        road.RoadMaterial3 = RoadMaterial3;
        road.RoadMaterial4 = RoadMaterial4;
        road.RoadMaterialMarker1 = RoadMaterialMarker1;
        road.RoadMaterialMarker2 = RoadMaterialMarker2;
        road.RoadMaterialMarker3 = RoadMaterialMarker3;
        road.RoadMaterialMarker4 = RoadMaterialMarker4;
        road.ShoulderMaterial1 = ShoulderMaterial1;
        road.ShoulderMaterial2 = ShoulderMaterial2;
        road.ShoulderMaterial3 = ShoulderMaterial3;
        road.ShoulderMaterial4 = ShoulderMaterial4;
        road.ShoulderMaterialMarker1 = ShoulderMaterialMarker1;
        road.ShoulderMaterialMarker2 = ShoulderMaterialMarker2;
        road.ShoulderMaterialMarker3 = ShoulderMaterialMarker3;
        road.ShoulderMaterialMarker4 = ShoulderMaterialMarker4;

        road.RoadPhysicMaterial = RoadPhysicMaterial;
        road.ShoulderPhysicMaterial = ShoulderPhysicMaterial;

        road.spline.TriggerSetup();

        Selection.activeGameObject = road.transform.gameObject;
    }


    private void FixZ()
    {
        FixZ_Mobile();
    }


    private void FixZ_Mobile()
    {
        //This road:
        Object[] markerObjs = transform.GetComponentsInChildren<MeshRenderer>();
        Vector3 vector = default(Vector3);
        foreach (MeshRenderer MR in markerObjs)
        {
            if (MR.transform.name.Contains("Marker"))
            {
                vector = new Vector3(0f, 0.02f, 0f);
                MR.transform.localPosition = vector;
            }
            else if (MR.transform.name.Contains("SCut") || MR.transform.name.Contains("RoadCut")
                || MR.transform.name.Contains("ShoulderR")
                || MR.transform.name.Contains("ShoulderL"))
            {
                vector = MR.transform.position;
                vector.y += 0.01f;
                MR.transform.position = vector;
            }
            else if (MR.transform.name.Contains("RoadMesh"))
            {
                vector = MR.transform.position;
                vector.y += 0.02f;
                MR.transform.position = vector;
            }
            else if (MR.transform.name.Contains("Pavement"))
            {
                vector = MR.transform.position;
                vector.y -= 0.01f;
                MR.transform.position = vector;
            }
        }


        //Intersections (all):
        markerObjs = GSDRS.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer MR in markerObjs)
        {
            if (MR.transform.name.Contains("CenterMarkers"))
            {
                vector = new Vector3(0f, 0.02f, 0f);
                MR.transform.localPosition = vector;
            }
            else if (MR.transform.name.Contains("-Inter") && MR.transform.name.Contains("-Lane"))
            {
                vector = new Vector3(0f, 0.02f, 0f);
                MR.transform.localPosition = vector;
            }
            else if (MR.transform.name.Contains("-Inter") && MR.transform.name.Contains("-Stretch"))
            {
                vector = new Vector3(0f, 0.03f, 0f);
                MR.transform.localPosition = vector;
            }
            else if (MR.transform.name.Contains("-Inter") && MR.transform.name.Contains("-Tiled"))
            {
                vector = new Vector3(0f, 0.01f, 0f);
                MR.transform.localPosition = vector;
            }
        }
    }


    private void FixZ_Win()
    {
        //This road:
        Object[] markerObjs = transform.GetComponentsInChildren<MeshRenderer>();
        Vector3 vector = default(Vector3);
        foreach (MeshRenderer MR in markerObjs)
        {
            if (MR.transform.name.Contains("Marker"))
            {
                vector = new Vector3(0f, 0.01f, 0f);
                MR.transform.localPosition = vector;
            }
        }


        //Intersections (all):
        markerObjs = Object.FindObjectsOfType<MeshRenderer>();
        foreach (MeshRenderer MR in markerObjs)
        {
            if (MR.transform.name.Contains("-Inter") && MR.transform.name.Contains("-Lane"))
            {
                vector = new Vector3(0f, 0.01f, 0f);
                MR.transform.localPosition = vector;
            }
            else if (MR.transform.name.Contains("-Inter") && MR.transform.name.Contains("-Stretch"))
            {
                vector = new Vector3(0f, 0.01f, 0f);
                MR.transform.localPosition = vector;
            }
        }
    }
#endif
}