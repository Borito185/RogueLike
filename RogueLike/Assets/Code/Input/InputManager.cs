using UnityEngine;

namespace Assets.Code.Input
{
    public class InputManager : Singleton<InputManager>
    {
        [field: SerializeField] 
        public KeyBindings Bindings { get; private set; } = new();

        [field: SerializeField]
        public EventValue<Vector2> Direction { get; private set; } = new();

        [field: SerializeField]
        public EventValue<bool> Dash { get; private set; } = new();
        [field: SerializeField]
        public EventValue<Vector3> Mouse { get; private set; } = new();
        [field: SerializeField]
        public EventValue<bool> Escape { get; private set; } = new();
        [field: SerializeField]
        public EventValue<bool> Interact { get; private set; } = new();
        // Update is called once per frame
        void Update()
        {
            ReadMovement();
            ReadMouse();
            ReadButtons();
        }

        private void ReadButtons()
        {
            Escape.Value = GetKey(KeyCode.Escape);

            Dash.Value = GetKey(Bindings.Dash);

            Interact.Value = GetKey(Bindings.Interact);

            static bool GetKey(KeyCode key)
            {
                return UnityEngine.Input.GetKey(key);
            }
        }

        public void Start()
        {
            DontDestroyOnLoad(this);
        }

        private void ReadMouse()
        {
            Mouse.Value = UnityEngine.Input.mousePosition;
        }

        private void ReadMovement()
        {
            float xDirection = ReadAxis(Bindings.MoveRight, Bindings.MoveLeft);
            float yDirection = ReadAxis(Bindings.MoveUp, Bindings.MoveDown);

            Vector2 fullDirection = new (xDirection, yDirection);

            Direction.Value = fullDirection.normalized;
            float ToFloat(bool value) => value ? 1f : 0;

            float ReadAxis(KeyCode plus, KeyCode minus)
            {
                float value = ToFloat(UnityEngine.Input.GetKey(plus));
                value -= ToFloat(UnityEngine.Input.GetKey(minus));
                return value;
            }
        }
    }
}
