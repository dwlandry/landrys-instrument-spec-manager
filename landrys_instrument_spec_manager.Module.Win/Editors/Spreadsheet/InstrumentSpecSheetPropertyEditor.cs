//-----------------------------------------------------------------------
// <copyright file="F:\Users\dlandry\Source\Repos\dwlandry\landrys-instrument-spec-manager\landrys_instrument_spec_manager.Module.Win\Editors\Spreadsheet\InstrumentSpecSheetPropertyEditor.cs" company="David W. Landry III">
//     Author: _**David Landry**_
//     *Copyright (c) David W. Landry III. All rights reserved.*
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using System;
using System.IO;
using System.Text;

namespace landrys_instrument_spec_manager.Module.Win.Editors.Spreadsheet
{
    // For an example on how to implement a property editor in winforms, refer to https://docs.devexpress.com/eXpressAppFramework/112679/task-based-help/property-editors/how-to-implement-a-property-editor-based-on-a-custom-control-winforms

    [PropertyEditor(typeof(String), "XLS", false)]
    public class InstrumentSpecSheetPropertyEditor : WinPropertyEditor, IInplaceEditSupport
    {
        private SpreadsheetContainer ctrl = null;
        //bool changed = false;

        public InstrumentSpecSheetPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }



        protected override object CreateControlCore()
        {

            ctrl = new SpreadsheetContainer();
            ctrl.SheetControl.ModifiedChanged += SpreadSheetEditValueChanged;
            //ctrl.SheetControl.DocumentLoaded += SpreadsheetDocumentLoaded;
            //ctrl.SheetControl.ContentChanged += SpreadSheetEditValueChanged;
            ctrl.SheetControl.ReadOnly = true;
            ctrl.RibbonControl.Minimized = true;
            ControlBindingProperty = "Text";
            return ctrl;
        }



        protected override void ReadValueCore()  //Load From Text PropertyValue
        {
            UnicodeEncoding uniEncoding = new UnicodeEncoding();
            if (PropertyValue != null)
            {
                try
                {
                    Byte[] bytes = Convert.FromBase64String(PropertyValue.ToString());

                    using (MemoryStream ms = new MemoryStream(bytes))
                    {

                        ms.Position = 0;
                        ctrl.SheetControl.LoadDocument(ms, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
                    }
                }
                catch (Exception)
                {
                    string filePath = PropertyValue.ToString();
                    if (File.Exists(filePath))
                    {
                        ctrl.SheetControl.LoadDocument(filePath);
                    }
                    else if (File.Exists(filePath.Replace(@"E:\ReCon\", @"D:\")))
                    {
                        ctrl.SheetControl.LoadDocument(filePath.Replace(@"E:\ReCon\", @"D:\"));
                    }
                    else if (File.Exists(filePath.Replace(@"E:\ReCon\", @"F:\")))
                    {
                        ctrl.SheetControl.LoadDocument(filePath.Replace(@"E:\ReCon\", @"F:\"));
                    }
                    else if (File.Exists(filePath.Replace(@"E:\ReCon\", @"\\RECONSERVER\TX Users\dlandry\Public\landrys-instrument-spec-manager\")))
                    {
                        ctrl.SheetControl.LoadDocument(filePath.Replace(@"E:\ReCon\", @"\\RECONSERVER\TX Users\dlandry\Public\landrys-instrument-spec-manager\"));
                    }
                    else
                    {
                        ctrl.SheetControl.CreateNewDocument();
                        ctrl.SheetControl.Document.Worksheets[0].Cells[0, 0].Value = "The spec sheet could not be located.  Looked in the following locations:";
                        ctrl.SheetControl.Document.Worksheets[0].Cells[1, 0].Value = filePath;
                        ctrl.SheetControl.Document.Worksheets[0].Cells[2, 0].Value = filePath.Replace(@"E:\ReCon\", @"D:\");
                        ctrl.SheetControl.Document.Worksheets[0].Cells[3, 0].Value = filePath.Replace(@"E:\ReCon\", @"F:\");
                        ctrl.SheetControl.Document.Worksheets[0].Cells[4, 0].Value = filePath.Replace(@"E:\ReCon\", @"\\RECONSERVER\TX Users\dlandry\Public\landrys-instrument-spec-manager\");
                    }
                    //throw;
                }



            }
            else
            {
                // load a new workbook
                ctrl.SheetControl.CreateNewDocument();
                //ctrl.SheetControl.ReadOnly = false;
                //MessageBox.Show("PropertyValue is null.");
            }
        }

        private void SpreadSheetEditValueChanged(object sender, EventArgs e) // Save to Text PropertyValue
        {

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {

                    ctrl.SheetControl.SaveDocument(ms, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
                    //ms.Seek(0, SeekOrigin.Begin);
                    ms.Position = 0;

                    PropertyValue = Convert.ToBase64String(ms.ToArray());
                }
            }
            catch { PropertyValue = null; }

        }

        //private void SpreadsheetDocumentLoaded(object sender, EventArgs e)
        //{
        //    // if the loaded document is different from the one on file, raise the event SpreadSheetEditValueChanged
        //    if (PropertyValue != null)
        //    {
        //        var loadedSpreadsheetValue="";
        //        using (MemoryStream ms = new MemoryStream())
        //        {

        //            ctrl.SheetControl.SaveDocument(ms, DevExpress.Spreadsheet.DocumentFormat.OpenXml);
        //            //ms.Seek(0, SeekOrigin.Begin);
        //            ms.Position = 0;

        //            loadedSpreadsheetValue = Convert.ToBase64String(ms.ToArray());
        //        }

        //        Byte[] bytes = Convert.FromBase64String(PropertyValue.ToString());
        //    }
        //    MessageBox.Show("SpreadsheetDocumentLoaded");
        //}

        protected override void OnAllowEditChanged()
        {
            base.OnAllowEditChanged();
            if (ctrl != null)
                ctrl.SheetControl.ReadOnly = !AllowEdit;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (ctrl != null)
        //    {
        //        ctrl.SheetControl.ModifiedChanged -= SpreadSheetEditValueChanged;
        //        ctrl = null;
        //    }
        //    base.Dispose(disposing);
        //}

        public DevExpress.XtraEditors.Repository.RepositoryItem CreateRepositoryItem()
        {
            throw new NotImplementedException();
        }
    }
}
