using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Entrada.Editor.Core;

namespace Entrada.Editor
{
    public partial class PreferencesTab : UserControl
    {
        private AudioDjStudio.AudioDjStudio dj;

        public PreferencesTab (AudioDjStudio.AudioDjStudio dj)
        {
            InitializeComponent ();

            this.dj = dj;
        }

        private void PreferencesTab_Load (object sender, EventArgs e)
        {
            cmbDefaultZoom.SelectedIndex = cmbDefaultZoom.Properties.Items.IndexOf (EditorCore.Settings.DefaultDocumentZoom + "%");
            cmbAudioSpeed.SelectedIndex = cmbAudioSpeed.Properties.Items.IndexOf (EditorCore.Settings.FastForwardRewindSpeed + "%");
            txtFootPedalBounce.Text = EditorCore.Settings.FootPedalBounceBack.ToString ();
            txtKeyboardBounce.Text = EditorCore.Settings.KeyboardBounceBack.ToString ();

            UpdateDeviceList ();

            if (EditorCore.Settings.ActiveAudioDevice >= 0 && cmbAudioDevice.Properties.Items.Count >= EditorCore.Settings.ActiveAudioDevice)
                cmbAudioDevice.SelectedIndex = EditorCore.Settings.ActiveAudioDevice;
        }

        private void btnSave_Click (object sender, EventArgs e)
        {
            EditorCore.Settings.DefaultDocumentZoom = int.Parse (cmbDefaultZoom.Text.Trim ('%'));
            EditorCore.Settings.FastForwardRewindSpeed = int.Parse (cmbAudioSpeed.Text.Trim ('%'));
            EditorCore.Settings.FootPedalBounceBack = float.Parse (txtFootPedalBounce.Text);
            EditorCore.Settings.KeyboardBounceBack = float.Parse (txtKeyboardBounce.Text);

            var index = (short)cmbAudioDevice.SelectedIndex;

            if (index >= 0) {
                EditorCore.Settings.PreferredAudioDevice = cmbAudioDevice.SelectedItem.ToString ();

                dj.StreamOutputDeviceSet (0, index);
                EditorCore.Settings.ActiveAudioDevice = index;
            }
        }

        private void UpdateDeviceList ()
        {
            var index = cmbAudioDevice.SelectedIndex;

            cmbAudioDevice.Properties.Items.Clear ();

            var count = dj.GetOutputDevicesCount ();

            for (short i = 0; i < count; i++)
                cmbAudioDevice.Properties.Items.Add (dj.GetOutputDeviceDesc (i));

            if (index >= 0)
                cmbAudioDevice.SelectedIndex = index;
        }        
    }
}
