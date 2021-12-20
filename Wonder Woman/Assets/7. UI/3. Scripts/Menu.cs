using UnityEngine;
using UnityEngine.UI;

namespace LupiLab.UI
{
    [AddComponentMenu("LupiLab/UI/Menus/Basic Menu")]
    public class Menu : MonoBehaviour, IMenu
    {
        [SerializeField] private Selectable FirstSelectedElement;
        protected IMenu _previousMenu;

        public virtual void Open()
        {
            gameObject.SetActive(true);
            FirstSelectedElement?.Select();
        }

        public virtual void Open(IMenu previousMenu)
        {
            _previousMenu = previousMenu;
            Open();
        }

        public virtual void Close()
        {
            gameObject.SetActive(false);
        }

        public virtual void OpenOtherMenu(IMenu otherMenu)
        {
            otherMenu.Open(previousMenu: this);
            Close();
        }

        public virtual void OpenOtherMenu(Menu otherMenu)
        {
            otherMenu.Open(previousMenu: this);
            Close();
        }

        public virtual void Back()
        {
            _previousMenu?.Open();
            _previousMenu = null;
            Close();
        }
    }
}