using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace XMLGenerator.ViewModel
{
    public class YesNoDialogViewModel : ViewModelBase
    {
        private ICommand m_yesComman;
        private ICommand m_noCommand;
        private Action m_yesAction;
        private Action m_noAction;

        public YesNoDialogViewModel(Action yesAction, Action noAction)
        {
            m_yesAction = yesAction;
            m_noAction = noAction;

            YesCommand = new DelegateCommand(Yes);
            NoCommand = new DelegateCommand(No);
        }

        private void No()
        {
            m_noAction.Invoke();
        }

        private void Yes()
        {
            m_yesAction.Invoke();
        }

        public ICommand YesCommand { get { return m_yesComman; } set { m_yesComman = value; OnPropertyChanged("YesCommand"); } }
        public ICommand NoCommand { get { return m_noCommand; } set { m_noCommand = value; OnPropertyChanged("NoCommand"); } }
    }
}
