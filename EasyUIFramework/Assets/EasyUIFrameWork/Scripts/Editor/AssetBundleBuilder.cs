using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class AssetBundleBuilder : Editor {

    /// <summary>
    /// 打包,默认输入目录为Assetbundles,输出目录为StreamingAssets
    /// </summary>
    /// <param name="buildTarget"></param>
	private static void BaseBuildAssetbundle (BuildTarget buildTarget)
    {
        string inPath = "Assets/Assetbundles";
        string outPath = "Assets/StreamingAssets";
        AssetBundleBuild[] builds = null;

        //目前仅考虑prefab 可自行调整
        string[] files = Directory.GetFiles(inPath,"*.prefab",SearchOption.AllDirectories);
        int count = files.Length;
        builds = new AssetBundleBuild[count];
        for (int i = 0;i < count;i ++)
        {
            AssetBundleBuild abb = new AssetBundleBuild();

            string[] temp = files[i].Split('\\');
            string resName = temp[temp.Length-1].Split('.')[0];

            Debug.Log(string.Format(" {0}/{1} -- {2} {3}", i + 1, count, files[i], resName));

            //尝试添加进build
            abb.assetBundleName = resName;
            abb.assetNames = new string[] { files[i] };
            builds[i] = abb;
        }

        BuildPipeline.BuildAssetBundles(outPath, builds, BuildAssetBundleOptions.None, buildTarget);
    }

    [MenuItem("Build Assetbundle/Android")]
    private static void BuildAndroidAssetbundle_Android ()
    {
        BaseBuildAssetbundle(BuildTarget.Android);
    }

    [MenuItem("Build Assetbundle/IOS")]
    private static void BuildAndroidAssetbundle_IOS ()
    {
        BaseBuildAssetbundle(BuildTarget.iOS);
    }
}
