using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.UI
{
    public class MenuGroup : MonoBehaviour
    {
        public List<GameObject> Menus = new();

        public void NavigateTo(GameObject menu)
        {
            if (menu == null || !Menus.Contains(menu))
                return;

            CloseMenus();
            menu.SetActive(true);
        }

        private void CloseMenus()
        {
            foreach (GameObject menu in Menus)
            {
                menu.SetActive(false);
            }
        }
    }
}