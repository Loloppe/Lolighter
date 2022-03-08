using System.Windows;
using System.Text.Json;
using Lolighter.Data;
using Lolighter.Algorithm;
using System.IO;
using System.Collections.Generic;
using System;
using Lolighter.Data.Structure;
using System.Linq;
using System.Windows.Data;
using System.Windows.Controls;
using System.Diagnostics;
using Microsoft.Win32;
using Lolighter.Data.V2;

namespace Lolighter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Temporary save the path to song folder
        internal string filePath = "";
        // Ignore null value during serialization
        internal JsonSerializerOptions options = new();
        // Json data
        internal InfoData? infoData = new();
        internal List<DifficultyData> difficultyData = new();
        // Information on the difficulty
        internal List<DataItem> dataItem = new();

        public MainWindow()
        {
            InitializeComponent();

            options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            // Start
            OpenButton.Visibility = Visibility.Visible;

            // Prefill DataGrid
            for (int i = 1; i <= 2; ++i)
            {
                var column = new DataGridTextColumn();
                if(i == 1)
                {
                    column.Header = "Type";
                }
                else
                {
                    column.Header = "#";
                }
                column.Binding = new Binding("Column" + i);
                DiffDataGrid.Columns.Add(column);
            }

            dataItem.Add(new DataItem { Column1 = "Note", Column2 = "" });
            dataItem.Add(new DataItem { Column1 = "Bomb", Column2 = "" });
            dataItem.Add(new DataItem { Column1 = "Obstacle", Column2 = "" });
            dataItem.Add(new DataItem { Column1 = "Chain", Column2 = "" });
            dataItem.Add(new DataItem { Column1 = "Arc", Column2 = "" });
            dataItem.Add(new DataItem { Column1 = "Light", Column2 = "" });
            dataItem.Add(new DataItem { Column1 = "Boost", Column2 = "" });

            foreach(var item in dataItem)
            {
                DiffDataGrid.Items.Add(item);
            }
        }

        /// <summary>
        /// Data for the InfoDataGrid column
        /// </summary>
        public class DataItem
        {
            public string? Column1 { get; set; }
            public string? Column2 { get; set; }
        }

        /// <summary>
        /// Used when files are loaded to show new layout on the window
        /// </summary>
        public void Transition()
        {
            OpenButton.Visibility = Visibility.Collapsed;
            DiffListBox.Visibility = Visibility.Visible;
            DiffListBox.Width = 150;
            DiffListBox.SelectedIndex = 0;
            // Filling the data to show the user
            DiffListBox.SelectionMode = SelectionMode.Single;
            WindowSize.Width = 600;
            LightButton.Visibility = Visibility.Visible;
            DownLightButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Visible;
            //AutomapperButton.Visibility = Visibility.Visible;
            //BombButton.Visibility = Visibility.Visible;
            //ExpandButton.Visibility = Visibility.Visible;
            //LineButton.Visibility = Visibility.Visible;
            //InvertButton.Visibility = Visibility.Visible;
            //LoloppeButton.Visibility = Visibility.Visible;
            //WindowButton.Visibility = Visibility.Visible;
            //ShrinkButton.Visibility = Visibility.Visible;
            //ShuffleButton.Visibility = Visibility.Visible;
            //SliderButton.Visibility = Visibility.Visible;
            //DownmapButton.Visibility = Visibility.Visible;
            // Show data
            DiffDataGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Fill out the InfoDataGrid with currently selected difficulty
        /// </summary>
        /// <param name="Index">Difficulty</param>
        public void FillDataGrid(int Index)
        {
            

            dataItem[0].Column2 = difficultyData[Index].colorNotes.Length.ToString();
            dataItem[1].Column2 = difficultyData[Index].bombNotes.Length.ToString();
            dataItem[2].Column2 = difficultyData[Index].obstacles.Length.ToString();
            dataItem[3].Column2 = difficultyData[Index].burstSliders.Length.ToString();
            dataItem[4].Column2 = difficultyData[Index].sliders.Length.ToString();
            dataItem[5].Column2 = difficultyData[Index].basicBeatmapEvents.Length.ToString();
            dataItem[6].Column2 = difficultyData[Index].colorBoostBeatmapEvents.Length.ToString();

            DiffDataGrid.Items[0] = dataItem[0];
            DiffDataGrid.Items[1] = dataItem[1];
            DiffDataGrid.Items[2] = dataItem[2];
            DiffDataGrid.Items[3] = dataItem[3];
            DiffDataGrid.Items[4] = dataItem[4];
            DiffDataGrid.Items[5] = dataItem[5];
            DiffDataGrid.Items[6] = dataItem[6];

            DiffDataGrid.Items.Refresh();
        }

        /// <summary>
        /// Select info.dat, verify difficulty, read them.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if(filePath == "") // No file are selected yet
            {
                OpenFileDialog openFileDialog = new();
                openFileDialog.Filter = "Info.dat|Info.dat";
                openFileDialog.Title = "Open Info.dat";
                openFileDialog.InitialDirectory = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Beat Saber\\Beat Saber_Data";
                bool? result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    filePath = openFileDialog.FileName;        
                }
            }
            if(filePath != "") // A file is selected
            {
                string? path = Path.GetDirectoryName(filePath) + "\\";
                string? selectedFileName = Path.GetFileName(filePath);
                if(selectedFileName != null)
                {
                    if (selectedFileName.Equals("Info.dat", StringComparison.OrdinalIgnoreCase))
                    {
                        try // Read the Info.dat
                        {
                            using StreamReader r = new(path + selectedFileName);
                            {
                                while (r.Peek() != -1)
                                {
                                    string json = r.ReadToEnd();
                                    infoData = JsonSerializer.Deserialize<InfoData>(json);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("ERROR: Reading Info.dat");
                            infoData = null;
                        }

                        if(infoData != null)
                        {
                            DiffListBox.Items.Clear(); // Prepare the PathListBox to add all the difficulty inside

                            try
                            {
                                foreach (var difficulty in infoData._difficultyBeatmapSets)
                                {
                                    var type = difficulty._beatmapCharacteristicName;
                                    foreach (var beatmap in difficulty._difficultyBeatmaps)
                                    {
                                        if (File.Exists(path + beatmap._beatmapFilename))
                                        {
                                            using StreamReader r = new(path + beatmap._beatmapFilename);
                                            while (r.Peek() != -1)
                                            {
                                                string json = r.ReadToEnd();
                                                if(json.Contains("_version")) // Older version (probably 2.0.0)
                                                {
                                                    OldDifficultyData oldDiffData = JsonSerializer.Deserialize<OldDifficultyData>(json);
                                                    // Convert it to 3.0.0
                                                    difficultyData.Add(new(oldDiffData));
                                                }
                                                else // Version 3.0.0 beatmap
                                                {
                                                    difficultyData.Add(JsonSerializer.Deserialize<DifficultyData>(json));
                                                }
                                            }

                                            DiffListBox.Items.Add(beatmap._beatmapFilename);
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("ERROR: Reading difficulty");
                            }

                            if(difficultyData.Count > 0)
                            {
                                Transition();
                                FillDataGrid(0);
                            }
                            else
                            {
                                filePath = "";
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("ERROR: Info.dat not selected");
                    }
                }
            }
        }

        private void PathListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DiffDataGrid.Visibility == Visibility.Visible)
            {
                FillDataGrid(DiffListBox.SelectedIndex);
            }
        }

        /// <summary>
        /// Generate light for selected difficulty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Light_Click(object sender, RoutedEventArgs e)
        {
            List<float> timing = new();
            float offset = 0f;
            float colorSwap = 4f;
            bool allowBackLight = true;
            bool allowNeonLight = true;
            bool allowSideLight = true;
            bool allowFade = true;
            bool allowSpinZoom = true;
            bool nerfStrobes = false;
            bool applyToAll = false;
            bool boostLight = false;

            foreach(var note in difficultyData[DiffListBox.SelectedIndex].colorNotes)
            {
                timing.Add(note.beat);
            }

            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to apply this Lightshow to all the difficulty?", "Light", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                applyToAll = true;
            }

            messageBoxResult = MessageBox.Show("Do you want to use bomb to light too?", "Light", MessageBoxButton.YesNo);
            if(messageBoxResult == MessageBoxResult.Yes)
            {
                foreach (var bomb in difficultyData[DiffListBox.SelectedIndex].bombNotes)
                {
                    timing.Add(bomb.beat);
                }
                timing.Sort();
            }

            messageBoxResult = MessageBox.Show("Do you want to reduce strobes?", "Light", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                nerfStrobes = true;
            }

            messageBoxResult = MessageBox.Show("Do you want to use Boost Light?", "Light", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                boostLight = true;
            }

            List<ColorBoostEventData> boostEvents = new();
            List<BasicEventData> basicEvents = new();

            (boostEvents, basicEvents) = Light.CreateLight(timing, offset, colorSwap, allowBackLight, allowNeonLight, allowSideLight, allowFade, allowSpinZoom, nerfStrobes, boostLight);

            difficultyData[DiffListBox.SelectedIndex].colorBoostBeatmapEvents = boostEvents.ToArray();
            difficultyData[DiffListBox.SelectedIndex].basicBeatmapEvents = basicEvents.ToArray();

            if (applyToAll)
            {
                foreach(var difficulty in difficultyData)
                {
                    difficulty.basicBeatmapEvents = difficultyData[DiffListBox.SelectedIndex].basicBeatmapEvents;
                    if(boostLight)
                    {
                        difficulty.colorBoostBeatmapEvents = difficultyData[DiffListBox.SelectedIndex].colorBoostBeatmapEvents;
                    }
                }
            }
            
            FillDataGrid(DiffListBox.SelectedIndex);
        }

        /// <summary>
        /// Turn all long strobes into pulse, remove fast off, set on backlight during long period, remove spam
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Downlight_Click(object sender, RoutedEventArgs e)
        {
            bool applyToAll = false;

            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to apply Downlight to all the difficulty?", "Light", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                applyToAll = true;
            } 

            if (applyToAll)
            {
                foreach (var difficulty in difficultyData)
                {
                    difficulty.basicBeatmapEvents = DownLight.Down(difficulty.basicBeatmapEvents.ToList(), 0.5, 0.25, 5).ToArray();
                }
            }
            else
            {
                difficultyData[DiffListBox.SelectedIndex].basicBeatmapEvents = DownLight.Down(difficultyData[DiffListBox.SelectedIndex].basicBeatmapEvents.ToList(), 0.5, 0.25, 5).ToArray();
            }

            FillDataGrid(DiffListBox.SelectedIndex);
        }

        /// <summary>
        /// Save difficulty and the Info.dat file in ProgramData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var systemPath = Environment.
                             GetFolderPath(
                                 Environment.SpecialFolder.CommonApplicationData
                             );
                var complete = Path.Combine(systemPath, "Lolighter");

                Directory.CreateDirectory(complete);

                for (int i = 0; i < DiffListBox.Items.Count; i++)
                {
                    #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                    string fileName = DiffListBox.Items[i].ToString();
                    #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                    using (StreamWriter wr = new StreamWriter(complete + "\\" + fileName))
                    {
                        wr.WriteLine(JsonSerializer.Serialize<DifficultyData>(difficultyData[i]));
                    }
                }

                using (StreamWriter wr = new StreamWriter(complete + "\\" + "Info.dat"))
                {
                    wr.WriteLine(JsonSerializer.Serialize<InfoData>(infoData));
                }

                MessageBox.Show("Done");

                ProcessStartInfo dir = new ProcessStartInfo
                {
                    Arguments = complete,
                    FileName = "explorer.exe"
                };

                Process.Start(dir);

                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ShrinkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExpandButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SliderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void WindowButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void InvertButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BombButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoloppeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AutomapperButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DownmapButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
