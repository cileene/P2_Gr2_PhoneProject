using UnityEngine;
using TMPro;
using System;
using System.Text;
using System.Linq;
using UnityEngine.UI;

namespace SettingsApp
{
    public class BackupCodeLogic : MonoBehaviour
    {
        [SerializeField] private TMP_InputField codeInputField;
        [SerializeField] private GameObject popUp;
        [SerializeField] private GameObject selfieGameObject;
        [SerializeField] private Button viewButton;
        [SerializeField] private int backupCode = 1184; // The fall of Troy

        private int _inputCode;
        private string _fileName;
        private string _textToSave;
        private GameManager _gm;

        // --------------------------------------------------------------------

        private void Awake()
        {
            _gm = GameManager.Instance;
            if (_gm == null)
            {
                Debug.LogError("BackupCodeLogic: GameManager instance not found.");
                return;
            }
        }

        private void Start()
        {
            viewButton.onClick.AddListener(OnDebugPickButton);
        }

        public void CheckCode()
        {
            if (!ReadValue()) return; // parse input

            if (popUp == null)
            {
                Debug.LogWarning("BackupCodeLogic: popUp reference not assigned.");
                return;
            }

            if (_inputCode == backupCode)
            {
                //selfieGameObject.SetActive(true); //TODO: Selfies are broken
                popUp.SetActive(true);
                SetFileNameAndText();
                SaveTextFileToDevice.SaveTextFile(_fileName, _textToSave);
                

                // lift the friction once the truth is revealed
                _gm.rotationFriction = false;
                _gm.birdFriction = false;
                _gm.wasShaken = false;
                
                // Record analytics event
                Unity.Services.Analytics.AnalyticsService.Instance.RecordEvent("backupUnlocked");
            }
            else
            {
                Debug.Log("BackupCodeLogic: Code did not match.");
            }
        }

        // --------------------------------------------------------------------

        private bool ReadValue()
        {
            if (int.TryParse(codeInputField.text, out int codeInt))
            {
                _inputCode = codeInt;
                Debug.Log("Parsed value: " + _inputCode);
                return true;
            }

            Debug.LogWarning($"Could not parse '{codeInputField.text}' as an integer.");
            return false;
        }

        public void OnDebugPickButton()
        {
#if UNITY_IOS && !UNITY_EDITOR
        FilesBridge.Pick();
#endif
        }

        // Called from Swift
        void OnFilePicked(string path)
        {
            if (string.IsNullOrEmpty(path)) return; // user cancelled
            Debug.Log("Picked file: " + path);
#if UNITY_IOS && !UNITY_EDITOR
            FilesBridge.Preview(path);   // show Quick Look preview
#endif
        }

        // After you create your backup file:
        void ShareBackup(string fullPath)
        {
#if UNITY_IOS && !UNITY_EDITOR
        FilesBridge.Share(fullPath);
#endif
        }

        // --------------------------------------------------------------------

        private void SetFileNameAndText()
        {
            _fileName = "BACKUP.txt";
            _textToSave = BuildBackupText();
        }

