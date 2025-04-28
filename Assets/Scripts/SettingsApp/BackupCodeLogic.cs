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

        private int inputCode;
        private string fileName;
        private string textToSave;
        private GameManager gm;

        // --------------------------------------------------------------------

        private void Awake()
        {
            gm = GameManager.Instance;
            if (gm == null)
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

            if (inputCode == backupCode)
            {
                //selfieGameObject.SetActive(true); //TODO: Selfies are broken
                popUp.SetActive(true);
                SetFileNameAndText();
                SaveTextFileToDevice.SaveTextFile(fileName, textToSave);
                

                // lift the friction once the truth is revealed
                gm.rotationFriction = false;
                gm.birdFriction = false;
                gm.wasShaken = false;
                
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
                inputCode = codeInt;
                Debug.Log("Parsed value: " + inputCode);
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
            fileName = "BACKUP.txt";
            textToSave = BuildBackupText();
        }

        // --------------------------------------------------------------------
        //  Generates the complete backup-log reveal
        // --------------------------------------------------------------------
        private string BuildBackupText()
        {
            var sb = new StringBuilder();
            var today = DateTime.Today;

            sb.AppendLine("//—BEGIN_BACKUP_RECONSTRUCTION—//");
            sb.AppendLine($"OWNER          : {gm.playerName}");
            sb.AppendLine($"AGE / YOB      : {gm.playerAge} / {gm.playerBirthYear}");
            sb.AppendLine($"LEVEL REACHED  : {gm.currentLevel}");
            sb.AppendLine($"LAST SCENE     : {gm.currentScene}");
            sb.AppendLine($"BIRD HI-SCORE  : {gm.birdHighScore}");
            sb.AppendLine();
            sb.AppendLine("— DEVICE —");
            sb.AppendLine($"MODEL          : {SystemInfo.deviceModel}");
            sb.AppendLine($"OS VERSION     : {SystemInfo.operatingSystem}");
            sb.AppendLine(
                $"BATTERY LEVEL  : {(SystemInfo.batteryLevel >= 0 ? (SystemInfo.batteryLevel * 100).ToString("F0", System.Globalization.CultureInfo.InvariantCulture) + "%" : "N/A")}");
            sb.AppendLine();
            sb.AppendLine("— HABIT —");
            sb.AppendLine($"SMOKER         : {gm.playerSmokes}  ({gm.playerCigarettesPerDay}/day)");
            sb.AppendLine($"DRINKER        : {gm.playerDrinks}  ({gm.playerDrinksPerWeek}/week)");
            sb.AppendLine($"EXERCISE/WEEK  : {gm.playerExerciseSessionsPerWeek}");
            sb.AppendLine($"SLEEP (h/night): {gm.playerSleepHours}");
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
                            $"{date}  {gm.playerCigarettesPerDay} cigarettes logged. Paris texts: 'City still burning?'.");
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
                $"{today.AddDays(-35).ToString("yyyy-MM-dd")}  Sandra: \"hey {gm.playerName}, build uploaded. don't judge the placeholder art.\"");
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
                $"{today.AddDays(-21).ToString("yyyy-MM-dd")}  Sandra: \"i wish. that's on you {gm.playerName}.\"");
            sb.AppendLine(
                $"{today.AddDays(-14).ToString("yyyy-MM-dd")}  Sandra: \"did you back up? please tell me you did.\"");
            sb.AppendLine($"{today.AddDays(-13).ToString("yyyy-MM-dd")}  You   : \"why?\"");
            sb.AppendLine(
                $"{today.AddDays(-8).ToString("yyyy-MM-dd")}   You   : \"getting weird push alerts. says 'remember'. yours?\"");
            sb.AppendLine($"{today.AddDays(-8).ToString("yyyy-MM-dd")}   Sandra: \"nope.\"");
            sb.AppendLine();
            sb.AppendLine("— PARIS THREAD —");
            sb.AppendLine($"{today.AddDays(-19).ToString("yyyy-MM-dd")}  Paris : \"hey {gm.playerName}, you alive?\"");
            sb.AppendLine($"{today.AddDays(-18).ToString("yyyy-MM-dd")}  You   : \"busy.\"");
            sb.AppendLine(
                $"{today.AddDays(-11).ToString("yyyy-MM-dd")}  Paris : \"heard about the reset. bad idea {gm.playerName}.\"");
            sb.AppendLine($"{today.AddDays(-2).ToString("yyyy-MM-dd")}   Paris : \"look under 'Files'. not kidding.\"");
            // ----------------------  USAGE PATTERNS  -------------------------
            int totalAppOpens = gm.appCounts.Sum();
            int totalPhotoViews = gm.photoCounts.Sum();

            sb.AppendLine();
            sb.AppendLine("— USAGE PATTERNS —");
            sb.AppendLine($"TOTAL APP OPENS  : {totalAppOpens}");
            sb.AppendLine($"TOTAL PHOTO VIEWS: {totalPhotoViews}");
            sb.AppendLine();
            sb.AppendLine("   APP        ▸ opens");
            for (int i = 0; i < gm.appNames.Count; i++)
            {
                if (i < gm.appCounts.Count)
                    sb.AppendLine($"{gm.appNames[i],-12}: {gm.appCounts[i]}");
            }

            sb.AppendLine();
            sb.AppendLine("   PHOTO      ▸ views");
            for (int i = 0; i < gm.photoNames.Count; i++)
            {
                if (i < gm.photoCounts.Count && gm.photoCounts[i] > 0)
                    sb.AppendLine($"{gm.photoNames[i],-12}: {gm.photoCounts[i]}");
            }

            // ----------------------------------------------------------------
            sb.AppendLine();
            sb.AppendLine("MEMORY_OVERRIDE : COMPLETE");
            sb.AppendLine($"// 1184 — fall of Troy. {gm.playerName} chose the sack.");
            sb.AppendLine("//—END_BACKUP_RECONSTRUCTION_#3—//");

            return sb.ToString();
        }
    }
}