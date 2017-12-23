using System;
using System.Security.Principal;
using System.Windows.Forms;

namespace renzeulware
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!CheckPriv())
            {
                MessageBox.Show("관리자 권한이 필요합니다!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("이 프로그램은 다음과 같은 동작을 수행합니다: \n" +
                            " 1. 레지스트리 정보 수집 및 변경\n" +
                            " 2. 타 프로세스의 주소 참조 및 변경\n" +
                            "이상에 동의하지 않는 경우 [아니오]를 눌러 종료하십시오.", "련즐웨어",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.No) return;

            Application.Run(new RenZeulMain());
        }

        static bool CheckPriv()
        {
            var identity = WindowsIdentity.GetCurrent();

            if (identity != null)
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            return false;
        }
    }
}
