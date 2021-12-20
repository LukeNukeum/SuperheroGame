namespace LupiLab.UI
{
    public interface IMenu
    {
        void Open();
        void Open(IMenu previousMenu);
        void OpenOtherMenu(IMenu subMenu);
        void Close();
        void Back();
    }
}