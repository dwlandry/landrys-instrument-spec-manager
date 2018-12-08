//-----------------------------------------------------------------------
// <copyright file="E:\OneDrive\My Programming Projects\landrys-instrument-spec-manager\landrys_instrument_spec_manager.Module\BusinessObjects\Project.cs" company="David W. Landry III">
//     Author: _**David Landry**_
//     *Copyright (c) David W. Landry III. All rights reserved.*
// </copyright>
//-----------------------------------------------------------------------

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace landrys_instrument_spec_manager.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[ImageName("BO_Contact")]
    [DefaultProperty("ProjectNumber")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class Project : BaseObject, ITreeNode
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Project(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        Client client;
        string projectDescription;
        string projectNumber;

        [Size(20)]
        public string ProjectNumber
        {
            get => projectNumber;
            set => SetPropertyValue(nameof(ProjectNumber), ref projectNumber, value);
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string ProjectDescription
        {
            get => projectDescription;
            set => SetPropertyValue(nameof(ProjectDescription), ref projectDescription, value);
        }

        [Association("Project-Instruments")]
        public XPCollection<Instrument> Instruments
        {
            get
            {
                return GetCollection<Instrument>(nameof(Instruments));
            }
        }

        [Association("Client-Projects")]
        public Client Client
        {
            get => client;
            set => SetPropertyValue(nameof(Client), ref client, value);
        }

        public string Name => $"{ProjectNumber} - {ProjectDescription}";

        public ITreeNode Parent => Client;

        public IBindingList Children => Instruments;
    }
}