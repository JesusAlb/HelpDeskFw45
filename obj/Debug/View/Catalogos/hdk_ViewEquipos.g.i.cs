﻿#pragma checksum "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "21C503A6C5C233E14798FF083C5AC67B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.18444
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace HelpDesk.View.Catalogos {
    
    
    /// <summary>
    /// hdk_ViewEquipos
    /// </summary>
    public partial class hdk_ViewEquipos : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle titleBar;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minButton;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataG;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Add;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Mod;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Imp;
        
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
            System.Uri resourceLocater = new System.Uri("/HelpDesk;component/view/catalogos/hdk_viewequipos.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
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
            this.titleBar = ((System.Windows.Shapes.Rectangle)(target));
            
            #line 22 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
            this.titleBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.titleBar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.closeButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
            this.closeButton.Click += new System.Windows.RoutedEventHandler(this.closeButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.minButton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
            this.minButton.Click += new System.Windows.RoutedEventHandler(this.minButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DataG = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.Add = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
            this.Add.Click += new System.Windows.RoutedEventHandler(this.Add_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Mod = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
            this.Mod.Click += new System.Windows.RoutedEventHandler(this.Mod_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Imp = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\View\Catalogos\hdk_ViewEquipos.xaml"
            this.Imp.Click += new System.Windows.RoutedEventHandler(this.Imp_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

