﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using Newtonsoft.Json.Linq;
using DevExpress.XtraEditors;

namespace SaccoBook.Modules.Settings.Notifications
{
    public partial class AssignDocumentTemplate : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string _DocumentId = null;
        public AssignDocumentTemplate()
        {
            InitializeComponent();
            // This line of code is generated by Data Source Configuration Wizard
            // Fill a SqlDataSource
            sqlDataSource1.Fill();
        }
        public AssignDocumentTemplate(string DocumentId)
        {
            InitializeComponent();
            // This line of code is generated by Data Source Configuration Wizard
            // Fill a SqlDataSource
            sqlDataSource1.Fill();
            _DocumentId = DocumentId;
            LoadtemplateSettings(DocumentId);
        }

        private void LoadtemplateSettings(string documentId)
        {
            string TemplateSettings = EF.NotificationTemplatesQueries.GetNotificationSettingInfo(documentId);
            dynamic GetNotificationSettingInfoResponseObject = JObject.Parse(TemplateSettings);

            string NotificationTemplateId = GetNotificationSettingInfoResponseObject.NotificationTemplateId;
            bool NotificationsEnabled = GetNotificationSettingInfoResponseObject.NotificationsEnabled;
            string Description = GetNotificationSettingInfoResponseObject.Description;
            _EnableNotifications.Checked = NotificationsEnabled;
            _Document.Text = Description;
            _Template.EditValue = NotificationTemplateId;
        }

        private void btn_save_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (dxValidationProviderAssignDocumentTemplate.Validate().Equals(true))
            {
                if(EF.NotificationTemplatesQueries.UpdateNotificationSettings(_DocumentId, _Document.Text, _Template.EditValue.ToString(), _EnableNotifications.Checked, Login.LoggedInUser, DateTime.Now.ToString()))
                {
                    XtraMessageBox.Show("Information has been successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}