﻿#pragma checksum "..\..\..\ViewAltas\hdk_ViewAltaReqCuantificables.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9EC30FA2439955D2BD6A4CB8EF02371A"
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
    /// hdk_ViewAltaReqCuantificables
    /// </summary>
    public partial class hdk_ViewAltaReqCuantificables : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\ViewAltas\hdk_ViewAltaReqCuantificables.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Aceptar;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\ViewAltas\hdk_ViewAltaReqCuantificables.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox can;
        
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
            System.Uri resourceLocater = new System.Uri("/HelpDesk;component/viewaltas/hdk_viewaltareqcuantificables.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ViewAltas\hdk_ViewAltaReqCuantificables.xaml"
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
            this.Aceptar = ((System.Windows.Controls.Button)(target));
            
            #line 8 "..\..\..\ViewAltas\hdk_ViewAltaReqCuantificables.xaml"
            this.Aceptar.Click += new System.Windows.RoutedEventHandler(this.Aceptar_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.can = ((System.Windows.Controls.TextBox)(target));
            
            #line 9 "..\..\..\ViewAltas\hdk_ViewAltaReqCuantificables.xaml"
            this.can.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.can_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
