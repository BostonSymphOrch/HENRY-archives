using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BSO.Archive.WebApp.Classes
{
    public class FormHelpers
    {
        public static void SaveFormItems(Hashtable _results, Control _container)
        {
            foreach (Control ctrl in _container.Controls)
            {
                if (ctrl is ListControl)
                    _results.Add(ctrl.ID, ((ListControl)ctrl).Items);

                else if (ctrl is TextBox)
                    _results.Add(ctrl.ID, ((TextBox)ctrl).Text);

                else if (ctrl is CheckBox)
                    _results.Add(ctrl.ID, ((CheckBox)ctrl).Checked);

                else
                {
                    ValidationPropertyAttribute attr = (ValidationPropertyAttribute)Attribute.GetCustomAttribute(ctrl.GetType(), typeof(ValidationPropertyAttribute));

                    if (attr != null)
                    {
                        System.Reflection.PropertyInfo info = ctrl.GetType().GetProperty(attr.Name);
                        _results.Add(ctrl.ID, info.GetValue(ctrl, null));
                    }
                }
            }
        }

        public static void LoadFormItems(Hashtable _input, Control _container)
        {
            foreach (DictionaryEntry de in _input)
            {
                Control ctrl = _container.FindControl(de.Key.ToString());

                if (ctrl == null) continue;

                if (ctrl is ListControl)
                {
                    if (de.Value is ListItemCollection)
                    {
                        ListControl _lstCtrl = (ListControl)ctrl;
                        foreach (ListItem li in ((ListItemCollection)de.Value))
                        {
                            ListItem _curr = _lstCtrl.Items.FindByValue(li.Value);
                            if (_curr != null && _curr.Selected != li.Selected)
                            {
                                if (_lstCtrl is ListBox &&
                                    (((ListBox)_lstCtrl).SelectionMode == ListSelectionMode.Multiple))
                                    _curr.Selected = li.Selected;
                                else
                                    _lstCtrl.SelectedIndex = _lstCtrl.Items.IndexOf(_curr);
                            }
                        }
                    }
                    else
                    {
                        ListControl _lstCtrl = (ListControl)ctrl;
                        ListItem _curr = _lstCtrl.Items.FindByValue(de.Value.ToString());
                        if (_curr != null)
                            _lstCtrl.SelectedIndex = _lstCtrl.Items.IndexOf(_curr);
                    }
                }

                else if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = de.Value.ToString();

                else if (ctrl is CheckBox)
                    ((CheckBox)ctrl).Checked = (bool)de.Value;

                else
                {
                    ValidationPropertyAttribute attr = (ValidationPropertyAttribute)
                        Attribute.GetCustomAttribute(ctrl.GetType(),
                        typeof(ValidationPropertyAttribute));

                    if (attr != null)
                    {
                        System.Reflection.PropertyInfo info = ctrl.GetType().GetProperty(attr.Name);
                        try
                        {
                            info.SetValue(ctrl, de.Value, null);
                        }
                        catch
                        {
                            try
                            {
                                // try to set a mythical property named value if we failed
                                info = ctrl.GetType().GetProperty("Value");
                                info.SetValue(ctrl, de.Value, null);
                            }
                            catch { }
                        }
                    }
                }
            }
        }

        public const string Dictionary_Serialize = "Dictionary_Serialize";
        /// <summary>
        /// Saves the form items: textbox, lists, checkbox, radio button, and validation properties recursively.
        /// </summary>
        /// <param name="_results">Dictionary collection to serialize for future use</param>
        /// <param name="_container"></param>
        public static void SaveFormItems(System.Collections.Specialized.StringDictionary _results,
            Control _container)
        {
            foreach (Control ctrl in _container.Controls)
            {
                if (ctrl is ListControl)
                {
                    string selected = string.Empty;
                    foreach (ListItem li in ((ListControl)ctrl).Items)
                        if (li.Selected)
                            selected += li.Value + "|";

                    if (selected.Length > 1)
                        selected = selected.Remove(selected.Length - 1, 1);
                    SaveDictionaryItem(_results, ctrl.UniqueID, selected);
                }

                else if (ctrl is TextBox)
                    SaveDictionaryItem(_results, ctrl.UniqueID, ((TextBox)ctrl).Text);

                else if (ctrl is CheckBox)
                    SaveDictionaryItem(_results, ctrl.UniqueID,
                        ((CheckBox)ctrl).Checked.ToString());

                else if (ctrl is RadioButton)
                    SaveDictionaryItem(_results, ctrl.UniqueID,
                        ((RadioButton)ctrl).Checked.ToString());

                    // pick up a validation property otherwise
                else
                {
                    ValidationPropertyAttribute attr = (ValidationPropertyAttribute)Attribute.GetCustomAttribute(ctrl.GetType(), typeof(ValidationPropertyAttribute));

                    if (attr != null)
                    {
                        System.Reflection.PropertyInfo info = ctrl.GetType().GetProperty(attr.Name);
                        object currValue = info.GetValue(ctrl, null);

                        if (currValue != null)
                            SaveDictionaryItem(_results, ctrl.UniqueID, currValue.ToString());
                    }
                    // continue processing naming container sub items if there is no validation property
                    else if (ctrl is INamingContainer || ctrl is Panel || ctrl is PlaceHolder)
                        SaveFormItems(_results, ctrl);

                }
            }
        }

        private static void SaveDictionaryItem(System.Collections.Specialized.StringDictionary _dict,
                                  string name, string value)
        {
            if (_dict.ContainsKey(name))
                _dict[name] = value;
            else
                _dict.Add(name, value);
        }

        public static void LoadFormItems(System.Collections.Specialized.StringDictionary _input, Control _container)
        {
            LoadFormItems(_input, _container, false);
        }

        /// <summary>
        /// Saves the form items: textbox, lists, checkbox, radio button, and validation properties recursively.
        /// </summary>
        /// <param name="_input"></param>
        /// <param name="_container"></param>
        /// <param name="ignoreSerializeAttr">If true it will load all items regardless of attribute</param>
        public static void LoadFormItems(System.Collections.Specialized.StringDictionary _input,
            Control _container, bool ignoreSerializeAttr)
        {
            foreach (Control ctrl in _container.Controls)
            {
                // check for the serialize attribute on the webcontrol
                if (ctrl is WebControl)
                {
                    string serialize = ((WebControl)ctrl).Attributes[Dictionary_Serialize];
                    if (serialize != null && serialize.ToLower() == "false" && !(ignoreSerializeAttr))
                        continue;
                }

                if (!_input.ContainsKey(ctrl.UniqueID))
                    continue;

                string prevAnswer = _input[ctrl.UniqueID];

                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = prevAnswer;

                else if (ctrl is CheckBox)
                    ((CheckBox)ctrl).Checked = bool.Parse(prevAnswer);

                else if (ctrl is ListControl)
                {
                    ListControl _tmpCtrl = (ListControl)ctrl;
                    foreach (string ans in prevAnswer.Split("|".ToCharArray()))
                    {
                        ListItem li = _tmpCtrl.Items.FindByValue(ans);
                        if (li != null)
                        {
                            if ((_tmpCtrl is ListBox) &&
                                ((ListBox)_tmpCtrl).SelectionMode == ListSelectionMode.Multiple)

                                li.Selected = true;
                            else
                                _tmpCtrl.SelectedIndex = _tmpCtrl.Items.IndexOf(li);
                        }
                    }
                }

                else
                {
                    ValidationPropertyAttribute attr = (ValidationPropertyAttribute)
                        Attribute.GetCustomAttribute(ctrl.GetType(),
                        typeof(ValidationPropertyAttribute));

                    if (attr != null && _input.ContainsKey(ctrl.UniqueID))
                        SetValidProp(ctrl, _input[ctrl.UniqueID], attr);
                    else
                        LoadFormItems(_input, ctrl, ignoreSerializeAttr);

                    continue;
                }
            }
        }

        private static void SetValidProp(Control ctrl, string inValue, ValidationPropertyAttribute attr)
        {
            {
                System.Reflection.PropertyInfo info = ctrl.GetType().GetProperty(attr.Name);
                try
                {
                    info.SetValue(ctrl,
                        Convert.ChangeType(inValue, info.PropertyType), null);
                }
                catch
                {
                    try
                    {
                        // try to set a mythical property named value if we failed
                        info = ctrl.GetType().GetProperty("Value");
                        info.SetValue(ctrl,
                            Convert.ChangeType(inValue, info.PropertyType), null);
                    }
                    catch { }
                }
            }

        }
    }
}