using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HiddenObject.Components
{
    public class StarComponent : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> _activeStars;

        public int Stars
        {
            get
            {
                return _activeStars.Count(x => x.activeSelf);
            }
            set
            {
                for (int i = 0; i < _activeStars.Count; i++)
                {
                    _activeStars[i].SetActive(value > i);
                }
            }
        }
    }
}
