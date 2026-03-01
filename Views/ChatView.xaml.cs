using AppGemini.ViewModels;

namespace AppGemini.Views;

public partial class ChatView : ContentPage
{
    public ChatView(ChatViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}