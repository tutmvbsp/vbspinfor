﻿#pragma checksum "..\..\..\WpfDoiChieu.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BB37C7AD202A467C31FA752F2C78A487625A672E"
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
    /// WpfDoiChieu
    /// </summary>
    public partial class WpfDoiChieu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\WpfDoiChieu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\WpfDoiChieu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOk;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\WpfDoiChieu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtpNgay;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\WpfDoiChieu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblNgay;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\WpfDoiChieu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboPos;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\WpfDoiChieu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblPos;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\WpfDoiChieu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboTo;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\WpfDoiChieu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CboXa;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\WpfDoiChieu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTo;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\WpfDoiChieu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblXa;
        
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
            System.Uri resourceLocater = new System.Uri("/Presentation;component/wpfdoichieu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WpfDoiChieu.xaml"
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
            
            #line 4 "..\..\..\WpfDoiChieu.xaml"
            ((Presentation.WpfDoiChieu)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 6 "..\..\..\WpfDoiChieu.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnOk = ((System.Windows.Controls.Button)(target));
            
            #line 7 "..\..\..\WpfDoiChieu.xaml"
            this.btnOk.Click += new System.Windows.RoutedEventHandler(this.btnOk_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.dtpNgay = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 5:
            this.lblNgay = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.CboPos = ((System.Windows.Controls.ComboBox)(target));
            
            #line 10 "..\..\..\WpfDoiChieu.xaml"
            this.CboPos.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CboPos_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lblPos = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.CboTo = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.CboXa = ((System.Windows.Controls.ComboBox)(target));
            
            #line 13 "..\..\..\WpfDoiChieu.xaml"
            this.CboXa.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.CboXa_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.lblTo = ((System.Windows.Controls.Label)(target));
            return;
            case 11:
            this.lblXa = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

