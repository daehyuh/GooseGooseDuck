namespace GooseGooseDuck
{
    internal static class Program
    {
        /// <summary>
        /// �ش� ���ø����̼��� �� �������Դϴ�.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}