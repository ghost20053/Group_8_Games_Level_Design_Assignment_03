using UnityEngine;
using System.Collections;
using UnityEditor;
 
public static class ExportWithLayers 
{
 
    [MenuItem("Assets/Asset Store Tools/Export package with tags and physics layers")]
    public static void ExportPackage()
    {
        // Comment.
		// Another comment from S02-530-00
        string[] projectContent = new string[] {"Assets", "ProjectSettings/TagManager.asset", "ProjectSettings/Physics2DSettings.asset", "Library/BuildSettings.asset"};
        AssetDatabase.ExportPackage(projectContent, "2101GFS_2DPlatformer.unitypackage", ExportPackageOptions.Interactive | ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
        Debug.Log("Project Exported");
    }
 
}