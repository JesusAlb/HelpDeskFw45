﻿#pragma checksum "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2B96E69B16715A699E6DB9464E075BB5"
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


namespace HelpDesk.ViewAltas {
    
    
    /// <summary>
    /// hdk_ViewAltaTipoIncidencia
    /// </summary>
    public partial class hdk_ViewAltaTipoIncidencia : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle titleBar;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minButton;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTipo;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAñadir;
        
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
            System.Uri resourceLocater = new System.Uri("/HelpDesk;component/viewaltas/cat%c3%a1logo/hdk_viewaltatipoincidencia.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
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
            
            #line 22 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
            this.titleBar.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.titleBar_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.closeButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
            this.closeButton.Click += new System.Windows.RoutedEventHandler(this.closeButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.minButton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
            this.minButton.Click += new System.Windows.RoutedEventHandler(this.minButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtTipo = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
            this.txtTipo.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.tipo_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
            this.txtTipo.KeyDown += new System.Windows.Input.KeyEventHandler(this.tipo_KeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnAñadir = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\..\ViewAltas\Catálogo\hdk_ViewAltaTipoIncidencia.xaml"
            this.btnAñadir.Click += new System.Windows.RoutedEventHandler(this.añadir_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

