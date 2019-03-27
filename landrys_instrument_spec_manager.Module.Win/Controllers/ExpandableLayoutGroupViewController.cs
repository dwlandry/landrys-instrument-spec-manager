﻿//-----------------------------------------------------------------------
// <copyright file="F:\Users\dlandry\Source\Repos\dwlandry\landrys-instrument-spec-manager\landrys_instrument_spec_manager.Module.Win\Controllers\ExpandableLayoutGroupViewController.cs" company="David W. Landry III">
//     Author: _**David Landry**_
//     *Copyright (c) David W. Landry III. All rights reserved.*
// </copyright>
//-----------------------------------------------------------------------
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Layout;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.Utils;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace landrys_instrument_spec_manager.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ExpandableLayoutGroupViewController : ViewController<DetailView>, IModelExtender
    {
        Dictionary<string, IModelWinLayoutGroupExtender> itemToWinModelLayoutGroupExtenderMap = new Dictionary<string, IModelWinLayoutGroupExtender>();
        WinLayoutManager winLayoutManager = null;

        public ExpandableLayoutGroupViewController() => InitializeComponent();
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            winLayoutManager = View.LayoutManager as WinLayoutManager;
            if (winLayoutManager != null)
            {
                winLayoutManager.ItemCreated += ExpandableLayoutGroupViewControllercs_ItemCreated;
                if (winLayoutManager.Container != null)
                {
                    winLayoutManager.Container.HandleCreated += Container_HandleCreated;
                }
                View.ModelSaved += View_ModelSaved;
            }
        }
        protected override void OnViewControlsCreated() => base.OnViewControlsCreated();
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            if (winLayoutManager != null)
            {
                winLayoutManager.ItemCreated -= ExpandableLayoutGroupViewControllercs_ItemCreated;
                if (winLayoutManager.Container != null)
                {
                    winLayoutManager.Container.HandleCreated -= Container_HandleCreated;
                    winLayoutManager = null;
                }
                View.ModelSaved -= View_ModelSaved;
            }
            itemToWinModelLayoutGroupExtenderMap.Clear();
            base.OnDeactivated();
        }

        public void ExtendModelInterfaces(ModelInterfaceExtenders extenders)
        {
            extenders.Add<IModelWinLayoutGroup, IModelWinLayoutGroupExtender>();
        }
        void ExpandableLayoutGroupViewControllercs_ItemCreated(object sender, ItemCreatedEventArgs e)
        {
            if (e.ModelLayoutElement is IModelWinLayoutGroup)
            {
                IModelWinLayoutGroupExtender modelLayoutGroupExtender = (IModelWinLayoutGroupExtender)e.ModelLayoutElement;
                if ((modelLayoutGroupExtender).Expandable)
                {
                    itemToWinModelLayoutGroupExtenderMap.Add(e.Item.Name, (IModelWinLayoutGroupExtender)e.ModelLayoutElement);
                }
            }
        }
        void Container_HandleCreated(object sender, EventArgs e)
        {
            LayoutControl lc = ((LayoutControl)sender);
            lc.BeginUpdate();
            foreach (BaseLayoutItem item in lc.Items)
            {
                if ((item is LayoutControlGroup) && itemToWinModelLayoutGroupExtenderMap.ContainsKey(item.Name))
                {
                    ((LayoutGroup)item).Expanded = itemToWinModelLayoutGroupExtenderMap[item.Name].Expanded;
                    ((LayoutGroup)item).HeaderButtonsLocation = itemToWinModelLayoutGroupExtenderMap[item.Name].HeaderButtonsLocation;
                    ((LayoutGroup)item).ExpandButtonVisible = true;
                    ((LayoutGroup)item).ExpandOnDoubleClick = true;
                }
            }
            lc.EndUpdate();
        }
        void View_ModelSaved(object sender, EventArgs e)
        {
            foreach (BaseLayoutItem item in winLayoutManager.Container.Items)
            {
                if ((item is LayoutControlGroup) && itemToWinModelLayoutGroupExtenderMap.ContainsKey(item.Name))
                {
                    itemToWinModelLayoutGroupExtenderMap[item.Name].Expanded = ((LayoutGroup)item).Expanded;
                }
            }
        }
    }
    public interface IModelWinLayoutGroupExtender
    {
        [DefaultValue(true)]
        bool Expanded { get; set; }
        [DefaultValue(true)]
        bool Expandable { get; set; }
        [DefaultValue(GroupElementLocation.Default)]
        GroupElementLocation HeaderButtonsLocation { get; set; }
    }
}
