//-----------------------------------------------------------------------
// <copyright file="F:\Users\dlandry\Source\Repos\dwlandry\landrys-instrument-spec-manager\landrys_instrument_spec_manager.Module\BusinessObjects\Instrument.cs" company="David W. Landry III">
//     Author: _**David Landry**_
//     *Copyright (c) David W. Landry III. All rights reserved.*
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace landrys_instrument_spec_manager.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Spec Sheets")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Instrument : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Instrument(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}

        string modelNumber;
        string manufacturer;
        InstrumentType instrumentType;
        string serviceDescription;
        Project project;
        string specSheet;
        string tag;

        [Size(40), ToolTip("Instrument Tag")]
        public string Tag
        {
            get => tag;
            set => SetPropertyValue(nameof(Tag), ref tag, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ServiceDescription
        {
            get => serviceDescription;
            set => SetPropertyValue(nameof(ServiceDescription), ref serviceDescription, value);
        }


        public InstrumentType InstrumentType
        {
            get => instrumentType;
            set => SetPropertyValue(nameof(InstrumentType), ref instrumentType, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Manufacturer
        {
            get => manufacturer;
            set => SetPropertyValue(nameof(Manufacturer), ref manufacturer, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ModelNumber
        {
            get => modelNumber;
            set => SetPropertyValue(nameof(ModelNumber), ref modelNumber, value);
        }

        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false)]
        [EditorAlias("Spec Sheet")]
        public string SpecSheet
        {
            get => specSheet;
            set => SetPropertyValue(nameof(SpecSheet), ref specSheet, value);
        }


        [Association("Project-Instruments")]
        public Project Project
        {
            get => project;
            set => SetPropertyValue(nameof(Project), ref project, value);
        }

    }

}