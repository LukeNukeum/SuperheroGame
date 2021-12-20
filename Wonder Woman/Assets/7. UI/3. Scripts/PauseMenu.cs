using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LupiLab.Core;

namespace LupiLab.UI
{
    [AddComponentMenu("LupiLab/UI/Menus/Pause Menu")]
    public class PauseMenu : Menu
    {

        public enum PauseMenus { None, Main, Options }
        [SerializeField] private GameObject Background;
        [SerializeField] private GameObject OptionsMenu;





        public void QuitLevel()
        {
            //_gameManager.LoadScene(0);
        }


        public override void Open()
        {
            Background.SetActive(true);
            base.Open();
        }

        public override void Open(IMenu previousMenu)
        {
            Background.SetActive(true);
            base.Open(previousMenu: previousMenu);
        }

        public override void OpenOtherMenu(IMenu otherMenu)
        {
            otherMenu.Open(previousMenu: this);
            base.Close();
        }

        public override void OpenOtherMenu(Menu otherMenu)
        {
            otherMenu.Open(previousMenu: this);
            base.Close();
        }

        public override void Close()
        {
            base.Close();
            Background.SetActive(false);
        }

        public override void Back()
        {
            //ResumeGame();
        }
    }
}