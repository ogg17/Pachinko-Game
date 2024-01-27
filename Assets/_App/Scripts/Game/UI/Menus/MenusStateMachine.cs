using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VgGames.Core.InjectModule;

namespace VgGames.Game.UI.Menus
{
    public class MenusStateMachine : IInjectable
    {
        private readonly Dictionary<MenuType, Menu> _menus = new();
        private Menu _currentMenu;

        public void Add(Menu menu)
        {
            _menus.Add(menu.Type, menu);
        }

        public async UniTask SetMenu(MenuType type)
        {
            if (_menus.TryGetValue(type, out var menu))
            {
                if(_currentMenu != null)
                    await _currentMenu.Disable();
                await menu.Enable();
                _currentMenu = menu;
            }
        }
    }
}