﻿#pragma checksum "..\..\..\Pages\AddProductPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CBF0A6FA183D4BA75A5BA9342B726042B4DEE18CFC17C3127EAC138AF47708B3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using AccountingOfGoods.Pages;
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


namespace AccountingOfGoods.Pages {
    
    
    /// <summary>
    /// AddProductPage
    /// </summary>
    public partial class AddProductPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\Pages\AddProductPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtIdProduct;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Pages\AddProductPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtNameProduct;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Pages\AddProductPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbNameCategory;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Pages\AddProductPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbUnit;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\Pages\AddProductPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtStorage;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\Pages\AddProductPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddProduct;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\Pages\AddProductPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteSection;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\Pages\AddProductPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteSection2;
        
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
            System.Uri resourceLocater = new System.Uri("/AccountingOfGoods;component/pages/addproductpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\AddProductPage.xaml"
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
            this.txtIdProduct = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtNameProduct = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.cmbNameCategory = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.cmbUnit = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.txtStorage = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.btnAddProduct = ((System.Windows.Controls.Button)(target));
            
            #line 110 "..\..\..\Pages\AddProductPage.xaml"
            this.btnAddProduct.Click += new System.Windows.RoutedEventHandler(this.btnAddProduct_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnDeleteSection = ((System.Windows.Controls.Button)(target));
            
            #line 124 "..\..\..\Pages\AddProductPage.xaml"
            this.btnDeleteSection.Click += new System.Windows.RoutedEventHandler(this.btnDeleteSection_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnDeleteSection2 = ((System.Windows.Controls.Button)(target));
            
            #line 136 "..\..\..\Pages\AddProductPage.xaml"
            this.btnDeleteSection2.Click += new System.Windows.RoutedEventHandler(this.btnDeleteSection2_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

