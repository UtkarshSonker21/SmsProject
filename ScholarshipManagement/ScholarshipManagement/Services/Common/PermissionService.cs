using ScholarshipManagement.DTOs.Common.Menu;
using ScholarshipManagement.Helper;

namespace ScholarshipManagement.Services.Common
{
    public class PermissionService
    {
        private Dictionary<string, LoadMenuDto> _menuMap = new();

        public PermissionService()
        {
        }

        public void BuildMenuMap(List<LoadMenuDto> menus)
        {
            var allMenus = new List<LoadMenuDto>();

            foreach (var menu in menus)
            {
                allMenus.Add(menu);

                if (menu.SubMenus != null && menu.SubMenus.Any())
                    allMenus.AddRange(menu.SubMenus);
            }

            _menuMap = allMenus.ToDictionary(x => x.ActualName);
            var result = _menuMap;
        }


        public bool CanView(string menuKey)
        {
            var result = _menuMap.TryGetValue(menuKey, out var menu)
                && menu.Permissions?.CanView == true;

            return result;
        }

        public bool CanInsert(string menuKey)
        {
            var result = _menuMap.TryGetValue(menuKey, out var menu)
                && menu.Permissions?.CanInsert == true;

            return result;
        }

        public bool CanUpdate(string menuKey)
        {
            var result = _menuMap.TryGetValue(menuKey, out var menu)
                && menu.Permissions?.CanUpdate == true;

            return result;
        }

        public bool CanDelete(string menuKey)
        {
            var result = _menuMap.TryGetValue(menuKey, out var menu)
                && menu.Permissions?.CanDelete == true;

            return result;
        }

    }
}
