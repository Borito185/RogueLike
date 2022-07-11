using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using TMPro;
using Assets.Code.Input;
using System.Collections;

namespace Assets.Code.UI.Menus
{
    public class KeybindingButton : MonoBehaviour
    {
        private PropertyInfo Property;
        public TextMeshProUGUI ButtonText;
        public TextMeshProUGUI LabelText;
        private static bool IsAwaitingKey = false;
        //kill me
        private static readonly int[] PossibleKeybinds = {9, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 59, 91, 92, 93, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 256, 257, 258, 259, 260, 261, 262, 263, 264, 265, 266, 267, 268, 269, 270, 271, 272, 273, 274, 275, 276, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291, 292, 293, 294, 295, 296, 301, 303, 304, 305, 306, 307, 308, 323, 324, 325, 326, 327, 328, 329};

        public void PropertyInfo(PropertyInfo propInfo)
        {
            Property = propInfo;
            LabelText.text = ConvertLabel(Property.Name);
            DisplayKeybinding();
        }

        public string ConvertLabel(string label)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(label[0]);
            for (int i = 1; i < label.Length; i++)
            {
                char c = label[i];
                if (char.IsUpper(c)) sb.Append(' ');
                sb.Append(char.ToLower(c));
            }
            return sb.ToString();
        }

        public void DisplayKeybinding()
        {
            ButtonText.text = Property.GetValue(InputManager.Instance.Bindings).ToString();
        }

        public void StartListeningForKey()
        {
            if (IsAwaitingKey) return;
            IsAwaitingKey = true;
            StartCoroutine(AwaitKey());
        }

        public IEnumerator AwaitKey()
        {
            bool foundKey = false;
            KeyCode pressedKey = KeyCode.None;

            while (!foundKey)
            {
                while (!UnityEngine.Input.anyKeyDown)
                {
                    yield return null;
                }

                foreach (int key in PossibleKeybinds)
                {
                    if (UnityEngine.Input.GetKeyDown((KeyCode)key))
                    {
                        foundKey = true;
                        pressedKey = (KeyCode)key;
                        break;
                    }
                }
            }
            ChangeKeybinding(pressedKey);
            IsAwaitingKey = false;
        }

        private void ChangeKeybinding(KeyCode newKey)
        {
            Property.SetValue(InputManager.Instance.Bindings, newKey);
            DisplayKeybinding();
        }
    }
}
