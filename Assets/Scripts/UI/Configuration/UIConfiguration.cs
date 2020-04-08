using System;
using System.Collections.Generic;
using System.Linq;
using HiddenObject.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HiddenObject.UI.Configuration
{
    [CreateAssetMenu(fileName = "UIConfiguration", menuName = "UIConfiguration", order = 51)]
    public class UIConfiguration : ScriptableObject
    {
        [SerializeField]
        public List<UIScene> _scenes;

        public UIScene GetScene(SceneType type)
        {
            return _scenes.FirstOrDefault(x => x.Type == type);
        }
    }

    [Serializable]
    public class UIScene
    {
        [SerializeField]
        public SceneType Type;
        [SerializeField]
        public LoadSceneMode LoadType;
        [SerializeField]
        public string Scene;
    }
}