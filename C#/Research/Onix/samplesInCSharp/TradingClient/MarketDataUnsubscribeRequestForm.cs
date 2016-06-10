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

using System.Windows.Forms;
using System.Collections.Generic;

namespace TradingClientSample
{
    public partial class MarketDataUnsubscribeRequestForm : Form
    {
		public MarketDataUnsubscribeRequestForm(List<string> symbols)
        {
            InitializeComponent();

			symbolComboBox.Items.AddRange(symbols.ToArray());
        }

        public string Symbol
        {
            get { return symbolComboBox.Text; }
        }
    }
}