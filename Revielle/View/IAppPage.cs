using Reveille.Utility.Cortana;
using System.Threading.Tasks;

namespace Reveille.View
{
    public interface IAppPage
    {
        Task RespondToVoice(CortanaCommand command);
    }
}
