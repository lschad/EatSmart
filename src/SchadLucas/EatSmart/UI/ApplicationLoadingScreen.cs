using SchadLucas.EatSmart.Utilities;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace SchadLucas.EatSmart.UI
{
    public class ApplicationLoadingScreen
    {
        private readonly Window _window;

        public event EventHandler Activated;
        public event EventHandler Closed;

        public double Left => _window.Left;
        public double Top => _window.Top;

        public ApplicationLoadingScreen(object view)
        {
            Ensure.NotNull(view);

            _window = new Window
            {
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                WindowStyle = WindowStyle.None,
                Background = Brushes.Transparent,
                Width = 800,
                Height = 600,
                ShowInTaskbar = false,
                AllowsTransparency = true,
                Content = view,
                Opacity = 1
            };

            _window.Activated += OnActivated;
            _window.Closed += OnClosed;
            _window.MouseDown += OnMouseDown;

            _window.Hide();
        }

        private void OnActivated(object sender, EventArgs e)
        {
            Activated?.Invoke(sender, e);
        }

        private void OnClosed(object sender, EventArgs e)
        {
            UnsubscribeEvents();

            Closed?.Invoke(sender, e);
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _window.DragMove();
            }
        }

        private void UnsubscribeEvents()
        {
            _window.Activated -= OnActivated;
            _window.Closed -= OnClosed;
            _window.MouseDown -= OnMouseDown;
        }

        public void Close()
        {
            _window.Close();
        }

        public void Show()
        {
            _window.Show();
            _window.Activate();
        }
    }
}