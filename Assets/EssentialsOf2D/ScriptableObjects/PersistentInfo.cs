namespace GemDrop
{
    using UnityEngine;

	[CreateAssetMenu(fileName = "PersistentInfo", menuName = "ScriptableObjects/PersistentInfo", order = 2)]
	public class PersistentInfo : ScriptableObject
    {
        public Vector3 playerStart;
        public bool controlEnabled;
        public int currentSceneNumber;
        public int numberOfScenes;
        public int scoreToProgress;
    }
}