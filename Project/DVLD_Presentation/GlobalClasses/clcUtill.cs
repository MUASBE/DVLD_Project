using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation
{
    public class clcUtill
    {
        public static string GenerateGUID()
        {
            Guid newGuid = Guid.NewGuid();
            return newGuid.ToString();
        }
        public static string ReplaceFileNameWithGUID(string sourceFile)
        {
            FileInfo fi = new FileInfo(sourceFile);
            string extn = fi.Extension;
            return GenerateGUID() + extn;
        }
        public static bool CreateFileIfDoesNotExist()
        {
            string folderPath = @"D:\DVLD_Images";

            if (!Directory.Exists(folderPath))
            {
                try
                {
                    Directory.CreateDirectory(folderPath);
                }
                catch(Exception ex)
                {
                    return false;
                }
            }   

            return true;
        }
        public static bool CopyImageToProjectImageFolder(ref string sourceFile)
        {
            string DestinationFolder = @"D:\DVLD_Images\";

            if (!CreateFileIfDoesNotExist())
            {
                return false;
            }
            
            string FileName = DestinationFolder + ReplaceFileNameWithGUID(sourceFile);
            try
            {
                File.Copy(sourceFile, FileName, true);

            }
            catch (IOException iox)
            {
                MessageBox.Show(iox.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            sourceFile = FileName;
            return true;
        }

    }
}
