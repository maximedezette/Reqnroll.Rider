using System;
using System.IO;
using System.Reflection;
using JetBrains.Application;
using Newtonsoft.Json;

namespace ReSharperPlugin.SpecflowRiderPlugin.Analytics
{
    [ShellComponent]
    public class RiderInstallationStatusService : IRiderInstallationStatusService
    {
        private readonly IAnalyticsTransmitter _analyticsTransmitter;
        private RiderInstallationStatus _currentStatusData;
        private static readonly string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create);
        private static readonly string SpecFlowFolder = Path.Combine(AppDataFolder, "SpecFlow");
        private static readonly string SpecflowRiderPluginFilePath = Path.Combine(SpecFlowFolder, "specflowriderplugin.json");

        public RiderInstallationStatusService(IAnalyticsTransmitter analyticsTransmitter)
        {
            _analyticsTransmitter = analyticsTransmitter;
        }

        public RiderInstallationStatus GetRiderInstallationStatus()
        {
            var today = DateTime.Today;
            var currentPluginVersion = Assembly.GetExecutingAssembly().GetName().Version;

            if (_currentStatusData == null)
            {
                _currentStatusData = new RiderInstallationStatus()
                {
                    InstallDate = today,
                    InstalledVersion = currentPluginVersion.ToString(),
                    LastUsedDate = today,
                    UsageDays = 1
                };
                if (!File.Exists(SpecflowRiderPluginFilePath))
                {
                    _analyticsTransmitter.TransmitRuntimeEvent(new GenericEvent("Rider Extension installed"));
                    SaveNewStatus(_currentStatusData);
                }
                else
                {
                    try
                    {
                        _currentStatusData = JsonConvert.DeserializeObject<RiderInstallationStatus>(
                            File.ReadAllText(SpecflowRiderPluginFilePath));
                    }
                    catch
                    {
                        //NOP
                    }
                }
            }
            return _currentStatusData;
        }

        public void SaveNewStatus(RiderInstallationStatus newStatus)
        {
            try
            {
                _currentStatusData = newStatus;
                File.WriteAllText(SpecflowRiderPluginFilePath, JsonConvert.SerializeObject(_currentStatusData));
            }
            catch
            {
                //NOP
            }
        }
    }
}