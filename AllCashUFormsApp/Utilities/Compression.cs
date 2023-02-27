using System;
using System.IO;
using System.IO.Compression;

namespace AllCashUFormsApp
{
    public static class Compression
    {

        //// Path to directory of files to compress.
        //string dirpath = @"c:\users\public\reports";

        //DirectoryInfo di = new DirectoryInfo(dirpath);


        //// Compress the directory's files.
        //foreach (FileInfo fi in di.GetFiles())
        //    Compress(fi);

        //// Decompress all *.gz files in the directory.
        //foreach (FileInfo fi in di.GetFiles("*.gz"))
        //    Decompress(fi);

        // Method to compress.
        public static void Compress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Compressing:
                // Prevent compressing hidden and already compressed files.

                if ((File.GetAttributes(fi.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fi.Extension != ".gz")
                {
                    // Create the compressed file.
                    using (FileStream outFile = File.Create(fi.FullName + ".zip"))
                    {
                        using (GZipStream Compress = new GZipStream(outFile, CompressionMode.Compress))
                        {

                            // Copy the source file into the compression stream.
                            inFile.CopyTo(Compress);

                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.", fi.Name, fi.Length.ToString(), outFile.Length.ToString());
                        }
                    }
                }
            }
        }

        // Method to decompress.
        public static void Decompress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Get orignial file extension, for example "doc" from report.doc.gz.
                string curFile = fi.FullName;
                var origName = curFile.Remove(curFile.Length - fi.Extension.Length);

                // Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                    {

                        // Copy the decompression stream 
                        // into the output file.
                        Decompress.CopyTo(outFile);

                        Console.WriteLine("Decompressed: {0}", fi.Name);
                    }
                }
            }
        }

        public static System.Diagnostics.Process Launch_in_Shell(string WorkingDirectory, string FileName, string Arguments)
        {
            System.Diagnostics.ProcessStartInfo processInfo = new System.Diagnostics.ProcessStartInfo();

            processInfo.WorkingDirectory = WorkingDirectory;
            processInfo.FileName = FileName;
            processInfo.Arguments = Arguments;
            processInfo.UseShellExecute = true;
            System.Diagnostics.Process process = System.Diagnostics.Process.Start(processInfo);

            return process;
        }

        public static void UnZip(string zipFile, string folderPath)
        {
            //if (!File.Exists(zipFile))
            //    throw new FileNotFoundException();

            //if (!Directory.Exists(folderPath))
            //    Directory.CreateDirectory(folderPath);

            //var shellAppType = Type.GetTypeFromProgID("Shell.Application");
            //var oShell = Activator.CreateInstance(shellAppType);
            //var destinationFolder = (Shell32.Folder)shellAppType.InvokeMember("NameSpace", System.Reflection.BindingFlags.InvokeMethod, null, oShell, new object[] { folderPath });
            //var sourceFile = (Shell32.Folder)shellAppType.InvokeMember("NameSpace", System.Reflection.BindingFlags.InvokeMethod, null, oShell, new object[] { zipFile });

            //foreach (var file in sourceFile.Items())
            //{
            //    destinationFolder.CopyHere(file, 4 | 16);
            //}
        }

        public static void ExtractZipFile(string zipFileLocation, string destination)
        {
            //using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile(zipFileLocation))
            //{
            //    zip.ExtractAll(destination);
            //}
        }
    }

}
