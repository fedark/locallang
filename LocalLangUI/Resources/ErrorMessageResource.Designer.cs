﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LocalLangUI.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessageResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessageResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LocalLangUI.Resources.ErrorMessageResource", typeof(ErrorMessageResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Категория не определена.
        /// </summary>
        public static string CategoryInvalidError {
            get {
                return ResourceManager.GetString("CategoryInvalidError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ты далбаеб? Сказано же, выберите категорию, блять.
        /// </summary>
        public static string CategoryRequiredError {
            get {
                return ResourceManager.GetString("CategoryRequiredError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Слишком длинное выражение.
        /// </summary>
        public static string ExpressionInvalidError {
            get {
                return ResourceManager.GetString("ExpressionInvalidError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Укажите выражение.
        /// </summary>
        public static string ExpressionRequiredError {
            get {
                return ResourceManager.GetString("ExpressionRequiredError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Слишком длинный перевод.
        /// </summary>
        public static string TranslationInvalidError {
            get {
                return ResourceManager.GetString("TranslationInvalidError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Укажите перевод.
        /// </summary>
        public static string TranslationRequiredError {
            get {
                return ResourceManager.GetString("TranslationRequiredError", resourceCulture);
            }
        }
    }
}