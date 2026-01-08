namespace Dashboard.Components
{
    public partial class App
    {
        public string ColorTheme { get; set; } = "light";
        protected override void OnInitialized()
        {
            ColorTheme = "light";
            base.OnInitialized();
        }
    }
}
