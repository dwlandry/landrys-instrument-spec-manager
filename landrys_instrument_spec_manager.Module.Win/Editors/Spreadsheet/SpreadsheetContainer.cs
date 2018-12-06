using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraBars.Ribbon;

namespace landrys_instrument_spec_manager.Module.Win.Editors.Spreadsheet
{
    public partial class SpreadsheetContainer : UserControl
    {
        public SpreadsheetContainer()
        {
            InitializeComponent();
        }
        public SpreadsheetControl SheetControl => spreadsheetControl1;
        public RibbonControl RibbonControl => ribbonControl1;
        
        
        private void barStaticItemReadOnlyStatus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SheetControl.ReadOnly = !SheetControl.ReadOnly;
        }

        private void spreadsheetControl1_ReadOnlyChanged(object sender, EventArgs e)
        {
            barStaticItemReadOnlyStatus.Caption = SheetControl.ReadOnly ? "[READ ONLY MODE]" : "[EDIT MODE]";
        }
    }
}
