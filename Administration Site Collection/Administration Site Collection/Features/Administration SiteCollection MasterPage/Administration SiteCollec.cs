using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace Administration.Features.Administration_SiteCollection_MasterPage
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("b927c76f-c3f6-474f-a10e-db08e8f5c20c")]
    public class Administration_SiteCollection_MasterPageEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite siteCollection = properties.Feature.Parent as SPSite;

            if (siteCollection != null)
            {
                SPWeb topLevelSite = siteCollection.RootWeb;
                Uri masterUri = new Uri(topLevelSite.Url + "/_catalogs/masterpage/lg_Administration.master");

                //publish a major version of our custom master page
                SPFile file = topLevelSite.GetFile(masterUri.AbsolutePath);
                file.Publish("");

                //set the default master page
                topLevelSite.MasterUrl = masterUri.AbsolutePath;
                topLevelSite.CustomMasterUrl = masterUri.AbsolutePath;

                //set the default site logo
                topLevelSite.SiteLogoUrl = topLevelSite.Url + "/Style%20Library/Images/Logo.svg";
                topLevelSite.Update();
            }
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPSite siteCollection = properties.Feature.Parent as SPSite;

            if (siteCollection != null)
            {
                SPWeb topLevelSite = siteCollection.RootWeb;
                Uri masterUri = new Uri(topLevelSite.Url + "/_catalogs/masterpage/seattle.master");

                //set the default master page
                topLevelSite.MasterUrl = masterUri.AbsolutePath;
                topLevelSite.CustomMasterUrl = masterUri.AbsolutePath;
                topLevelSite.Update();
            }
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
