﻿using Newtonsoft.Json;
using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGDBMetadata
{
    public class SGDBMetadataSettings : ISettings
    {
        private readonly SGDBMetadata plugin;

        public string ApiKey { get; set; } = string.Empty;
        public string SDimension { get; set; } = "600x900";
        public string SStyle { get; set; } = "alternate";
        public string SNsfw { get; set; } = "false";
        public string SHumor { get; set; } = "false";
        public string SIconAssetSelection { get; set; } = "icons";

        // Playnite serializes settings object to a JSON object and saves it as text file.
        // If you want to exclude some property from being saved then use `JsonIgnore` ignore attribute.
        [JsonIgnore]
        public bool OptionThatWontBeSaved { get; set; } = false;

        // Parameterless constructor must exist if you want to use LoadPluginSettings method.
        public SGDBMetadataSettings()
        {
        }

        public SGDBMetadataSettings(SGDBMetadata plugin)
        {
            // Injecting your plugin instance is required for Save/Load method because Playnite saves data to a location based on what plugin requested the operation.
            this.plugin = plugin;

            // Load saved settings.
            var savedSettings = plugin.LoadPluginSettings<SGDBMetadataSettings>();
            // LoadPluginSettings returns null if not saved data is available.
            if (savedSettings != null)
            {
                var logger = LogManager.GetLogger();
                logger.Info("saved settings");
                logger.Info(savedSettings.SDimension);
                logger.Info(savedSettings.SStyle);
                logger.Info(savedSettings.SNsfw);
                logger.Info(savedSettings.SHumor);
                logger.Info(savedSettings.SIconAssetSelection);
                ApiKey = savedSettings.ApiKey;
                SDimension = savedSettings.SDimension;
                SStyle = savedSettings.SStyle;
                SNsfw = savedSettings.SNsfw;
                SHumor = savedSettings.SHumor;
                SIconAssetSelection = savedSettings.SIconAssetSelection;
            }
        }

        public void BeginEdit()
        {
            // Code executed when settings view is opened and user starts editing values.
        }

        public void CancelEdit()
        {
            // Code executed when user decides to cancel any changes made since BeginEdit was called.
            // This method should revert any changes made to Option1 and Option2.
        }

        public void EndEdit()
        {
            // Code executed when user decides to confirm changes made since BeginEdit was called.
            // This method should save settings made to Option1 and Option2.
            plugin.SavePluginSettings(this);
        }

        public bool VerifySettings(out List<string> errors)
        {
            // Code execute when user decides to confirm changes made since BeginEdit was called.
            // Executed before EndEdit is called and EndEdit is not called if false is returned.
            // List of errors is presented to user if verification fails.
            errors = new List<string>();
            return true;
        }
    }
}
