//-----------------------------------------------------------------------
// <copyright file="F:\Users\dlandry\Source\Repos\dwlandry\landrys-instrument-spec-manager\landrys_instrument_spec_manager.Module.Win\Editors\Spreadsheet\SpreadsheetContainer.cs" company="David W. Landry III">
//     Author: _**David Landry**_
//     *Copyright (c) David W. Landry III. All rights reserved.*
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Spreadsheet;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraSpreadsheet;
using System;
using System.Linq;
using System.Windows.Forms;

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

        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var workbook = SheetControl;

            workbook.Unit = DevExpress.Office.DocumentUnit.Inch;

            Margins pageMargins = workbook.ActiveWorksheet.ActiveView.Margins;

            pageMargins.Top = 0.25F;
            pageMargins.Bottom = 0.25F;
            pageMargins.Left = 0.25F;
            pageMargins.Right = 0.25F;
            pageMargins.Header = 0;
            pageMargins.Footer = 0;

            workbook.ActiveWorksheet.PrintOptions.CenterHorizontally = true;
            workbook.ActiveWorksheet.PrintOptions.CenterVertically = true;
        }

        
    }
}
