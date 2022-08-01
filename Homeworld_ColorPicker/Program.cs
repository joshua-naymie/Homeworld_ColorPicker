namespace Homeworld_ColorPicker
{
    using Forms;
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainWindow());

            //Objects.HomeworldColour c = new Objects.HomeworldColour();

            //System.Diagnostics.Debug.WriteLine(c);

            //TestObj t = new TestObj(ref c);

            //t.ChangeColour();

            //System.Diagnostics.Debug.WriteLine(c);

            //IO.TeamColourReader.ReadTeamColourLua(@"G:\Documents\Homeworld ColorPicker\HW2_RM\leveldata\campaign\ascension\m07_veil_of_fire\teamcolour.lua");
        }
    }
}