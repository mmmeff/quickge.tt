﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace quickge.tt.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string email {
            get {
                return ((string)(this["email"]));
            }
            set {
                this["email"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string pass {
            get {
                return ((string)(this["pass"]));
            }
            set {
                this["pass"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(@"AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA7NgBr7rHo0qkwNxaBjfbSAAAAAACAAAAAAAQZgAAAAEAACAAAACGLXcAPXeRFrim3wQDJOO2mU2Br8Gdj4Qs++JJGE5a2wAAAAAOgAAAAAIAACAAAACtHEAF30H5vsTb+SHVV16HR+e6cSGZ3wuQldnsaCg/rVAAAAD/uoaHQL6EI0aFaXP1KBAG3lNFWUdepVN7rjHHl6yGRLSm+qgaWicvuSLRpJ3cnz9i553YXARoleEQdfr8sLh4J6BEVRwZqy5bJNM4lAZ8Q0AAAABWjyfVfBLRsQ2MGDtM2K6HvTKnLCDTw0OUnqdg66h+G9ZrkMCb6prpWt2E3X6+FapIn2HwcTGyDkpsXDZ9yXgb")]
        public string apikey {
            get {
                return ((string)(this["apikey"]));
            }
            set {
                this["apikey"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool valid {
            get {
                return ((bool)(this["valid"]));
            }
            set {
                this["valid"] = value;
            }
        }
    }
}
