﻿#pragma checksum "..\..\..\..\Windows\CreateAccountWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4A5EC25E4F27D297CA67CCB15A22C52263FA82D2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Question_Maker_Pro_WPF_Prototype.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Question_Maker_Pro_WPF_Prototype.Windows {
    
    
    /// <summary>
    /// CreateAccountWindow
    /// </summary>
    public partial class CreateAccountWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\..\..\Windows\CreateAccountWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailTextBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Windows\CreateAccountWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passwordEntry;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Windows\CreateAccountWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox confirmPasswordEntry;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Windows\CreateAccountWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox firstNameTextbox;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Windows\CreateAccountWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lastNameTextbox;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\Windows\CreateAccountWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox organizationTextBox;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\Windows\CreateAccountWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button submitButton;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Windows\CreateAccountWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Question Maker Pro WPF Prototype;component/windows/createaccountwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Windows\CreateAccountWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.emailTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.passwordEntry = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.confirmPasswordEntry = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            this.firstNameTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.lastNameTextbox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.organizationTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.submitButton = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\..\..\Windows\CreateAccountWindow.xaml"
            this.submitButton.Click += new System.Windows.RoutedEventHandler(this.button_click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\..\Windows\CreateAccountWindow.xaml"
            this.cancelButton.Click += new System.Windows.RoutedEventHandler(this.button_click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

