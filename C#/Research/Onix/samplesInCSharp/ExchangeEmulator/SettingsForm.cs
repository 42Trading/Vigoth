#region Copyright
/*
* Copyright Onix Solutions Limited [OnixS]. All rights reserved.
*
* This software owned by Onix Solutions Limited [OnixS] and is protected by copyright law
* and international copyright treaties.
*
* Access to and use of the software is governed by the terms of the applicable ONIXS Software
* Services Agreement (the Agreement) and Customer end user license agreements granting
* a non-assignable, non-transferable and non-exclusive license to use the software
* for it's own data processing purposes under the terms defined in the Agreement.
*
* Except as otherwise granted within the terms of the Agreement, copying or reproduction of any part
* of this source code or associated reference material to any other location for further reproduction
* or redistribution, and any amendments to this copyright notice, are expressly prohibited.
*
* Any reproduction or redistribution for sale or hiring of the Software not in accordance with
* the terms of the Agreement is a violation of copyright law.
*/
#endregion

using System;
using System.Configuration;
using System.Windows.Forms;

namespace ExchangeEmulator
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();

            tbAskPrice.Text = ConfigurationHelper.Get("AskPrice");
			tbBidPrice.Text = ConfigurationHelper.Get("BidPrice");
			tbPriceIncrement.Text = ConfigurationHelper.Get("PriceIncrement");
        }

        private void Save()
        {
			Configuration cfg = ConfigurationHelper.GetConfiguration();
			cfg.AppSettings.Settings["AskPrice"].Value = tbAskPrice.Text;
            cfg.AppSettings.Settings["BidPrice"].Value = tbBidPrice.Text;
            cfg.AppSettings.Settings["PriceIncrement"].Value = tbPriceIncrement.Text;
            cfg.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            Save();
            Close();
        }
    }
}