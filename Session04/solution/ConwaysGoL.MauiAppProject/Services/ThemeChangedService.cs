using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConwaysGoL.MauiAppProject.Services
{
    public class ThemeChangedService
    {
        private readonly ConditionalWeakTable<object, Action<AppTheme>> _themeChangedListeners;
        private readonly Microsoft.Maui.Controls.Application _application;

        public ThemeChangedService(IApplication application)
        {
            _themeChangedListeners = [];
            _application = (Microsoft.Maui.Controls.Application)application;

            _application.RequestedThemeChanged += (sender, args) =>
            {
                foreach (var (_, listener) in _themeChangedListeners)
                    listener(args.RequestedTheme);
            };
        }

        public AppTheme CurrentTheme => _application.RequestedTheme;

        public void SetThemeChangedListener(object key, Action<AppTheme> listener)
        {
            _themeChangedListeners.AddOrUpdate(key, listener);
        }

        public void RemoveThemeChangedListener(object key)
        {
            _themeChangedListeners.Remove(key);
        }

        public bool TryInvokeListener(object key)
        {
            if (_themeChangedListeners.TryGetValue(key, out var listener))
            {
                listener(CurrentTheme);
                return true;
            }
            return false;
        }
    }
}
