using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;

namespace RandomNameChanger
{
    [ComVisible(true)]
    [COMServerAssociation(AssociationType.AllFiles)]
    [COMServerAssociation(AssociationType.Directory)]
    public class NameChanger : SharpContextMenu
    {
        protected override bool CanShowMenu()
        {
            return true;
        }

        protected override ContextMenuStrip CreateMenu()
        {
            // create the menu strip.
            var menu = new ContextMenuStrip();
            var RenameFileItem = new ToolStripMenuItem
            {
                Text = "Случайное имя",
                Image = Properties.Resources.random
            };
            RenameFileItem.Click += (sender, args) => RenameFile();
            menu.Items.Add(RenameFileItem);
            return menu;
        }
        private void RenameFile()
        {
            foreach (var filePath in SelectedItemPaths)
            {
                string patchwitoutname = Path.GetDirectoryName(filePath);
                string exten = Path.GetExtension(filePath);
                string oldname = Path.GetFullPath(filePath);
                string newname = patchwitoutname + "\\" + GeneratedName() + exten;

                System.IO.Directory.Move(oldname, newname);              
            }

        }

        private static readonly Random rnd = new Random();

        public string GeneratedName()
        {
            string parameters = "abcdefghijklnopqrstuvwxyz1234567890";
            int count = 13;
            string result = "";
            //    Random rnd = new Random();
            int lenght = parameters.Length;
            for (int i = 0; i < count; i++)
            {
                result += parameters[rnd.Next(lenght)];
            }
            return result;
        }

    }
}
