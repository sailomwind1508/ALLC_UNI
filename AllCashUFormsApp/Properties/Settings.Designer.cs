﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AllCashUFormsApp.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=UBN.DNSDOJO.NET,14501;Initial Catalog=DB_ALL_CASH_UNI_UBN_TEST;Persis" +
            "t Security Info=True;User ID=sa;Password=Alldb#")]
        public string DB_ALL_CASH_UNI_UBN_TESTConnectionString {
            get {
                return ((string)(this["DB_ALL_CASH_UNI_UBN_TESTConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=LRI.DNSDOJO.NET,14501;Initial Catalog=DB_ALL_CASH_UNI_LRI_TEST;Persis" +
            "t Security Info=True;User ID=sa;Password=Alldb#")]
        public string DB_ALL_CASH_UNI_LRI_TESTConnectionString {
            get {
                return ((string)(this["DB_ALL_CASH_UNI_LRI_TESTConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.1.200;Initial Catalog=DB_ALL_CASH_UNI_SNK;Persist Security In" +
            "fo=True;User ID=sa;Password=Alldb#")]
        public string DB_ALL_CASH_UNI_SNKConnectionString {
            get {
                return ((string)(this["DB_ALL_CASH_UNI_SNKConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.1.200;Initial Catalog=DB_ALL_CASH_UNI_TKM;User ID=sa")]
        public string DB_ALL_CASH_UNI_TKMConnectionString {
            get {
                return ((string)(this["DB_ALL_CASH_UNI_TKMConnectionString"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=192.168.1.200;Initial Catalog=DB_ALL_CASH_UNI_TKM_TEST;User ID=sa")]
        public string DB_ALL_CASH_UNI_TKM_TESTConnectionString {
            get {
                return ((string)(this["DB_ALL_CASH_UNI_TKM_TESTConnectionString"]));
            }
        }
    }
}
