using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;

namespace XamarinForm.ViewModel
{
    public class SimpleColorPickerPageViewModel:BaseViewModel
    {
        ColorTypeConverter colorTypeConverter = new ColorTypeConverter();
        public List<String> ColorNames
        {
            get
            {
                return typeof(Color).GetFields().Where(p => p.IsPublic && p.IsStatic).Select(p => p.Name).ToList<String>();
            }
        }

        string selectedColorName;

        public String SelectedColorName
        {
            get
            {
                return selectedColorName;
            }
            set
            {
                if (selectedColorName != value)
                {
                    selectedColorName = value;
                    OnPropertyChanged();
                    OnPropertyChanged("SelectedColor");
                }
            }
        }

        public Color SelectedColor
        {
            get
            {
                if (String.IsNullOrWhiteSpace(selectedColorName))
                {
                    return Color.Default;
                }
                else
                {
                    return (Color)colorTypeConverter.ConvertFromInvariantString(selectedColorName);
                }
            }
        }
    }
}
