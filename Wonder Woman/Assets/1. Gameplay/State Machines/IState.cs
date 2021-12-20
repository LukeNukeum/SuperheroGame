using System.Collections;

namespace LupiLab.StateMachines
{
    public interface IState
    {
        void OnEnter();
        void OnExit();
        void Tick();
    }
}