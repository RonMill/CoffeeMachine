using KaffeemaschineWPF.Framework;
using SharedObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeemaschineWPF.States
{
    public class UserStates : ObservableObject, IUserStates
    {
        private IUser _user;
        public IUser User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
    }
}
