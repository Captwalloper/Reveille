using Reveille.Utility.Cortana;
using Reveille.ViewModel;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Reveille.View
{
    /// An empty page that can be used on its own or navigated to within a Frame.
    public sealed partial class MainPage : Page, IAppPage
    {
        public MainViewModel viewModel;

        public MainPage()
        {
            this.InitializeComponent();
            viewModel = new MainViewModel(this);
            this.DataContext = viewModel;
        }

        async Task IAppPage.RespondToVoice(CortanaCommand command)
        {
            await viewModel.RespondToVoice(command);
        }

    }
}
