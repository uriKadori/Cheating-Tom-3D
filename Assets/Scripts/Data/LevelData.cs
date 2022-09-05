using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
    public class LevelData : ScriptableObject
    {
        public GridData GridData => gridData;
        public float TeacherTimeToLook => teacherTimeToLook;

        [SerializeField] private GridData gridData;
        [SerializeField] private float teacherTimeToLook;
    }
}