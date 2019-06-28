using Microsoft.Win32;
using Model.Entities.Surfaces;
using Model.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using View;

namespace MapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileInfo currentFile;
        Map currentMap;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Работа с файлами
        private void NewFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            currentMap = new Map();
            mapImage.Source = GameMapView.DrawMap(currentMap);
            ChangeInterfaceState(true);
        }

        private void OpenFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var openMapDialog = new OpenFileDialog()
            {
                Filter = $"BattleCity Remastered map files ({Map.EXT})|*{Map.EXT}"
            };
            var response = openMapDialog.ShowDialog(this);
            if (!response.HasValue || !response.Value)
                return;
            currentFile = new FileInfo(openMapDialog.FileName);
            using (var file = openMapDialog.OpenFile())
                currentMap = (Map)new BinaryFormatter().Deserialize(file);
            ChangeInterfaceState(true);
        }

        private void SaveFileMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveFileAsMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseFileMenuItem_Click(object sender, RoutedEventArgs e)
        {

            ChangeInterfaceState(false);
        }
        #endregion

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #region Работа с интерфейсом
        private void ChangeInterfaceState(bool isEnabled)
        {
            toolsToolBar.IsEnabled = isEnabled;
            mapToolBar.IsEnabled = isEnabled;
        }
        #endregion

        #region Работа с инструментами
        private void CursorButton_Click(object sender, RoutedEventArgs e)
        {
            Cursor = new Cursor(Application.GetResourceStream(new Uri("Cursors\\DefaultCursor.cur", UriKind.Relative)).Stream);
        }

        private void PencilButton_Click(object sender, RoutedEventArgs e)
        {
            Cursor = new Cursor(Application.GetResourceStream(new Uri("Cursors\\PencilCursor.cur", UriKind.Relative)).Stream);
        }

        private void FillingButton_Click(object sender, RoutedEventArgs e)
        {
            Cursor = new Cursor(Application.GetResourceStream(new Uri("Cursors\\FillCursor.cur", UriKind.Relative)).Stream);
        }

        private void EraserButton_Click(object sender, RoutedEventArgs e)
        {
            Cursor = new Cursor(Application.GetResourceStream(new Uri("Cursors\\EraserCursor.cur", UriKind.Relative)).Stream);
        }
        #endregion

        #region Работа с картой
        private Dictionary<Surface.Kinds, List<BitmapSource>> surfacesAnimations;

        private void LoadSurfaces()
        {
            surfacesAnimations = GameMapView.EnumerateSurfaceAnimations();
            foreach (var pair in surfacesAnimations)
            {
                var newButton = new Button()
                {
                    Content = new Image()
                    {
                        Source = pair.Value[0],
                        Stretch = System.Windows.Media.Stretch.None
                    },
                    ToolTip = pair.Key,
                    Tag = pair.Key,

                };
                newButton.Click += SurfaceButton_Click;
                mapToolBar.Items.Add(newButton);
            }
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CursorButton_Click(null, null);
            LoadSurfaces();      
        }

        private void SurfaceButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
