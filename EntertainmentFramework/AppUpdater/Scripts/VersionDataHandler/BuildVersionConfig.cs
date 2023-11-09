using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Build Version Config")]
public class BuildVersionConfig : ScriptableObject
{
    public float versionNumber;
    public float versionCode;
    public string AndroidAppStoreURL;
    public string iOSAppStoreURL;
}
