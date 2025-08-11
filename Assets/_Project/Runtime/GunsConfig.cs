using UnityEngine;

namespace _Project.Runtime
{
    [CreateAssetMenu(fileName = "GunsConfig", menuName = "Guns Config")]
    public class GunsConfig : ScriptableObject
    {
        public GunData[] Guns;
    }
}