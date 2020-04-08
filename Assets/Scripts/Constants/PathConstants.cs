using UnityEngine;

namespace HiddenObject.Constants
{
    public class PathConstants
    {
        public static readonly string PersistentDataPath = Application.persistentDataPath;
        public static readonly string StreamingAssetsPath = Application.streamingAssetsPath;

        public static readonly string LocalDataFolderName = "LocalData";
        public static readonly string LocalDataPath = PersistentDataPath + "/" + LocalDataFolderName + "/";

        public static readonly string JsonFormat = ".json";
        public static readonly string BinaryFormat = ".dat";

        public static readonly string ScriptableDirectory = "SpriteScriptable";
        public static readonly string ScriptablePath = "Assets/Resources/" + ScriptableDirectory;
    }
}
