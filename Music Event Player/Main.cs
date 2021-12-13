using GTA;
using GTA.UI;
using GTA.Native;
using Newtonsoft.Json;
using LemonUI;
using LemonUI.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Music_Event_Player
{
    public class Main : Script
    {
        string[] eventListOutput = { };
        string[] eventStopListOutput = { };
        string selItemString;
        int underScoreIndex;
        string subString;
        string currentStopListItem;

        private static readonly ObjectPool mainPool = new ObjectPool();
        private static readonly NativeMenu mainMenu = new NativeMenu("Music Event Player")
        {
            ItemCount = CountVisibility.Always
        };
        private readonly NativeMenu subMenu = new NativeMenu("Music Event Player", "Play Music Events");
        private readonly NativeItem currentItem;
        private readonly NativeItem stopEvent = new NativeItem("Stop Music Event");
        private readonly List<NativeItem> musicEventListItems = new List<NativeItem>();
        private NativeItem selItem;
        public Main()
        {
            Tick += OnTick;
            KeyDown += OnKeyDown;
            stopEvent.Activated += EventDeactivated;

            mainPool.Add(mainMenu);
            mainPool.Add(subMenu);
            mainMenu.AddSubMenu(subMenu);
            mainMenu.Add(stopEvent);

            using (StreamReader reader = new StreamReader("scripts\\musicEventNames.json"))
            {
                string mainJson = reader.ReadToEnd();
                eventListOutput = JsonConvert.DeserializeObject<string[]>(mainJson);
            }
            using (StreamReader reader = new StreamReader("scripts\\musicStopEventNames.json"))
            {
                string stopJson = reader.ReadToEnd();
                eventStopListOutput = JsonConvert.DeserializeObject<string[]>(stopJson);
            }
            for (int i = 0; i < eventListOutput.Length; i++)
            {
                currentItem = new NativeItem(eventListOutput[i]);
                musicEventListItems.Add(currentItem);
            }
            for (int i = 0; i < musicEventListItems.Count; i++)
            {
                subMenu.Add(musicEventListItems[i]);
            }
        }
        private void OnTick(object sender, EventArgs e)
        {
            mainPool.Process();
        }
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (subMenu.Visible)
                {
                    EventActivated();
                }
            }
            if (e.KeyCode == Keys.F3)
            {
                if (!mainMenu.Visible && !subMenu.Visible)
                {
                    mainMenu.Visible = true;
                }
            }
        }
        private void EventActivated()
        {
            selItem = subMenu.SelectedItem;
            selItemString = selItem.Title;
            Notification.Show(("~g~Currently Playing: " + selItemString));
            Function.Call(Hash.PREPARE_MUSIC_EVENT, selItemString);
            Function.Call(Hash.TRIGGER_MUSIC_EVENT, selItemString);
        }
        private void EventDeactivated(object sender, EventArgs e)
        {
            if (selItemString != null)
            {
                for (int i = 0; i < selItemString.Length; i++)
                {
                    if (selItemString[i].Equals("_"))
                    {
                        underScoreIndex = selItemString[i];
                        break;
                    }
                }
                subString = selItemString.Remove(underScoreIndex);
                for (int i = 0; i < eventStopListOutput.Length; i++)
                {
                    currentStopListItem = eventStopListOutput[i];
                    if (currentStopListItem.Contains(subString))
                    {
                        Notification.Show(("~o~Stopping: " + selItemString));
                        Function.Call(Hash.PREPARE_MUSIC_EVENT, currentStopListItem);
                        Function.Call(Hash.TRIGGER_MUSIC_EVENT, currentStopListItem);
                    }
                }
            }
        }
    }
}
