﻿#pragma checksum "..\..\..\WpfChamCongSet.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4AFE231B996913320F7F5F214BC87B0A439F0DAC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RootLibrary.WPF.Localization;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Presentation {
    
    
    /// <summary>
    /// WpfChamCongSet
    /// </summary>
    public partial class WpfChamCongSet : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\WpfChamCongSet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\WpfChamCongSet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOk;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\WpfChamCongSet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgvData;
        
        #line default
        #line hidden
        
        
        #line 159 "..\..\..\WpfChamCongSet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNgay_Copy;
        
        #line default
        #line hidden
        
        
        #line 160 "..\..\..\WpfChamCongSet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtpNgay;
        
        #line default
        #line hidden
        
        
        #line 161 "..\..\..\WpfChamCongSet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPos;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\WpfChamCongSet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboPos;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\WpfChamCongSet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblXa;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\..\WpfChamCongSet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboPB;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\..\WpfChamCongSet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblGetData;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Presentation;component/wpfchamcongset.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WpfChamCongSet.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\..\WpfChamCongSet.xaml"
            ((Presentation.WpfChamCongSet)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 6 "..\..\..\WpfChamCongSet.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_OnClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnOk = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\WpfChamCongSet.xaml"
            this.btnOk.Click += new System.Windows.RoutedEventHandler(this.BtnOk_OnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.dgvData = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.lblNgay_Copy = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.dtpNgay = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.lblPos = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.CboPos = ((System.Windows.Controls.ComboBox)(target));
            
            #line 162 "..\..\..\WpfChamCongSet.xaml"
            this.CboPos.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CboPos_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lblXa = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.CboPB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 164 "..\..\..\WpfChamCongSet.xaml"
            this.CboPB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CboPB_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.lblGetData = ((System.Windows.Controls.Label)(target));
            
            #line 165 "..\..\..\WpfChamCongSet.xaml"
            this.lblGetData.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.LblGetData_OnMouseDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
