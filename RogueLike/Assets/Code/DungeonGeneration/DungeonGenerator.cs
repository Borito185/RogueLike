using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Code.DungeonGeneration
{
    [ExecuteAlways]
    public class DungeonGenerator : MonoBehaviour
    {
        [SerializeField]
        private Tilemap Ground;        
        [SerializeField]
        private Tilemap Obstacles;

        [SerializeField]
        private List<DungeonFloor> Rooms;

        [SerializeField]
        private uint _roomsToGen;
        [SerializeField]
        private uint _roomsToKeep;
        [SerializeField]
        private Vector2Int _roomSize = new(5,40);

        public void Generate()
        {
            Ground.ClearAllTiles();
            Obstacles.ClearAllTiles();

           // EditorCoroutineUtility.StartCoroutine(RoomIt(), this);
        }

        public IEnumerator RoomIt()
        {
            //creates an array of random rooms
            Room[] rooms = new Room[_roomsToGen];
            for (int i = 0; i < _roomsToGen; i++)
                rooms[i] = Room.Random(_roomSize.x, _roomSize.y);

            //moves the rooms so none overlap
            for (int i = 0; i < _roomsToGen; i++)
                for (int j = 0; j < i; j++)
                    if (i != j)
                        while (rooms[i].Intersects(rooms[j]))
                            rooms[i] = rooms[i].MoveAwayFromCenter();

            //sorts array from largest to smallest room
            Array.Sort(
                rooms, 
                (first, second) =>
                {
                    int sizeDiff = second.Size - first.Size;
                    int value = sizeDiff + Mathf.RoundToInt(first.Center.magnitude - second.Center.magnitude);
                    return value;
                });

            //select largest rooms
            int grabAmount = Mathf.Min((int)_roomsToKeep, (int)_roomsToGen);
            Room[] selectedRooms = new Room[grabAmount];
            for (int i = 0; i < grabAmount; i++)
                selectedRooms[i] = rooms[i];

            //draw the selected rooms
            for (int i = 0; i < grabAmount; i++)
                DrawRoom(selectedRooms[i]);

            yield break;
        }

        public void DrawRoom(Room room)
        {
            BoundsInt bounds = room.ToBoundsInt();
            for (int i = bounds.xMin; i < bounds.xMax; i++)
                for (int j = bounds.yMin; j < bounds.yMax; j++)
                    Ground.SetTile(new(i, j), Rooms[0].GroundTiles[0]);
        }
    }
}

