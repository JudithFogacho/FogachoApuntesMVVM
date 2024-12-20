namespace FogachoApuntes
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.AFNotePage), typeof(Views.AFNotePage));
        }
    }
}
