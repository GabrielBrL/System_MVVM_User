using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserInfo.ViewsModels;

namespace UserInfo.Views
{
    /// <summary>
    /// Interação lógica para UserView.xam
    /// </summary>
    public partial class UserView : UserControl
    {
        private UserViewModel userView;
        public UserView()
        {
            InitializeComponent();
            userView = new UserViewModel();
            this.DataContext = userView;
        }
    }
}
