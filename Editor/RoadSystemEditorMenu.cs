#if UNITY_EDITOR
#region "Imports"
using UnityEngine;
using UnityEditor;
using RoadArchitect;
#endregion


/// <summary> Provides the menu items inside the editor  </summary>
public class RoadSystemEditorMenu : ScriptableObject
{
    /// <summary> Creates the road system. </summary>
    [MenuItem("Window/Road Architect/Create road system")]
    public static void CreateRoadSystem()
    {
        Object[] allRoadSystemObjects = GameObject.FindObjectsOfType(typeof(RoadSystem));
        int nextCount = (allRoadSystemObjects.Length + 1);
        allRoadSystemObjects = null;

        GameObject newRoadSystemObject = new GameObject("RoadArchitectSystem" + nextCount.ToString());
        RoadSystem newRoadSystem = newRoadSystemObject.AddComponent<RoadSystem>();
        //Add road for new road system.
        newRoadSystem.AddRoad(true);

        GameObject masterIntersectionsObject = new GameObject("Intersections");
        masterIntersectionsObject.transform.parent = newRoadSystemObject.transform;
    }


    /// <summary> Add road to gameobject. Not sure if this is necessary. </summary>
	[MenuItem("Window/Road Architect/Add road")]
    public static void AddRoad()
    {
        Object[] allRoadSystemObjects = GameObject.FindObjectsOfType(typeof(RoadSystem));
        if (allRoadSystemObjects != null && allRoadSystemObjects.Length == 0)
        {
            CreateRoadSystem();
            return;
        }
        else
        {
            RoadSystem firstRoadSystem = (RoadSystem) allRoadSystemObjects[0];
            Selection.activeGameObject = firstRoadSystem.AddRoad();
        }
    }


    /// <summary> Updates all roads. Used when things get out of sync. </summary>
    [MenuItem("Window/Road Architect/Update All Roads")]
    public static void UpdateAllRoads()
    {
        Road[] allRoadObjects = (Road[]) GameObject.FindObjectsOfType(typeof(Road));

        int roadCount = allRoadObjects.Length;

        Road singleRoad = null;
        SplineC[] tPiggys = null;
        if (roadCount > 1)
        {
            tPiggys = new SplineC[roadCount - 1];
        }

        for (int count = 0; count < roadCount; count++)
        {
            singleRoad = allRoadObjects[count];
            if (count > 0)
            {
                tPiggys[count - 1] = singleRoad.spline;
            }
        }

        singleRoad = allRoadObjects[0];
        if (tPiggys != null && tPiggys.Length > 0)
        {
            singleRoad.PiggyBacks = tPiggys;
        }
        singleRoad.UpdateRoad();
    }


    /// <summary> Show the help screen. </summary>
    [MenuItem("Window/Road Architect/Help")]
    public static void GSDRoadsHelp()
    {
        HelpWindow helpWindow = EditorWindow.GetWindow<HelpWindow>();
        helpWindow.Initialize();
    }


    /// <summary> WARNING: Only call this on an empty scene that has some terrains on it. We are not responsbile for data loss if this function is called by the user. </summary>
    [MenuItem("Window/Road Architect/Testing/Run all unit tests (caution)")]
    public static void TestProgram()
    {
        if (UnityEditor.EditorUtility.DisplayDialog("Warning !", "This will delete your first RoadSystem and will create a lot of test roads.", "OK", "Cancel"))
        {
            RoadArchitect.Tests.UnitTests.RoadArchitectUnitTests();
        }
    }


    /// <summary> WARNING: Only call this on an empty scene that has some terrains on it. We are not responsbile for data loss if this function is called by the user. </summary>
    [MenuItem("Window/Road Architect/Testing/Clean up tests (caution)")]
    public static void TestCleanup()
    {
        if(UnityEditor.EditorUtility.DisplayDialog("Warning !", "This will delete your first RoadSystem and will create a lot of test roads.", "OK", "Cancel"))
        {
            RoadArchitect.Tests.UnitTests.CleanupTests();
        }
    }


    /// <summary> Get code line count for RA project. </summary>
    [MenuItem("Window/Road Architect/Testing/Get line count of RA")]
    public static void TestCodeCount()
    {
        string mainDir = System.Environment.CurrentDirectory + "/" + RoadEditorUtility.GetBasePath();
        string[] files = System.IO.Directory.GetFiles(mainDir, "*.cs", System.IO.SearchOption.AllDirectories);
        int lineCount = 0;
        foreach (string file in files)
        {
            lineCount += System.IO.File.ReadAllLines(file).Length;
        }
        Debug.Log(string.Format("{0:n0}", lineCount) + " lines of code in Road Architect.");
    }


    [MenuItem("Window/Road Architect/Report a Bug")]
    public static void ReportBug()
    {
        Application.OpenURL("https://github.com/FritzsHero/RoadArchitect/issues");
    }
}
#endif