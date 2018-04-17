using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Configuration.Board
{
    [CreateAssetMenu(fileName = "New Gameboard Configuration", menuName = "Gameboard/Configuration")]
    public class GameboardConfiguration : ScriptableObject
    {
        public Vector2Int dimensions;

        public GameObject prefab;
    }
}
