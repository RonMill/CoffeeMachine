using SharedObjects;
using System.ComponentModel;

namespace KaffeemaschineWPF.States
{
    public interface IUserStates : INotifyPropertyChanged
    {
        IUser User { get; set; }
    }
}