using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Threading;
using System.Collections;

namespace KeafsMenuEdit
{
    public class Main : MelonLoader.MelonMod
    {
        public static Dictionary<string, Vector2> MenuNewAnchorPos = new Dictionary<string, Vector2>()
        {
            {
                "/UserInterface/QuickMenu/ShortcutMenu/NameText", new Vector2(340,-1555)
            },
            {
                "/UserInterface/QuickMenu/ShortcutMenu/FPSText", new Vector2(1530,-1555)
            },
            {
                "/UserInterface/QuickMenu/ShortcutMenu/BuildNumText", new Vector2(225,-1635)
            },
            {
                "/UserInterface/QuickMenu/ShortcutMenu/WorldText", new Vector2(780,-1635)
            },
            {
                "/UserInterface/QuickMenu/ShortcutMenu/PingText", new Vector2(1530,-1635)
            },
            {
                "/UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar", new Vector2(110,-755)
            },
            {
                "/UserInterface/QuickMenu/QuickMenu_NewElements/_Background/Panel", new Vector2(110,85)
            }
        };
        public static Dictionary<string, Vector2> MenuNewSizeDelta = new Dictionary<string, Vector2>
        {
            {
                "/UserInterface/QuickMenu/ShortcutMenu/NameText", new Vector2(675, 85)
            },
            {
                "/UserInterface/QuickMenu/ShortcutMenu/PingText", new Vector2(230, 85)
            },
            {
                "/UserInterface/QuickMenu/QuickMenu_NewElements/_InfoBar", new Vector2(1680,160)
            },
            {
                "/UserInterface/QuickMenu/QuickMenu_NewElements/_Background/Panel", new Vector2(1580,1430)
            }
        };
        List<string> FirstRowObj = new List<string>
        {
            "/UserInterface/QuickMenu/ShortcutMenu/WorldsButton",
            "/UserInterface/QuickMenu/ShortcutMenu/AvatarButton",
            "/UserInterface/QuickMenu/ShortcutMenu/SocialButton",
            "/UserInterface/QuickMenu/ShortcutMenu/SafetyButton"
        };
        List<string> SecondRowObj = new List<string>
        {
            "/UserInterface/QuickMenu/ShortcutMenu/GoHomeButton",
            "/UserInterface/QuickMenu/ShortcutMenu/RespawnButton",
            "/UserInterface/QuickMenu/ShortcutMenu/SettingsButton",
            "/UserInterface/QuickMenu/ShortcutMenu/ReportWorldButton"
        };
        List<string> ThirdRowObj = new List<string>
        {
            "/UserInterface/QuickMenu/ShortcutMenu/UIElementsButton",
            "/UserInterface/QuickMenu/ShortcutMenu/CameraButton",
            "/UserInterface/QuickMenu/ShortcutMenu/EmoteButton",
            "/UserInterface/QuickMenu/ShortcutMenu/EmojiButton"
        };
        Vector2 ButtonSize = new Vector2(420, 145);
        Vector2 ButtonSwapLoc = new Vector2(1890, -628);
        AssetBundle ass;
        GameObject ConsoleObj = new GameObject();
        public override void OnApplicationStart()
        {
            ass = AssetBundle.LoadFromFile("keafy");
            if (ass == null)
            {
                MelonLoader.MelonModLogger.LogError("fek mission failed");
                return;
            }
        }
        public override void VRChat_OnUiManagerInit()
        {
            //allow other peoples mod to copy buttons before we do are edits to them
            MelonLoader.MelonCoroutines.Start(menupatch());
        }
        public IEnumerator menupatch()
        {
            yield return new WaitForSeconds(1);
            Thread.Sleep(1000);
            #region big boi edits
            UnityEngine.Object.Destroy(GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/EarlyAccessText"));
            foreach (KeyValuePair<string, Vector2> a in MenuNewAnchorPos)
                GameObject.Find(a.Key).GetComponent<RectTransform>().anchoredPosition = a.Value;
            foreach (KeyValuePair<string, Vector2> b in MenuNewSizeDelta)
                GameObject.Find(b.Key).GetComponent<RectTransform>().sizeDelta = b.Value;
            foreach (string obj in FirstRowObj)
            {
                GameObject MenuButton = GameObject.Find(obj);
                MenuButton.GetComponent<RectTransform>().sizeDelta = ButtonSize;
                MenuButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(MenuButton.GetComponent<RectTransform>().anchoredPosition.x, -70);
            }
            foreach (var obj in SecondRowObj)
            {
                GameObject MenuButton = GameObject.Find(obj);
                MenuButton.GetComponent<RectTransform>().sizeDelta = ButtonSize;
                if (obj.Contains("ReportWorldButton"))
                    MenuButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(1050, -220);
                else
                    MenuButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(MenuButton.GetComponent<RectTransform>().anchoredPosition.x, -220);
            }
            foreach (string obj in ThirdRowObj)
            {
                GameObject MenuButton = GameObject.Find(obj);
                MenuButton.GetComponent<RectTransform>().sizeDelta = ButtonSize;
                MenuButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(MenuButton.GetComponent<RectTransform>().anchoredPosition.x, -375);
            }
            #endregion

            #region small edits
            GameObject.Destroy(GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/ReportWorldButton/CLIcon"));
            GameObject SitButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/SitButton");
            GameObject CalibrateButton = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu/CalibrateButton");
            SitButton.GetComponent<RectTransform>().anchoredPosition = ButtonSwapLoc;
            CalibrateButton.GetComponent<RectTransform>().anchoredPosition = ButtonSwapLoc;
            #endregion

            #region Add Console
            try
            {
                GameObject ShortcutNigger = GameObject.Find("/UserInterface/QuickMenu/ShortcutMenu");
                ConsoleObj = GameObject.Instantiate(ass.LoadAssetAsync("Console", GameObject.Il2CppType).asset.Cast<GameObject>(), ShortcutNigger.transform);
                ConsoleObj.GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
                //ConsoleObj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                //ConsoleObj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                ConsoleObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -150);
                //ConsoleObj.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                //ConsoleObj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                ConsoleObj.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
                ConsoleObj.transform.FindChild("Screen").GetComponent<RectTransform>().sizeDelta = new Vector2(1280, 670);
                ConsoleObj.transform.FindChild("Screen/ScrollRect").GetComponent<RectTransform>().sizeDelta = new Vector2(1260, 550);
                ConsoleObj.transform.FindChild("Screen/ScrollRect").GetComponent<RectTransform>().localPosition = new Vector3(-10, -20);
                ConsoleObj.transform.FindChild("Screen/ScrollRect").GetComponent<RectTransform>().anchoredPosition = new Vector3(-10, -20);
                ConsoleObj.transform.FindChild("Input").gameObject.SetActive(false);
                ConsoleObj.transform.FindChild("Screen/ScrollRect/Text").GetComponent<RectTransform>().sizeDelta = new Vector2(1200, 30);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            #endregion
        }
        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                ConsoleObj.transform.FindChild("Screen/ScrollRect/Text").GetComponent<Text>().text += Environment.NewLine+"<color=green>[</color><color=red>KeafuMod</color><color=green>]</color> TestLog";
            }
        }
    }
}
