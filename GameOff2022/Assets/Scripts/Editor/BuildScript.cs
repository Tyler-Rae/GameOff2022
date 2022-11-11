using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Reporting;

public class BuildScript
{
    [MenuItem("Build/Open Build Directory")]
    static void OpenBuildDirectory()
    {
        System.Diagnostics.Process.Start("explorer.exe", "_Builds");
    }

    [MenuItem("Build/Clean Builds")]
    static void CleanBuilds()
    {
        CleanDirectory("_Builds/Windows");
    }

    static void CleanDirectory(string path)
    {
        System.Console.WriteLine("Cleaning " + path + "...");
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
    }

    [MenuItem("Build/Build All")]
    static void BuildAll()
    {
        BuildClientWindows();
    }

    [MenuItem("Build/Build Client (Windows)")]
    static void BuildClientWindows()
    {
        CleanDirectory("_Builds/Windows");

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new string[] { "Assets/Scenes/GameScene.unity" };
        buildPlayerOptions.locationPathName = "_Builds/Windows/GameOff2022_win64.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;

        System.Console.WriteLine("Building Client (Windows) ...");
        BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);

        if (buildReport.summary.result == BuildResult.Succeeded)
        {
            System.Console.WriteLine("Build Finished!");
        }
        else if (buildReport.summary.result == BuildResult.Failed)
        {
            System.Console.WriteLine("Build Failed!");
        }
    }
}
