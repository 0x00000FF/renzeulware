using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace renzeulware
{
    public partial class RenZeulMain : Form
    {
        Settings ScoreSettings;
        RegistryKey Wallpaper;
        string OriginalWallpaperPath;

        const uint SET_WALLPAPER = 0x0014;
        const uint UPDATE = 0x03;

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        [DllImport("user32.dll")]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

        public RenZeulMain()
        {
            InitializeComponent();

            if (File.Exists("rensen.json"))
            {
                var settingDeserializer = new JavaScriptSerializer();
                ScoreSettings = settingDeserializer.Deserialize<Settings>(
                    File.ReadAllText("rensen.json")
                    );
            }
            else
            {
                ScoreSettings = new Settings()
                {
                    easy = 10000000,
                    normal = 10000000,
                    hard = 15000000,
                    lunatic = 20000000,
                    extra = 20000000
                };
            }

            Wallpaper = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", true);
            OriginalWallpaperPath = Wallpaper.GetValue("WallPaper") as string;

            try
            {
                File.Copy(OriginalWallpaperPath, $"{Application.StartupPath}", true);
            }
            catch { }

            new Thread(new ThreadStart(RensenLoop)).Start();
        }

        private void RensenLoop()
        {
            Process process = null;
            IntPtr processHandle = IntPtr.Zero;
            byte[] buffer = new byte[4];
            int readbuff = 0, readsize = 0, currLevel = 0, currStage = 0, goal = 0;
            bool processStatus = false;

            while (true)
            {
                while (!processStatus)
                {
                    var processTemp = Process.GetProcessesByName("th12");
                    if (processTemp.Length > 0)
                    {
                        process = processTemp[0];
                        processHandle = process.Handle;
                        processStatus = true;
                    }

                    readsize = -1;
                    CrossCall(Level, () =>
                    {
                        Level.Text = "련선 켜주세려 @~@";
                    });
                }

                if (!ReadProcessMemory(processHandle.ToInt32(), 0x4AEBD0, buffer, 4, ref readbuff))
                {
                    processStatus = false;
                    continue;
                }

                readsize = BitConverter.ToInt32(buffer, 0);
                if (readsize != currLevel)
                {
                    currLevel = readsize;
                    SetGoal(currLevel, ref goal);
                }

                if (!ReadProcessMemory(processHandle.ToInt32(), 0x4B0C44, buffer, 4, ref readbuff))
                {
                    processStatus = false;
                    continue;
                }

                readsize = BitConverter.ToInt32(buffer, 0);
                CrossCall(this, () =>
                {
                    if (readsize <= VisualProgress.Maximum)
                        VisualProgress.Value = readsize;
                    else
                        VisualProgress.Value = VisualProgress.Maximum;

                    Progress.Text = $"{readsize * 10} / {goal}";
                });

                if (readsize >= goal)
                {
                    var question = MessageBox.Show("련선 즐겁지요?", "련즐지?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (question == DialogResult.No)
                    {
                        MessageBox.Show("그렇다면 한번 더!", "@.@", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var path = process.MainModule.FileName;
                        processStatus = false;
                        process.Kill();

                        currLevel = 0;

                        process = new Process
                        {
                            StartInfo = new ProcessStartInfo()
                            {
                                FileName = path,
                                WorkingDirectory = Path.GetDirectoryName(path)
                            }
                        };
                        process.Start();

                        continue;
                    }

                    MessageBox.Show("수고했어려 ^ㅅ^", "련즐지!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetWallpaper(OriginalWallpaperPath);
                    CrossCall(Level, () =>
                    {
                        Level.Text += " CLEAR!";
                    });
                    break;
                }

                if (!ReadProcessMemory(processHandle.ToInt32(), 0x4B0CB0, buffer, 4, ref readbuff))
                {
                    processStatus = false;
                    continue;
                }
                readsize = BitConverter.ToInt32(buffer, 0);
                if (readsize != currStage)
                {
                    currStage = readsize;
                    if (readsize != 0)
                        SetWallpaper($"{Application.StartupPath}/images/{currStage}.jpg");
                }

                Thread.Sleep(150);
            }
        }

        public void SetWallpaper(string wallpaperPath)
        {
            Wallpaper.SetValue("WallPaper", wallpaperPath);
            SystemParametersInfo(SET_WALLPAPER, 0, wallpaperPath, UPDATE);
        }

        public void SetGoal(int level, ref int goal)
        {
            switch (level)
            {
                case 0:
                    goal = ScoreSettings.easy;
                    CrossCall(this, () =>
                    {
                        VisualProgress.Maximum = ScoreSettings.easy;
                        Level.Text = "EASY";
                    });
                    
                    break;
                case 1:
                    goal = ScoreSettings.normal;
                    CrossCall(this, () =>
                    {
                        VisualProgress.Maximum = ScoreSettings.normal;
                        Level.Text = "NORMAL";
                    });
                    break;
                case 2:
                    goal = ScoreSettings.hard;
                    CrossCall(this, () =>
                    {
                        VisualProgress.Maximum = ScoreSettings.hard;
                        Level.Text = "HARD";
                    });
                    break;
                case 3:
                    goal = ScoreSettings.lunatic;
                    CrossCall(this, () =>
                    {
                        VisualProgress.Maximum = ScoreSettings.lunatic;
                        Level.Text = "LUNATIC";
                    });
                    break;
                default:
                    goal = ScoreSettings.extra;
                    CrossCall(this, () =>
                    {
                        VisualProgress.Maximum = ScoreSettings.extra;
                        Level.Text = "EXTRA";
                    });
                    break;
            }
        }

        public void CrossCall(Control control, Action callback)
        {
            try
            {
                if (control.InvokeRequired)
                {
                    control.Invoke(new MethodInvoker(callback));
                }
                else
                {
                    callback();
                }
            }
            catch { Environment.Exit(-1); }
        }
    }

    public class Settings
{
        public int easy { get; set; }
        public int normal { get; set; }
        public int hard { get; set; }
        public int lunatic { get; set; }
        public int extra { get; set; }
    }

}