        // --------------------------------------------------------------------
        //  Generates the complete backup-log reveal
        // --------------------------------------------------------------------
        private string BuildBackupText()
        {
            var sb = new StringBuilder();
            var today = DateTime.Today;

            sb.AppendLine("//—BEGIN_BACKUP_RECONSTRUCTION—//");
            sb.AppendLine($"OWNER          : {_gm.playerName}");
            sb.AppendLine($"AGE / YOB      : {_gm.playerAge} / {_gm.playerBirthYear}");
            sb.AppendLine($"LEVEL REACHED  : {_gm.currentLevel}");
            sb.AppendLine($"LAST SCENE     : {_gm.currentScene}");
            sb.AppendLine($"BIRD HI-SCORE  : {_gm.birdHighScore}");
            sb.AppendLine();
            sb.AppendLine("— DEVICE —");
            sb.AppendLine($"MODEL          : {SystemInfo.deviceModel}");
            sb.AppendLine($"OS VERSION     : {SystemInfo.operatingSystem}");
            sb.AppendLine(
                $"BATTERY LEVEL  : {(SystemInfo.batteryLevel >= 0 ? (SystemInfo.batteryLevel * 100).ToString("F0", System.Globalization.CultureInfo.InvariantCulture) + "%" : "N/A")}");
            sb.AppendLine();
            sb.AppendLine("— HABIT —");
            sb.AppendLine($"SMOKER         : {_gm.playerSmokes}  ({_gm.playerCigarettesPerDay}/day)");
            sb.AppendLine($"DRINKER        : {_gm.playerDrinks}  ({_gm.playerDrinksPerWeek}/week)");
            sb.AppendLine($"EXERCISE/WEEK  : {_gm.playerExerciseSessionsPerWeek}");
            sb.AppendLine($"SLEEP (h/night): {_gm.playerSleepHours}");
            sb.AppendLine();
            sb.AppendLine("— DIARY (-30→0 days) —");

            for (int d = 30; d >= 0; d--)
            {
                string date = today.AddDays(-d).ToString("yyyy-MM-dd");
                switch (d)
                {
                    case 28:
                        sb.AppendLine(
                            $"{date}  Sandra pinged: 'Prophecy ignored = prophecy fulfilled?'. Cassandra flag raised.");
                        break;
                    case 25:
                        sb.AppendLine($"{date}  Auto‑lock extended to 5 min. Friction tactic recorded.");
                        break;
                    case 20:
                        sb.AppendLine($"{date}  Paris sent location pin: 'Same rooftop?'. Ignored.");
                        break;
                    case 15:
                        sb.AppendLine($"{date}  Missed call from Paris. Voicemail mentions 'wooden package'.");
                        break;
                    case 10:
                        sb.AppendLine($"{date}  Memory optimisation triggered. 2.4 GB off‑loaded.");
                        break;
                    case 7:
                        sb.AppendLine(
                            $"{date}  {_gm.playerCigarettesPerDay} cigarettes logged. Paris texts: 'City still burning?'.");
                        break;
                    case 5:
                        sb.AppendLine($"{date}  System haptics disabled. Silence preferred.");
                        break;
                    case 3:
                        sb.AppendLine($"{date}  Sandra: 'You can still undo this.' Message left unread.");
                        break;
                    case 0:
                        sb.AppendLine(
                            $"{date}  Backup reconstruction complete — if you're reading this, memory override failed.");
                        break;
                }
            }

            // ----------------------  CONVERSATION EXCERPTS  -----------------
            sb.AppendLine();
            sb.AppendLine("— CONVERSATION EXCERPTS —");
            sb.AppendLine(
                $"{today.AddDays(-35).ToString("yyyy-MM-dd")}  Sandra: \"hey {_gm.playerName}, build uploaded. don't judge the placeholder art.\"");
            sb.AppendLine($"{today.AddDays(-35).ToString("yyyy-MM-dd")}  You   : \"placeholders are my jam.\"");
            sb.AppendLine(
                $"{today.AddDays(-34).ToString("yyyy-MM-dd")}  Sandra: \"promise you'll actually play it this time?\"");
            sb.AppendLine($"{today.AddDays(-34).ToString("yyyy-MM-dd")}  You   : \"scout's honor.\"");
            sb.AppendLine(
                $"{today.AddDays(-33).ToString("yyyy-MM-dd")}  Sandra: \"cool. it's short. just, be honest.\"");
            sb.AppendLine($"{today.AddDays(-33).ToString("yyyy-MM-dd")}  You   : \"i'll find all the endings.\"");
            sb.AppendLine($"{today.AddDays(-32).ToString("yyyy-MM-dd")}  Sandra: \"lol. i'll patch that.\"");
            sb.AppendLine($"{today.AddDays(-31).ToString("yyyy-MM-dd")}  Sandra: \"any feedback yet?\"");
            sb.AppendLine(
                $"{today.AddDays(-31).ToString("yyyy-MM-dd")}  You   : \"still at the start. looking for secret code.\"");
            sb.AppendLine(
                $"{today.AddDays(-31).ToString("yyyy-MM-dd")}  Sandra: \"there's no secret code. just choices.\"");
            sb.AppendLine($"{today.AddDays(-29).ToString("yyyy-MM-dd")}  You   : \"found easter egg. u troll.\"");
            sb.AppendLine($"{today.AddDays(-28).ToString("yyyy-MM-dd")}  Sandra: \"that's not even the big twist.\"");
            sb.AppendLine(
                $"{today.AddDays(-21).ToString("yyyy-MM-dd")}  You   : \"phone glitching, did you code that?\"");
            sb.AppendLine(
                $"{today.AddDays(-21).ToString("yyyy-MM-dd")}  Sandra: \"i wish. that's on you {_gm.playerName}.\"");
            sb.AppendLine(
                $"{today.AddDays(-14).ToString("yyyy-MM-dd")}  Sandra: \"did you back up? please tell me you did.\"");
            sb.AppendLine($"{today.AddDays(-13).ToString("yyyy-MM-dd")}  You   : \"why?\"");
            sb.AppendLine(
                $"{today.AddDays(-8).ToString("yyyy-MM-dd")}   You   : \"getting weird push alerts. says 'remember'. yours?\"");
            sb.AppendLine($"{today.AddDays(-8).ToString("yyyy-MM-dd")}   Sandra: \"nope.\"");
            sb.AppendLine();
            sb.AppendLine("— PARIS THREAD —");
            sb.AppendLine($"{today.AddDays(-19).ToString("yyyy-MM-dd")}  Paris : \"hey {_gm.playerName}, you alive?\"");
            sb.AppendLine($"{today.AddDays(-18).ToString("yyyy-MM-dd")}  You   : \"busy.\"");
            sb.AppendLine(
                $"{today.AddDays(-11).ToString("yyyy-MM-dd")}  Paris : \"heard about the reset. bad idea {_gm.playerName}.\"");
            sb.AppendLine($"{today.AddDays(-2).ToString("yyyy-MM-dd")}   Paris : \"look under 'Files'. not kidding.\"");
            // ----------------------  USAGE PATTERNS  -------------------------
            int totalAppOpens = _gm.appCounts.Sum();
            int totalPhotoViews = _gm.photoCounts.Sum();

            sb.AppendLine();
            sb.AppendLine("— USAGE PATTERNS —");
            sb.AppendLine($"TOTAL APP OPENS  : {totalAppOpens}");
            sb.AppendLine($"TOTAL PHOTO VIEWS: {totalPhotoViews}");
            sb.AppendLine();
            sb.AppendLine("   APP        ▸ opens");
            for (int i = 0; i < _gm.appNames.Count; i++)
            {
                if (i < _gm.appCounts.Count)
                    sb.AppendLine($"{_gm.appNames[i],-12}: {_gm.appCounts[i]}");
            }

            sb.AppendLine();
            sb.AppendLine("   PHOTO      ▸ views");
            for (int i = 0; i < _gm.photoNames.Count; i++)
            {
                if (i < _gm.photoCounts.Count && _gm.photoCounts[i] > 0)
                    sb.AppendLine($"{_gm.photoNames[i],-12}: {_gm.photoCounts[i]}");
            }

            // ----------------------------------------------------------------
            sb.AppendLine();
            sb.AppendLine("MEMORY_OVERRIDE : COMPLETE");
            sb.AppendLine($"// 1184 — fall of Troy. {_gm.playerName} chose the sack.");
            sb.AppendLine("//—END_BACKUP_RECONSTRUCTION_#3—//");

            return sb.ToString();
        }
    }
}