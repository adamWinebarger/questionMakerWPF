﻿#pragma checksum "..\..\..\..\Pages\NewTesterInfoPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6EBDD3F5ECFB42A2709B9AFC706F5B40B431173D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Question_Maker_Pro_WPF_Prototype.Pages;
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


namespace Question_Maker_Pro_WPF_Prototype.Pages {
    
    
    /// <summary>
    /// NewTesterInfoPage
    /// </summary>
    public partial class NewTesterInfoPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\Pages\NewTesterInfoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox firstNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Pages\NewTesterInfoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lastNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\Pages\NewTesterInfoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dobPicker;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Pages\NewTesterInfoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton maleRadioButton;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Pages\NewTesterInfoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton femaleRadioButton;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Pages\NewTesterInfoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton otherRadioButton;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Pages\NewTesterInfoPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button submitButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Question Maker Pro WPF Prototype;component/pages/newtesterinfopage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\NewTesterInfoPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.firstNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.lastNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.dobPicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.maleRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 5:
            this.femaleRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 6:
            this.otherRadioButton = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.submitButton = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\..\..\Pages\NewTesterInfoPage.xaml"
            this.submitButton.Click += new System.Windows.RoutedEventHandler(this.submitButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

