using Assets.Code.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Reflection;

namespace Assets.Code.UI.Menus
{
    public class GameplaySettings : MonoBehaviour
    {
        public GameObject ButtonPrefab;
        public GameObject ButtonList;

        private void Start()
        {
            var newBindings = InputManager.Instance.Bindings.GetType().GetProperties().Where(info => info.PropertyType == typeof(KeyCode));
            foreach (var binding in newBindings)
            {
                CreateButton(binding);
            }
        }

        private void CreateButton(PropertyInfo property)
        {
            var button = Instantiate(ButtonPrefab);
            button.transform.SetParent(ButtonList.transform);
            var keybindingButton = button.GetComponent<KeybindingButton>();
            keybindingButton.PropertyInfo(property);
        }
    }
}
