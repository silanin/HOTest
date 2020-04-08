using System.IO;
using UnityEditor;
using UnityEngine;
using HiddenObject.Constants;

namespace HiddenObject.UI.SpriteScriptable.Editor
{
    public class ExportSpriteScriptable
    {
        [MenuItem("Assets/Create/SpriteScriptableObject")]
        static void Export()
        {
            var counter = 0;
            foreach (var o in Selection.objects)
            {
                Sprite sprite;
                if (o is Sprite)
                {
                    sprite = o as Sprite;
                    counter++;
                }
                else if (o is Texture2D)
                {
                    sprite = AssetDatabase.LoadAssetAtPath<Sprite>(AssetDatabase.GetAssetPath(o));
                    counter++;
                }
                else continue;

                if (!Directory.Exists(PathConstants.ScriptablePath))
                {
                    Directory.CreateDirectory(PathConstants.ScriptablePath);
                }

                var fullPath = Path.Combine(PathConstants.ScriptablePath, sprite.name + ".asset");
                if (File.Exists(fullPath))
                {
                    var obj = AssetDatabase.LoadAssetAtPath<SpriteScriptableObject>(fullPath);
                    if (obj.Sprite != sprite && EditorUtility.DisplayDialog("Sprite Exist", "Sprite with the same name is exist!", "Replace", "Cancel"))
                    {
                        obj.Sprite = sprite;
                    }

                    continue;
                }

                var s = ScriptableObject.CreateInstance<SpriteScriptableObject>();
                s.Sprite = sprite;
                AssetDatabase.CreateAsset(s, PathConstants.ScriptablePath + "/" + sprite.name + ".asset");
            }
            if (counter == 0)
            {
                EditorUtility.DisplayDialog("No Sprites Selected", "Please select sprites to use this function.", "OK");
            }
            else
            {
                AssetDatabase.SaveAssets();
                EditorUtility.DisplayDialog("Sprites Exported", "There were " + counter + " sprites exported as scriptable objects.", "OK");
            }
        }
    }
}