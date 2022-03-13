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
using Microsoft.VisualBasic;
using Lolighter.Info;

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
        internal InfoData infoData = new();
        internal List<DifficultyData> difficultyData = new();
        // Information on the difficulty
        internal List<DataItem> dataItem = new();
        internal List<List<string>> oldData = new();

        public MainWindow()
        {
            InitializeComponent();

            options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            // Start
            OpenButton.Visibility = Visibility.Visible;
            OpenAudio.Visibility = Visibility.Visible;

            // Prefill DataGrid
            for (int i = 1; i <= 3; ++i)
            {
                var column = new DataGridTextColumn();
                switch (i)
                {
                    case 1: column.Header = "Type";
                        break;
                    case 2: column.Header = "Before";
                        break;
                    case 3: column.Header = "Now";
                        break;
                }
                column.Binding = new Binding("Column" + i);
                DiffDataGrid.Columns.Add(column);
            }

            dataItem.Add(new DataItem { Column1 = "Note", Column2 = "", Column3 = "" });
            dataItem.Add(new DataItem { Column1 = "Bomb", Column2 = "", Column3 = "" });
            dataItem.Add(new DataItem { Column1 = "Obstacle", Column2 = "", Column3 = "" });
            dataItem.Add(new DataItem { Column1 = "Chain", Column2 = "", Column3 = "" });
            dataItem.Add(new DataItem { Column1 = "Arc", Column2 = "", Column3 = "" });
            dataItem.Add(new DataItem { Column1 = "Light", Column2 = "", Column3 = "" });
            dataItem.Add(new DataItem { Column1 = "Boost", Column2 = "", Column3 = "" });

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
            public string? Column3 { get; set; }
        }

        /// <summary>
        /// Used when files are loaded to show new layout on the window
        /// </summary>
        public void Transition()
        {
            OpenButton.Visibility = Visibility.Collapsed;
            OpenAudio.Visibility = Visibility.Collapsed;
            DiffListBox.Visibility = Visibility.Visible;
            DiffListBox.Width = 150;
            DiffListBox.SelectedIndex = 0;
            // Filling the data to show the user
            DiffListBox.SelectionMode = SelectionMode.Single;
            WindowSize.Width = 600;
            LightButton.Visibility = Visibility.Visible;
            DownLightButton.Visibility = Visibility.Visible;
            SaveButton.Visibility = Visibility.Visible;
            AutomapperButton.Visibility = Visibility.Visible;
            BombButton.Visibility = Visibility.Visible;
            ArcButton.Visibility = Visibility.Visible;
            InvertButton.Visibility = Visibility.Visible;
            LoloppeButton.Visibility = Visibility.Visible;
            ChainButton.Visibility = Visibility.Visible;
            DDButton.Visibility = Visibility.Visible;
            // Show data
            DiffDataGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Fill out the InfoDataGrid with currently selected difficulty
        /// </summary>
        /// <param name="Index">Difficulty</param>
        public void FillDataGrid(int Index)
        {
            List<string> temp = oldData[Index];

            dataItem[0].Column2 = temp[0];
            dataItem[1].Column2 = temp[1];
            dataItem[2].Column2 = temp[2];
            dataItem[3].Column2 = temp[3];
            dataItem[4].Column2 = temp[4];
            dataItem[5].Column2 = temp[5];
            dataItem[6].Column2 = temp[6];

            dataItem[0].Column3 = difficultyData[Index].colorNotes.Count.ToString();
            dataItem[1].Column3 = difficultyData[Index].bombNotes.Count.ToString();
            dataItem[2].Column3 = difficultyData[Index].obstacles.Count.ToString();
            dataItem[3].Column3 = difficultyData[Index].burstSliders.Count.ToString();
            dataItem[4].Column3 = difficultyData[Index].sliders.Count.ToString();
            dataItem[5].Column3 = difficultyData[Index].basicBeatmapEvents.Count.ToString();
            dataItem[6].Column3 = difficultyData[Index].colorBoostBeatmapEvents.Count.ToString();

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
                            infoData = new();
                            filePath = "";
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
                                                    var test = JsonSerializer.Deserialize<DifficultyData>(json);
                                                    difficultyData.Add(test);
                                                }
                                            }

                                            DiffListBox.Items.Add(beatmap._beatmapFilename);

                                            List<string> temp = new();

                                            temp.Add(difficultyData.Last().colorNotes.Count.ToString());
                                            temp.Add(difficultyData.Last().bombNotes.Count.ToString());
                                            temp.Add(difficultyData.Last().obstacles.Count.ToString());
                                            temp.Add(difficultyData.Last().burstSliders.Count.ToString());
                                            temp.Add(difficultyData.Last().sliders.Count.ToString());
                                            temp.Add(difficultyData.Last().basicBeatmapEvents.Count.ToString());
                                            temp.Add(difficultyData.Last().colorBoostBeatmapEvents.Count.ToString());

                                            oldData.Add(temp);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("ERROR: Reading difficulty");
                                MessageBox.Show(ex.Message);
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
                        filePath = "";
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
            List<ColorNote> timing = new();
            bool nerfStrobes = false;
            bool applyToAll = false;
            bool boostLight = false;

            timing.AddRange(difficultyData[DiffListBox.SelectedIndex].colorNotes);

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
                    timing.Add(new(bomb));
                }
                timing.OrderBy(o => o.beat);
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

            List<ColorBoostEventData> boostEvents;
            List<BasicEventData> basicEvents;
            List<BurstSliderData> burstSliderData = new(difficultyData[DiffListBox.SelectedIndex].burstSliders);

            (boostEvents, basicEvents) = Light.CreateLight(timing, burstSliderData, nerfStrobes, boostLight);

            difficultyData[DiffListBox.SelectedIndex].colorBoostBeatmapEvents = boostEvents;
            difficultyData[DiffListBox.SelectedIndex].basicBeatmapEvents = basicEvents;

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

            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to apply Downlight to all the difficulty?", "Downlight", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                applyToAll = true;
            } 

            if (applyToAll)
            {
                foreach (var difficulty in difficultyData)
                {
                    difficulty.basicBeatmapEvents = DownLight.Down(difficulty.basicBeatmapEvents.ToList(), 0.5, 0.25, 5);
                }
            }
            else
            {
                difficultyData[DiffListBox.SelectedIndex].basicBeatmapEvents = DownLight.Down(difficultyData[DiffListBox.SelectedIndex].basicBeatmapEvents.ToList(), 0.5, 0.25, 5);
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
            bool newVersion = true;

            MessageBoxResult messageBoxResult = MessageBox.Show("Save the map for Beat Saber version above 1.20.0 (v3)?", "Version", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                newVersion = false;
            }

            try
            {
                var systemPath = Environment.
                             GetFolderPath(
                                 Environment.SpecialFolder.CommonApplicationData
                             );
                var complete = Path.Combine(systemPath, "Lolighter");

                Directory.CreateDirectory(complete);

                if(newVersion)
                {
                    for (int i = 0; i < DiffListBox.Items.Count; i++)
                    {
                        #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        string fileName = DiffListBox.Items[i].ToString();
                        #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                        using StreamWriter wr = new(complete + "\\" + fileName);
                        wr.WriteLine(JsonSerializer.Serialize<DifficultyData>(difficultyData[i]));
                    }

                    using (StreamWriter wr = new(complete + "\\" + "Info.dat"))
                    {
                        wr.WriteLine(JsonSerializer.Serialize<InfoData>(infoData));
                    }
                }
                else
                {
                    for (int i = 0; i < DiffListBox.Items.Count; i++)
                    {
                        #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                        string fileName = DiffListBox.Items[i].ToString();
                        #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                        difficultyData[i].version = "2.0.0";
                        using StreamWriter wr = new(complete + "\\" + fileName);
                        wr.WriteLine(JsonSerializer.Serialize<OldDifficultyData>(new(difficultyData[i])));
                    }

                    using (StreamWriter wr = new(complete + "\\" + "Info.dat"))
                    {
                        infoData._version = "2.0.0";
                        wr.WriteLine(JsonSerializer.Serialize<InfoData>(infoData));
                    }
                }

                MessageBox.Show("Done");

                ProcessStartInfo dir = new()
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

        private void InvertButton_Click(object sender, RoutedEventArgs e)
        {
            bool limiter = true;

            MessageBoxResult messageBoxResult = MessageBox.Show("Use the limiter? Default: Yes", "Invert", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                limiter = false;
            }

            difficultyData[DiffListBox.SelectedIndex].colorNotes = Invert.MakeInvert(difficultyData[DiffListBox.SelectedIndex].colorNotes, 0.25, limiter);
            FillDataGrid(DiffListBox.SelectedIndex);
        }

        private void BombButton_Click(object sender, RoutedEventArgs e)
        {
            difficultyData[DiffListBox.SelectedIndex].bombNotes.AddRange(Bomb.CreateBomb(difficultyData[DiffListBox.SelectedIndex].colorNotes));
            FillDataGrid(DiffListBox.SelectedIndex);
        }

        private void LoloppeButton_Click(object sender, RoutedEventArgs e)
        {
            difficultyData[DiffListBox.SelectedIndex].colorNotes = Loloppe.LoloppeGen(difficultyData[DiffListBox.SelectedIndex].colorNotes);
            FillDataGrid(DiffListBox.SelectedIndex);
        }

        private void AutomapperButton_Click(object sender, RoutedEventArgs e)
        {
            (difficultyData[DiffListBox.SelectedIndex].colorNotes, difficultyData[DiffListBox.SelectedIndex].burstSliders) = PatternCreator.Create(difficultyData[DiffListBox.SelectedIndex].colorNotes, "RandomStream", false, false, true, false);
            difficultyData[DiffListBox.SelectedIndex].obstacles = new();
            difficultyData[DiffListBox.SelectedIndex].bombNotes = new();
            FillDataGrid(DiffListBox.SelectedIndex);
        }

        private void DDButton_Click(object sender, RoutedEventArgs e)
        {
            bool limiter = true;
            bool removeSide = false;

            MessageBoxResult messageBoxResult = MessageBox.Show("Reduce the map by half? Default: Yes", "Limiter", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                limiter = false;
            }

            if(limiter)
            {
                messageBoxResult = MessageBox.Show("Consider side note as up (to remove)? Default: No", "Limiter", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    removeSide = true;
                }
            }

            List<ColorNote> colorNotes;
            List<BurstSliderData> burstSliders;

            (colorNotes, burstSliders) = DoubleDirectional.Emilia(difficultyData[DiffListBox.SelectedIndex].colorNotes, limiter, removeSide);
            difficultyData[DiffListBox.SelectedIndex].colorNotes = colorNotes;
            difficultyData[DiffListBox.SelectedIndex].burstSliders = burstSliders;
            FillDataGrid(DiffListBox.SelectedIndex);
        }

        private void ChainButton_Click(object sender, RoutedEventArgs e)
        {
            List<ColorNote> colorNotes;
            List<BurstSliderData> burstSliders;
            (burstSliders, colorNotes) = Chain.Chains(difficultyData[DiffListBox.SelectedIndex].colorNotes);
            difficultyData[DiffListBox.SelectedIndex].colorNotes = colorNotes;
            difficultyData[DiffListBox.SelectedIndex].burstSliders = burstSliders;
            FillDataGrid(DiffListBox.SelectedIndex);
        }

        private void ArcButton_Click(object sender, RoutedEventArgs e)
        {
            List<SliderData> arc;
            arc = Arc.CreateArc(difficultyData[DiffListBox.SelectedIndex].colorNotes);
            difficultyData[DiffListBox.SelectedIndex].sliders = arc;
            FillDataGrid(DiffListBox.SelectedIndex);
        }

        private void OpenAudio_Click(object sender, RoutedEventArgs e)
        {
            if (filePath == "") // No file are selected yet
            {
                OpenFileDialog openFileDialog = new();
                openFileDialog.Filter = "*.mp3|*.mp3";
                openFileDialog.Title = "Open audio";
                openFileDialog.InitialDirectory = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Beat Saber\\Beat Saber_Data";
                bool? result = openFileDialog.ShowDialog();
                if (result == true)
                {
                    filePath = openFileDialog.FileName;
                }
            }
            if (filePath != "") // A file is selected
            {
                var systemPath = Environment.
                            GetFolderPath(
                                Environment.SpecialFolder.CommonApplicationData
                            );
                var complete = Path.Combine(systemPath, "Lolighter");

                Directory.CreateDirectory(complete);

                File.Move(filePath, Path.GetDirectoryName(filePath) + "\\song.mp3");

                filePath = Path.GetDirectoryName(filePath) + "\\song.mp3";

                MP3toOGG.ConvertToOgg(filePath, complete);

                List<ColorNote> colorNotes = new();
                List<BurstSliderData> burstSliders;

                float bpm;

                List<float> indistinguishableRange = new();
                indistinguishableRange.Add(0.005f);     // Overmapped (a bit)
                indistinguishableRange.Add(0.003f);     // Very good
                indistinguishableRange.Add(0.0015f);    // Good

                BPMDetector detector = new(filePath);
                BPMGroup group = detector.Groups.Where(o => o.Count == detector.Groups.Max(o => o.Count)).First();
                bpm = group.Tempo;
                try
                {
                    bpm = float.Parse(Interaction.InputBox("Enter song BPM (if it's wrong)", "Automatic BPM detection", bpm.ToString()));
                }
                catch (Exception)
                {
                    bpm = group.Tempo;
                }

                for (int i = 0; i < indistinguishableRange.Count; i++)
                {
                    colorNotes = new();
                    burstSliders = new();

                    (colorNotes, burstSliders) = Onset.GetMap(filePath, bpm, indistinguishableRange[i]);

                    if(colorNotes.Count > 0)
                    {
                        // Create a new file
                        difficultyData.Add(new(colorNotes));
                        difficultyData[i].burstSliders = burstSliders;
                    }
                }


                List<DifficultyBeatmaps> btList = new();
                DifficultyBeatmaps difficultyBeatmaps = new("Hard", 5, "HardStandard.dat", 16, 0, new(0, 0, "Decent", null, null, null, null, null, null, null));
                btList.Add(difficultyBeatmaps);
                infoData._difficultyBeatmapSets.Add(new("Standard", btList));
                infoData._difficultyBeatmapSets[0]._difficultyBeatmaps.Add(new("Expert", 7, "ExpertStandard.dat", 18, 0, new(0, 0, "Good", null, null, null, null, null, null, null)));
                infoData._difficultyBeatmapSets[0]._difficultyBeatmaps.Add(new("ExpertPlus", 9, "ExpertPlusStandard.dat", 20, 0, new(0, 0, "Overmapped", null, null, null, null, null, null, null)));
                infoData._beatsPerMinute = bpm;

                DiffListBox.Items.Clear();

                for(int i = 0; i < difficultyData.Count(); i++)
                {
                    switch (i)
                    {
                        case 0: DiffListBox.Items.Add("ExpertPlusStandard.dat"); break;
                        case 1: DiffListBox.Items.Add("ExpertStandard.dat"); break;
                        case 2: DiffListBox.Items.Add("HardStandard.dat"); break;
                        case 3: DiffListBox.Items.Add("NormalStandard.dat"); break;
                        case 4: DiffListBox.Items.Add("EasyStandard.dat"); break;
                    }

                    List<string> temp = new();

                    temp.Add(difficultyData[i].colorNotes.Count.ToString());
                    temp.Add(difficultyData[i].bombNotes.Count.ToString());
                    temp.Add(difficultyData[i].obstacles.Count.ToString());
                    temp.Add(difficultyData[i].burstSliders.Count.ToString());
                    temp.Add(difficultyData[i].sliders.Count.ToString());
                    temp.Add(difficultyData[i].basicBeatmapEvents.Count.ToString());
                    temp.Add(difficultyData[i].colorBoostBeatmapEvents.Count.ToString());

                    oldData.Add(temp);
                }
                
                DiffListBox.SelectedIndex = 0;

                if (difficultyData.Count > 0)
                {
                    Transition();
                    FillDataGrid(0);
                }
            }
        }
    }
}
