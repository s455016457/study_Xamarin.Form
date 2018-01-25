using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinForm.Delegates
{
    public delegate Boolean ListMenuItemClickHandle<T>(T t) where T : Models.MenuItem;
}
