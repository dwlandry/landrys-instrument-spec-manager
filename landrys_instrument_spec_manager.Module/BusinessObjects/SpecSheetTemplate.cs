//-----------------------------------------------------------------------
// <copyright file="F:\Users\dlandry\Source\Repos\dwlandry\landrys-instrument-spec-manager\landrys_instrument_spec_manager.Module\BusinessObjects\SpecSheetTemplate.cs" company="David W. Landry III">
//     Author: _**David Landry**_
//     *Copyright (c) David W. Landry III. All rights reserved.*
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace landrys_instrument_spec_manager.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Spec Sheets"), CreatableItem(false)]
    [DefaultListViewOptions(MasterDetailMode.ListViewAndDetailView, false, NewItemRowPosition.None)]
    public class SpecSheetTemplate : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public SpecSheetTemplate(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction() => base.AfterConstruction();

        string source;
        string formNumber;
        string title;
        string specSheet;
        SpecSheetTemplateCategory category;

        public SpecSheetTemplateCategory Category { get => category; set => SetPropertyValue(nameof(Category), ref category, value); }


        [Size(SizeAttribute.Unlimited)]
        [VisibleInListView(false)]
        [EditorAlias("Template")]
        public string SpecSheet { get => specSheet; set => SetPropertyValue(nameof(SpecSheet), ref specSheet, value); }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Title { get => title; set => SetPropertyValue(nameof(Title), ref title, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string FormNumber { get => formNumber; set => SetPropertyValue(nameof(FormNumber), ref formNumber, value); }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Source { get => source; set => SetPropertyValue(nameof(Source), ref source, value); }

        //public string DisplayName { get => $"{category.Name} _ {title} _ {source} _ {formNumber}"; }
    }
}