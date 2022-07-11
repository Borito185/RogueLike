using System;
using UnityEngine;

namespace Assets.Code.Input
{
    [Serializable]
    public class KeyBindings
    {
        [field: SerializeField]
        public KeyCode MoveUp { get; set; } = KeyCode.W;
        [field: SerializeField]
        public KeyCode MoveLeft { get; set; } = KeyCode.A;
        [field: SerializeField]
        public KeyCode MoveDown { get; set; } = KeyCode.S;
        [field: SerializeField]
        public KeyCode MoveRight { get; set; } = KeyCode.D;
        [field: SerializeField]
        public KeyCode Dash { get; set; } = KeyCode.Space;
        [field: SerializeField]
        public KeyCode Interact { get; set; } = KeyCode.E;
    }
}
