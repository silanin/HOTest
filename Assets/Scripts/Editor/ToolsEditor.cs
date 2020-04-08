using System.IO;
using System.Linq;
using HiddenObject.Constants;
using UnityEditor;
using UnityEngine;

namespace HiddenObject.Tools
{
    public class ToolsEditor : EditorWindow
    {
        [MenuItem("HiddenObject/Delete Empty Folders")]
        public static void DeleteEmptyFolders()
        {
            var directories = Directory.GetDirectories(Application.dataPath, "*", SearchOption.AllDirectories);
            var index = 0;
            var total = directories.Length;
            foreach (var directory in directories)
            {
                if (Directory.Exists(directory))
                {
                    EditorUtility.DisplayProgressBar("Scan", directory, index / (float)total);
                    var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
                    if (files.Length == 0 || files.All(s => Path.GetExtension(s) == ".meta"))
                    {
                        Directory.Delete(directory, true);
                        if (File.Exists(directory + ".meta"))
                        {
                            File.Delete(directory + ".meta");
                        }

                        Debug.Log("DELETE DIRECTORY: " + directory);
                        EditorUtility.DisplayProgressBar("Delete directory", directory, index / (float)total);
                    }
                }

                index++;
            }

            EditorUtility.ClearProgressBar();
        }

        [MenuItem("HiddenObject/Clean all data")]
        public static void CleanCache()
        {
            Caching.ClearCache();
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            CleanModels();
        }

        [MenuItem("HiddenObject/Clean only saved models")]
        public static void CleanModels()
        {
            var files = Directory.GetFiles(PathConstants.LocalDataPath, "*" + PathConstants.BinaryFormat);
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                File.Delete(file);
                Debug.Log("DELETE LOCAL SAVE: " + fileName + PathConstants.BinaryFormat);
            }
        }
    }
}