﻿#pragma checksum "..\..\..\WpfSkeNqh.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F87032FB5553B1E64C38C9EEF9A74255E3106223"
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
    /// WpfSkeNqh
    /// </summary>
    public partial class WpfSkeNqh : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\WpfSkeNqh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtpNgay;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\WpfSkeNqh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNgay;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\WpfSkeNqh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPos;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\WpfSkeNqh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboPos;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\WpfSkeNqh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblXa;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\WpfSkeNqh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboXa;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\WpfSkeNqh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\WpfSkeNqh.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOk;
        
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
            System.Uri resourceLocater = new System.Uri("/Presentation;component/wpfskenqh.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WpfSkeNqh.xaml"
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
            
            #line 4 "..\..\..\WpfSkeNqh.xaml"
            ((Presentation.WpfSkeNqh)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dtpNgay = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.lblNgay = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lblPos = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.CboPos = ((System.Windows.Controls.ComboBox)(target));
            
            #line 9 "..\..\..\WpfSkeNqh.xaml"
            this.CboPos.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CboPos_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lblXa = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.CboXa = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\WpfSkeNqh.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnOk = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\WpfSkeNqh.xaml"
            this.btnOk.Click += new System.Windows.RoutedEventHandler(this.btnOk_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
